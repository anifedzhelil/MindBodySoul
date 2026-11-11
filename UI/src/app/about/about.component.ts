import { Component } from '@angular/core';
import { faInstagram } from '@fortawesome/free-brands-svg-icons';
@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrl: './about.component.css',
  standalone: false,
})
export class AboutComponent {
  faInstagram = faInstagram;
}
