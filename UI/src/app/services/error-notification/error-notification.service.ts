import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ErrorNotificationService {
  private message$$ = new Subject<string>();

  message$ = this.message$$.asObservable();

  constructor() { }

  show(message: string){
    this.message$$.next(message);
  }
}
