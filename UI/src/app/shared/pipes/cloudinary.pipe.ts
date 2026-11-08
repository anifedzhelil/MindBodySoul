import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'cloudinary',
  standalone: false,
})
export class CloudinaryPipe implements PipeTransform {

  transform(value: string, width: number): string {
    if (!value) return '';
    
    return value.replace(`/upload/`, `/upload/w_${width},f_auto,q_auto/`);
  }
}
