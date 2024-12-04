import { FormGroup, ValidatorFn } from '@angular/forms';

export function matchPasswordsValidator(
  passCtrlOne: string,
  passCtrlTwo: string
): ValidatorFn {
  return (control) => {
    const group = control as FormGroup;
    const pass1 = group.get(passCtrlOne);
    const pass2 = group.get(passCtrlTwo);

    return pass1?.value == pass2?.value
      ? null
      : { matchPasswordsValidator: true };
  };
}
