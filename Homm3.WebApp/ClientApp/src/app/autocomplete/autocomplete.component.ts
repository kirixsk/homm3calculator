import { Component, EventEmitter, Input, Output } from "@angular/core";
import { AbstractControl, ControlValueAccessor, FormControl, NG_VALIDATORS, NG_VALUE_ACCESSOR, ValidationErrors, Validator } from "@angular/forms";
import { Observable, map, startWith, Subject, pipe, combineLatest } from "rxjs";

@Component({
  selector: 'app-autocomplete',
  templateUrl: "./autocomplete.component.html",
  styleUrls: ["autocomplete.component.css"],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: AutocompleteComponent
    },
    {
      provide: NG_VALIDATORS,
      multi: true,
      useExisting: AutocompleteComponent
    }
  ]
})
export class AutocompleteComponent implements ControlValueAccessor, Validator {

  autocompleteControl = new FormControl('');

  updateValuesSubject = new Subject<string>();

  public filteredValues: Observable<string[]> =
    combineLatest(
      this.autocompleteControl.valueChanges.pipe(startWith('')),
      this.updateValuesSubject.pipe(startWith('')))
      .pipe(map(v => this.filter(v[0] || '')));

  valuesDictionary: any = {};
  valuesArray: string[] = [];

  @Input()
  set values(val: string[]) {
    this.valuesArray = val;
    this.valuesDictionary = val.reduce((acc: any, curr) => (acc[curr] = ['1'], acc), {});
    this.updateValuesSubject.next("");
  }
  get values() {
    return this.valuesArray
  }

  @Input()
  autocompleteLabel: string = '';

  @Output() onDelete = new EventEmitter();

  onChange = (inputValue: any) => { };

  onTouched = () => { };

  touched = false;

  disabled = false;

  writeValue(input: string) {
    this.autocompleteControl.setValue(input);
  }

  registerOnChange(onChange: any) {
    this.onChange = onChange;
    this.autocompleteControl.valueChanges.subscribe(value => {
      this.onChange(value);
    });
  }

  registerOnTouched(onTouched: any) {
    this.onTouched = onTouched;
  }

  deleteMapObject() {
    this.onDelete.emit();
  }

  markAsTouched() {
    if (!this.touched) {
      this.onTouched();
      this.touched = true;
    }
  }

  private filter(value: string): string[] {
    const filterValue = value.toLowerCase();
    if (this.values == null) {
      return []
    }
    return this.values.filter(option => option.toLowerCase().includes(filterValue));
  }

  setDisabledState(disabled: boolean) {
    this.disabled = disabled;
  }

  validate(control: AbstractControl): ValidationErrors | null {
    return !this.valuesDictionary[control.value] ? { errorValue: { value: control.value } } : null;
  }
}
