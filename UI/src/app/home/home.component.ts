import { Component } from '@angular/core';
import { environment } from '../../environments/environment';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css'],
    standalone: false
})
export class HomeComponent {
 get backgroundStyle() {
    return {
      'background-image': `url(${environment.production ? '/MindBodySoul' : ''}/assets/images/home6.jpg)`
    };
  }
}
