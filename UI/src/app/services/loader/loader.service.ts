import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LoaderService {
  isHidden: boolean = true;

  showLoader(){
    this.isHidden = false;
  }

  hideLoader(){
    this.isHidden = true;
  }
}
