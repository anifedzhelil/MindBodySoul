import { Component } from '@angular/core';

@Component({
  selector: 'app-carousel',
  templateUrl: './carousel.component.html',
  styleUrl: './carousel.component.css',
  standalone: false
})
export class CarouselComponent {
  isSizeOpen: boolean = true;
  isTextOpen:  boolean = false;

  paddingSide: number = 10;
  paddingTop: number = 20;
  description: string = '';
  note: string = '';


  onToggleSizeSection(): void{
    this.isSizeOpen = !this.isSizeOpen;
  }

  onToggleTextSection(): void{
    this.isTextOpen = !this.isTextOpen;

  }
}
