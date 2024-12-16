import { Pipe, PipeTransform } from '@angular/core';
import * as moment from 'moment';
import 'moment/locale/bg'; 


@Pipe({
  name: 'elapsedTime'
})
export class ElapsedTimePipe implements PipeTransform {

  transform(dateString: string, ...args: unknown[]): unknown {
    moment.locale('bg');

    return moment(dateString).fromNow();
  }

}
