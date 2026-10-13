import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ValidationErrorService {

  fieldNames: Record<string, string> = {
    Title: 'Заглавие',
    SubCategoryId: 'Подкатегория',
    ImageUrl: 'Снимка',
    Content: 'Съдържание',
  };

  constructor() { }

  getErrorMessage(errors: any): string {
    if (errors) {
      const invalidFields = Object.keys(errors).map(
        (key) => this.fieldNames[key] ?? key
      );
      return 'Невалидни полета: ' + invalidFields.join(', ');
    }

    return 'Невалидни данни, въведете коректни данни!';


  }
}
