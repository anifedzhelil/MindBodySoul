import { Component } from '@angular/core';
import { CarouselBullet } from 'src/app/models/instagram/carousel/bullet.model';
import { faTrashCan } from '@fortawesome/free-solid-svg-icons';
import { SlugService } from 'src/app/services/slug/slug.service';
import html2canvas from 'html2canvas';

@Component({
  selector: 'app-carousel',
  templateUrl: './carousel.component.html',
  styleUrl: './carousel.component.css',
  standalone: false
})
export class CarouselComponent {
  isSizeOpen: boolean = true;
  isTextOpen: boolean = true;
  isBulletsOpen: boolean = true;

  paddingSide: number = 60;
  paddingTop: number = 100;
  description: string = '';
  note: string = '';
  descriptionSize: number = 14;
  bulletsSize: number = 14;
  noteSize: number = 14;
  carouselName: string = '';
  notePaddingTop: number = 15;

  bullets: CarouselBullet[] = [];

  faTrashCan = faTrashCan;

  constructor(private slugService: SlugService) {}

  onToggleSizeSection(): void {
    this.isSizeOpen = !this.isSizeOpen;
  }

  onToggleTextSection(): void {
    this.isTextOpen = !this.isTextOpen;
  }

  onToggleBulletsSection(): void {
    this.isBulletsOpen = !this.isBulletsOpen;
  }

  addBullet(): void {
    const bullet: CarouselBullet = {
      id: crypto.randomUUID(),
      text: '',
      header: '',
      carouselId: ''
    };

    this.bullets = [...this.bullets, bullet];
  }

  deleteBullet(id: string): void {
    this.bullets = this.bullets.filter((b) => b.id !== id);
  }

  async downloadCarousel() {
    const element = document.querySelector('.card') as HTMLElement;
      if (!element) return;
    
      const canvas = await html2canvas(element, {
      scale: 2, // 540x675 * 2 = 1080x1350 — реалният Instagram размер
      useCORS: true,
      backgroundColor: '#f6f2ed'
    });

    const link = document.createElement('a');
    link.download = `${this.slugService.toSlug(this.carouselName)}.png`;
    link.href = canvas.toDataURL('image/png');
    link.click();
  }
}
