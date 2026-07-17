import { Component } from '@angular/core';

@Component({
  selector: 'app-instagram',
  templateUrl: './instagram.component.html',
  styleUrl: './instagram.component.css',
  standalone: false
})
export class InstagramComponent {
  activeTab: 'carousel' | 'thumbnail' = 'thumbnail';

}
