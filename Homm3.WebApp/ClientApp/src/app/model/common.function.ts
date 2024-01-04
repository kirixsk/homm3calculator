import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";
import { Monster } from "./model";

export function getMonsterFilterFunction(monsters: Monster[]): Function {
  return (values: string[]): Monster[] => {
    var [name, town, level, upgrade] = values;
    return monsters.filter(option => (
      ((!level) || (option.level.toString() === level)) &&
      ((!town) || option.town === town)) &&
      ((!upgrade) || ((option.upgradeLevel === 0) && (upgrade === '0')) || (option.upgradeLevel > 0 && upgrade === '1')) &&
      (option.name.toLowerCase().includes(name)));
  };
}

export function nameInDictionaryValidator(allowedValues: any): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    var c = !allowedValues[control.value] ? { errorValue: { value: control.value } } : null;
    return c;
  };
}
