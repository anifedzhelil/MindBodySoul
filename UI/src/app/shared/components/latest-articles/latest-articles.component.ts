import { Component } from '@angular/core';
import { faCaretRight, faCaretLeft } from '@fortawesome/free-solid-svg-icons';
import { ArticleService } from 'src/app/services/article/article.service';
import { LatestArticle } from 'src/app/models/article/latest-article.model';

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

  articles: LatestArticle[] = [];

  constructor(private articleService: ArticleService) {}

  ngOnInit(): void {
    this.articleService.getLatestArticles(8).subscribe({
      next: (data) => {
        this.articles = data;
        this.getArticles(this.currentIndex);
      },
      error: (err) => {
        console.error(err);
      }
    });
  }

  getArticles(index: number): void {
    this.currentIndex = index;
    this.visibleArticles = this.articles.slice(index, index + 4);
  }
}
