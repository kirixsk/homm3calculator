import { Component, Inject, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { combineLatest, forkJoin, Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { FormBuilder } from '@angular/forms';
import { Monster, ReferenceData } from '../model/model';
import { Homm3Service } from '../services/homm3.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Clipboard } from '@angular/cdk/clipboard';
import { MatSnackBar } from '@angular/material/snack-bar';
import { getMonsterFilterFunction, nameInDictionaryValidator } from '../model/common.function';


@Component({
  selector: 'app-guesser',
  templateUrl: './guesser.component.html',
  styleUrls: ['guesser.component.css'],
})
export class GuesserComponent implements OnInit {
  public referenceData: ReferenceData | null = null;
  public mapObjectNames: string[] = [];
  public calculatorForm!: FormGroup;
  public formControl: FormControl = new FormControl('');
  public filteredMonsters!: Observable<Monster[]>;
  public filteredObjects!: Observable<string[]>;
  public monsterLevels: string[] = [];
  public calculationResult: any;
  public isLoading: boolean = false;
  public guessableObjects: string[] = [];
  public zoneGuardGuessableObjects: string[] = [];
  public nonZoneGuardGuessableObjects: string[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private service: Homm3Service,
    private clipboard: Clipboard,
    private snackBar: MatSnackBar,

    @Inject('BASE_URL') private baseUrl: string) { }

  ngOnInit(): void {
    const miniurl = this.route.snapshot.paramMap.get('miniurl');
    if (miniurl) {
      this.isLoading = true;
      forkJoin(
        this.service.getReferenceData(),
        this.service.getFormValue(miniurl)
      ).subscribe(result => {
        this.isLoading = false;
        const [ref, urlResult] = result;
        this.handleReferenceData(ref, JSON.parse(urlResult.value));
      }, error => this.snackBar.open('Error! Please update the form', 'Ok', {
        duration: 3000
      }));
    }
    else {
      this.service.getReferenceData().subscribe(result => {
        this.handleReferenceData(result, null);
      }, error => console.error(error));
    }
  }

  private handleReferenceData(result: ReferenceData, formValue: any) {
    this.referenceData = result;
    this.zoneGuardGuessableObjects = (this.referenceData?.guessableObjects ?? []).filter(v => v === "ZoneGuard");
    this.nonZoneGuardGuessableObjects = (this.referenceData?.guessableObjects ?? []).filter(v => v !== "ZoneGuard");
    this.guessableObjects = this.nonZoneGuardGuessableObjects;
    this.mapObjectNames = this.referenceData.mapObjects.map(x => x.name);
    var objectNames =
      formValue
        ? formValue.objectNames.map((v: any) => this.formBuilder.group({ name: [v, nameInDictionaryValidator(this.arrayToObjectKeys(this.referenceData!.mapObjects.map(x => x.name), ['1]']))] }))
        : [this.formBuilder.group({ name: ["Gold", nameInDictionaryValidator(this.arrayToObjectKeys(this.referenceData.mapObjects.map(x => x.name), ['1']))] })]
    this.calculatorForm = this.formBuilder.group({
      monsterName: [this.referenceData.monsters[0].name, nameInDictionaryValidator(this.arrayToObjectKeys(this.referenceData.monsters.map(x => x.name), ['1']))],
      mapMonsterStrengthName: ['Strong'],
      zoneStrengthName: ['Weak'],
      townName: ['Castle'],
      monsterTown: [''],
      monsterLevel: [''],
      monsterUpgrade: [''],
      totalTownZoneCount: ['5', [Validators.required, Validators.pattern("^[0-9]*$")]],
      currentTownZoneCount: ['1', [Validators.required, Validators.pattern("^[0-9]*$")]],
      visual: ["Pandora", [nameInDictionaryValidator(this.arrayToObjectKeys(this.referenceData.guessableObjects, ['1']))]],
      minMonsterCount: ["50", [Validators.required, Validators.pattern("^[0-9]*$")]],
      maxMonsterCount: ["99", [Validators.required, Validators.pattern("^[0-9]*$")]],
      objectNames: this.formBuilder.array(objectNames),
      townZoneCounts: this.formBuilder.group(this.arrayToObjectKeys(this.referenceData.towns, ["1", [Validators.required, Validators.pattern("^[0-9]*$")]])),
      week: ['1', [Validators.required, Validators.pattern("^[0-9]*$")]],
    }, {
      validators: [this.zoneGuardValidator()]
    });

    this.calculatorForm.get("zoneStrengthName")!.valueChanges.subscribe(x => {
      if (x == "ZoneGuard" && this.guessableObjects != this.zoneGuardGuessableObjects) {
        this.guessableObjects = this.zoneGuardGuessableObjects;
        this.calculatorForm.get("visual")?.patchValue(this.guessableObjects[0]);
      }
      if (x != "ZoneGuard" && this.guessableObjects != this.nonZoneGuardGuessableObjects) {
        this.guessableObjects = this.nonZoneGuardGuessableObjects;
        this.calculatorForm.get("visual")?.patchValue(this.guessableObjects[0]);
      }
    });

    if (formValue) {
      this.calculatorForm.patchValue(formValue);
    }
    const monsterLevel = this.referenceData?.monsters.map(x => x.level.toString());
    this.monsterLevels = Array.from(new Set(monsterLevel)).sort();
    const monsterFilterFunction = getMonsterFilterFunction(this.referenceData?.monsters ?? []);
    this.filteredMonsters = combineLatest(
      this.calculatorForm.get("monsterName")!.valueChanges.pipe(startWith('')),
      this.calculatorForm.get("monsterTown")!.valueChanges.pipe(startWith('')),
      this.calculatorForm.get("monsterLevel")!.valueChanges.pipe(startWith('')),
      this.calculatorForm.get("monsterUpgrade")!.valueChanges.pipe(startWith('')),
    ).pipe(
      map(value => monsterFilterFunction(value || ['', '', '']))
    );

    this.filteredObjects = this.calculatorForm.get("townName")!.valueChanges.pipe(
      startWith('Castle'),
      map(value => (this.referenceData?.mapObjects ?? []).filter(x => (!x.town) || (x.town == value)).map(x => x.name)));

    if (formValue?.preset) {
      this.formControl.setValue(formValue.preset);
    }
    else {
      this.formControl.setValue("Jebus Cross Start");
    }

    this.formControl.valueChanges.subscribe(x => {
      var newPreset = this.referenceData?.presets.filter(v => v.name === x)[0];
      var actualPreset = newPreset ?? this.referenceData?.presets[0];
      var newFormValue =
      {
        mapMonsterStrengthName: actualPreset.monsterStrength,
        zoneStrengthName: actualPreset.zoneStrength,
        totalTownZoneCount: actualPreset.totalTownZoneCount,
        zoneGuardValue: actualPreset.zoneGuardValue,
        townZoneCounts: actualPreset.townZoneCounts
      };
      this.calculatorForm.patchValue(newFormValue);
    });

    if (this.calculatorForm.valid) {
      this.onFormSubmit();
    }
    this.calculatorForm.valueChanges.subscribe(x => {
      if (this.calculatorForm.valid) {
        this.onFormSubmit();
      }
    });
  }

  zoneGuardValidator(): ValidatorFn {
    return (ctrl: AbstractControl): ValidationErrors | null => {
      const formGroup = ctrl as FormGroup;
      const zoneStrengthName = formGroup.get('zoneStrengthName');
      const visual = formGroup.get('visual');
      if (!zoneStrengthName || !visual) {
        return null;
      }

      if (zoneStrengthName.value === "ZoneGuard") {
        return visual.value === "ZoneGuard" ? null : { errorValue: { visual } };
      }
      return visual.value === "ZoneGuard" ? { errorValue: { value: visual.value } } : null;
    };
  }

  shareUrl() {
    this.isLoading = true;
    var formValue =
    {
      ...this.calculatorForm.value,
      preset: this.formControl.value
    };
    this.service.saveFormValue(formValue).subscribe(result => {
      this.isLoading = false;
      var newUrl = `${this.baseUrl}guesser/${result.value}`;
      this.clipboard.copy(newUrl);
      this.snackBar.open('Copied to clipboard', 'Ok', {
        duration: 3000
      });
    }, error => {
      this.isLoading = false;
      this.snackBar.open('Error! Please update the form', 'Ok', {
        duration: 3000
      });
    });
  }

  private arrayToObjectKeys(arr: string[], val: any): any {
    return arr.reduce((acc: any, curr) => (acc[curr] = [...val], acc), {});
  }

  onFormSubmit() {
    var requestBody = {
      ...this.calculatorForm.value,
      objectNames: this.calculatorForm.value.objectNames.map((x: any) => x.name),
    };
    this.isLoading = true;
    this.service.guessMapObject(requestBody).subscribe(result => {
      this.calculationResult = result;
      this.isLoading = false;
    }, error => {
      this.isLoading = false;
      this.snackBar.open('Error! Please update the form', 'Ok', {
        duration: 3000
      });
    });
  }

  get objectNames(): FormArray {
    return this.calculatorForm.get("objectNames") as FormArray;
  }

  get townZoneCountsFields(): string[] {
    return this.referenceData?.towns ?? [];;
  }

  deleteMapObject(mapObjectIndex: number) {
    this.objectNames.removeAt(mapObjectIndex);
  }

  addMapObject() {
    const objectForm = this.formBuilder.group({
      name: [this.referenceData?.mapObjects[0].name, nameInDictionaryValidator(this.arrayToObjectKeys(this.referenceData!.mapObjects.map(x => x.name), ['1']))]
    });
    this.objectNames.push(objectForm);
  }
}
