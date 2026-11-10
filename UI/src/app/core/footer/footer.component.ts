import { Component } from '@angular/core';
import {faInstagram} from '@fortawesome/free-brands-svg-icons';
@Component({
    selector: 'app-footer',
    templateUrl: './footer.component.html',
    styleUrls: ['./footer.component.css'],
    standalone: false
})
export class FooterComponent {
    currentYear = new Date().getFullYear(); 
    faInstagram = faInstagram;
}
