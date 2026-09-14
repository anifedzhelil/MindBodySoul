import { CarouselBullet } from "./bullet.model";

export interface CarouselDraft{
    id: string;
    paddingSide: number;
    paddingTop: number;
    descriptionSize: number;
    bulletsSize: number;
    notePaddingTop: number;
    noteSize: number;
    description: string;
    note: string;
    carouselName: string;
    bullets: CarouselBullet[];
}