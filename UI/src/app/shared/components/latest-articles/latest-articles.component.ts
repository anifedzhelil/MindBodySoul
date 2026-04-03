import { a } from '@angular/cdk/portal-directives.d-BoG39gYN';
import { Component } from '@angular/core';
import { faCaretRight, faCaretLeft } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-latest-articles',
  templateUrl: './latest-articles.component.html',
  styleUrl: './latest-articles.component.css',
  standalone: false,
})
export class LatestArticlesComponent {
  faCaretRight = faCaretRight;
  faCaretLeft = faCaretLeft;
  currentIndex = 0;
  visibleArticles: any[] = [];

  articles = [
    {
      id: 1,
      title: 'Лечебните свойства на лавандулата',
      imageUrl: 'https://picsum.photos/400/200?random=1',
      excerpt:
        'Открийте как лавандулата може да подобри съня и намали стреса в ежедневието.',
    },
    {
      id: 2,
      title: 'Нумерология: Числото на живота',
      imageUrl: 'https://picsum.photos/400/200?random=2',
      excerpt: 'Научете какво разкрива вашето число за личността и съдбата ви.',
    },
    {
      id: 3,
      title: 'Хиджама - древна лечебна практика',
      imageUrl: 'https://picsum.photos/400/200?random=3',
      excerpt:
        'Запознайте се с ползите от традиционната ислямска медицина и как тя помага днес.',
    },
    {
      id: 4,
      title: 'Детокс - древна лечебна практика',
      imageUrl: 'https://picsum.photos/400/200?random=4',
      excerpt:
        'Запознайте се с ползите от традиционната ислямска медицина и как тя помага днес.',
    },
    {
      id: 5,
      title: 'Мокса - древна лечебна практика',
      imageUrl: 'https://picsum.photos/400/200?random=5',
      excerpt:
        'Запознайте се с ползите от традиционната ислямска медицина и как тя помага днес.',
    },
    {
      id: 6,
      title: 'Хиродотерапия - древна лечебна практика',
      imageUrl: 'https://picsum.photos/400/200?random=6',
      excerpt:
        'Запознайте се с ползите от традиционната ислямска медицина и как тя помага днес.',
    },
    {
      id: 7,
      title: 'Масаж - древна лечебна практика',
      imageUrl: 'https://picsum.photos/400/200?random=7',
      excerpt:
        'Запознайте се с ползите от традиционната ислямска медицина и как тя помага днес.',
    },
  ];

  ngOnInit(): void {
    this.getArticles(this.currentIndex);
  }

  getArticles(index: number): void {
    debugger;
    this.visibleArticles = this.articles.slice(index, index + 4);
  }
}
