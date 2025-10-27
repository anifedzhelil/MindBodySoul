import { Component } from '@angular/core';
import { LoaderService } from 'src/app/services/loader/loader.service';

@Component({
    selector: 'app-loader',
    templateUrl: './loader.component.html',
    styleUrls: ['./loader.component.css'],
    standalone: false
})
export class LoaderComponent {
  constructor(public loaderServices: LoaderService){}
}
