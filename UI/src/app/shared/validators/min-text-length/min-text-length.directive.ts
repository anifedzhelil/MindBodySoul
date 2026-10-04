import { Directive, Input } from '@angular/core';
import { NG_VALIDATORS, AbstractControl, ValidationErrors, Validator } from '@angular/forms';

@Directive({
  selector: '[appMinTextLength]',
  providers: [
        {
            provide: NG_VALIDATORS,
            useExisting: MinTextLengthDirective,
            multi: true,
        },
    ],
    standalone: false
})
export class MinTextLengthDirective {
  // Minimum number of visible characters, bound as [appMinTextLength]="150"
  @Input('appMinTextLength') minLength = 0;

  validate(control: AbstractControl): ValidationErrors | null{
    const html: string = control.value ?? '';

    // Parse the HTML and read only the visible text (entities like &nbsp; are decoded)
    const text = new DOMParser()
      .parseFromString(html, 'text/html')
      .body.textContent?.trim() ?? '';

      return text.length >= this.minLength
        ? null
        : {minTextLength:{requiredLength: this.minLength, actualLength: text.length} } ;
  }
  constructor() { }

}
