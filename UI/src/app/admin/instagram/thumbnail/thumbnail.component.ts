import { Component } from '@angular/core';
import { faImage, faPhotoVideo } from '@fortawesome/free-solid-svg-icons';
import html2canvas from 'html2canvas';
import { SlugService } from 'src/app/services/slug/slug.service';


@Component({
  selector: 'app-thumbnail',
  templateUrl: './thumbnail.component.html',
  styleUrl: './thumbnail.component.css',
  standalone: false
})
export class ThumbnailComponent {
  previewUrl: string | null = null;
  selectedFile: File | null = null;
  faImage = faImage;
  faPhotoVideo = faPhotoVideo;
  zoom: number = 1;
  offsetX: number = 0;
  offsetY: number = 0;
  title: string = "";
  titleFontSize: number = 28; 
  titleTop: number = 70;
  category: string = "";
  selectedFileName: string = '';


  isPhotoOpen: boolean = true;
  isTitleOpen: boolean =  true;
  isCategoryOpen: boolean= true;

  constructor(private slugService: SlugService) {}

  onTogglePhotoSection(): void {
    this.isPhotoOpen = !this.isPhotoOpen;
  }

  onToggleTitleSection(): void{
    this.isTitleOpen = !this.isTitleOpen;
  }

   onToggleCategorySection(): void{
    this.isCategoryOpen = !this.isCategoryOpen;
  }


async downloadThumbnail() {
  const element = document.querySelector('.card') as HTMLElement;
  if (!element) return;

  const canvas = await html2canvas(element, {
    scale: 2, // 540x675 * 2 = 1080x1350 — реалният Instagram размер
    useCORS: true,
    backgroundColor: '#f6f2ed'
  });

  const link = document.createElement('a');
  link.download = `${this.slugService.toSlug(this.title)}.png`;
  link.href = canvas.toDataURL('image/png');
  link.click();
}
  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
  
    if (input.files && input.files.length > 0) {
    const file = input.files[0];
    this.selectedFileName = file.name;
    }

    if (this.previewUrl) {
      URL.revokeObjectURL(this.previewUrl);
    }

    if (input.files && input.files.length > 0) {
      this.selectedFile = input.files[0];
      this.previewUrl = URL.createObjectURL(this.selectedFile);
    } else {
      this.selectedFile = null;
      this.previewUrl = null; 
    }
  }

}
