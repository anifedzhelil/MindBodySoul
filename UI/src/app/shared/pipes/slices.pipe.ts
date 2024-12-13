import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'slices'
})
export class SlicesPipe implements PipeTransform {

  transform(value: string, maxCharCount = 10): string {
    return `${value.substring(0, maxCharCount)}${
      value.length > maxCharCount ? '...' : ''
    }`;
  }

}
