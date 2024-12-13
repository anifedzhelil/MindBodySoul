import { Pipe, PipeTransform } from '@angular/core';
import * as moment from 'moment';
@Pipe({
  name: 'formatTime',
})
export class FormatTimePipe implements PipeTransform {
  transform(dateString: string, ...args: unknown[]): unknown {
    return moment().format('DD/MM/YYYY г.');
  }
}
