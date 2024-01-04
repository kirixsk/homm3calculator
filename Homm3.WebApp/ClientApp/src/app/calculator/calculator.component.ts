import { Component, Inject, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { forkJoin, Observable, merge, combineLatest } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { FormBuilder } from '@angular/forms';
import { Monster, ReferenceData } from '../model/model';
import { Homm3Service } from '../services/homm3.service';
import { ActivatedRoute } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Clipboard } from '@angular/cdk/clipboard';
import { getMonsterFilterFunction, nameInDictionaryValidator } from '../model/common.function';

@Component({
  selector: 'app-calculator',
  templateUrl: './calculator.component.html',
  styleUrls: ['calculator.component.css'],
})
export class CalculatorComponent implements OnInit {
  public referenceData: ReferenceData | null = null;
  public calculatorForm!: FormGroup;
  public formControl: FormControl = new FormControl('');
  public filteredMonsters!: Observable<Monster[]>;
  public filteredObjects!: Observable<string[]>;
  public mapObjectNames: string[] = [];
  public monsterLevels: string[] = [];
  public calculationResult: any;
  public isLoading: boolean = false;

  constructor(
    private formBuilder: FormBuilder,
    private service: Homm3Service,
    private clipboard: Clipboard,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute,
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
      }, error => console.error(error));
    }
    else {
      this.service.getReferenceData().subscribe(result => {
        this.handleReferenceData(result, null);
      }, error => console.error(error));
    }
  }

  private handleReferenceData(result: ReferenceData, formValue: any) {
    this.referenceData = result;
    var objectNames =
      formValue
        ? formValue.objectNames.map((v: any) => this.formBuilder.group({ name: [v, nameInDictionaryValidator(this.arrayToObjectKeys(this.referenceData!.mapObjects.map(x => x.name), ["1"]))] }))
        : [this.formBuilder.group({ name: ["Ancient Lamp", nameInDictionaryValidator(this.arrayToObjectKeys(this.referenceData.mapObjects.map(x => x.name), ["1"]))] })]

    this.calculatorForm = this.formBuilder.group({
      monsterName: [this.referenceData.monsters[0].name, nameInDictionaryValidator(this.arrayToObjectKeys(this.referenceData.monsters.map(x => x.name), ["1"]))],
      mapMonsterStrengthName: ['Strong'],
      zoneStrengthName: ['Weak'],
      townName: ['Castle'],
      monsterTown: [''],
      monsterLevel: [''],
      monsterUpgrade: [''],
      totalTownZoneCount: ['5', [Validators.required, Validators.pattern("^[0-9]*$")]],
      currentTownZoneCount: ['1', [Validators.required, Validators.pattern("^[0-9]*$")]],
      zoneGuardValue: ['45000', [Validators.required, Validators.pattern("^[0-9]*$")]],
      objectNames: this.formBuilder.array(objectNames),
      week: ['1', [Validators.required, Validators.pattern("^[0-9]*$")]],
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
      map(value =>  (this.referenceData?.mapObjects ?? []).filter(x => (!x.town) || (x.town == value)).map(x => x.name)));
    
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
        currentTownZoneCount: actualPreset.currentTownZoneCount,
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

  private arrayToObjectKeys(arr: string[], val: any): any {
    return arr.reduce((acc: any, curr) => (acc[curr] = [...val], acc), {});
  }

  onFormSubmit() {
    var formValue = this.calculatorForm.value;
    var objectNames = (formValue.zoneStrengthName === 'ZoneGuard')
      ? [`ZoneGuard${formValue.zoneGuardValue}`]
      : formValue.objectNames.map((x: any) => x.name)

    var requestBody = {
      ...formValue,
      objectNames
    };
    this.isLoading = true;
    this.service.getMonsterValue(requestBody).subscribe(result => {
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
      name: [this.referenceData?.mapObjects[0].name, nameInDictionaryValidator(this.arrayToObjectKeys(this.referenceData!.mapObjects.map(x => x.name), "1"))]
    });
    this.objectNames.push(objectForm);
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
      var newUrl = `${this.baseUrl}calculator/${result.value}`;
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
}
