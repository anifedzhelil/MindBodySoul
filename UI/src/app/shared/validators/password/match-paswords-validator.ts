import { FormGroup, ValidatorFn } from "@angular/forms";

export function matchPasswordsValidator(
  passCtrlOne: string,
  passCtrlTwo: string
): ValidatorFn {
  return (control) => {
    const group = control as FormGroup;
    const pass1 = group.get(passCtrlOne);
    const pass2 = group.get(passCtrlTwo);

    if (!pass1 || !pass2) return null;

    const areEqual = pass1.value === pass2.value;

    pass2.setErrors(areEqual ? null : { matchPasswordsValidator: true }, { emitEvent: true });

    return areEqual ? null : { matchPasswordsValidator: true };
  };
}
