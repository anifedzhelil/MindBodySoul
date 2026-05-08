import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'slices',
  standalone: false,
})
export class SlicesPipe implements PipeTransform {
  transform(value: string, maxCharCount = 10): string {
    const withSpaces = value.replace(/<\/(p|h1|h2|h3|li|br)>/gi, ' ');
    const temp = document.createElement('div');
    temp.innerHTML = withSpaces;
    const plainText = (temp.textContent || temp.innerText || '').replace(
      /\.([А-ЯA-Z])/g,
      '. $1',
    );

    return `${plainText.substring(0, maxCharCount)}${
      plainText.length > maxCharCount ? '...' : ''
    }`;
  }
}
