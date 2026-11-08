import { Component } from '@angular/core';
import { faCaretRight, faCaretLeft } from '@fortawesome/free-solid-svg-icons';
import { ArticleService } from 'src/app/services/article/article.service';
import { LatestArticle } from 'src/app/models/article/latest-article.model';
import { HostListener } from '@angular/core';


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
    this.loadArticles();
  }

  @HostListener('window:resize')
  onResize() {
    this.updateVisibleArticles();
  }

  loadArticles(): void {
    this.articleService.getLatestArticles(8).subscribe({
      next: (data) => {
        this.articles = data;
        this.updateVisibleArticles();
      },
      error: (err) => {
        console.error(err);
      }
    });
  }

  updateVisibleArticles(){
        window.innerWidth <= 480 ? this.visibleArticles = this.articles :   this.getArticles(this.currentIndex);
  }

  getArticles(index: number): void {
    this.currentIndex = index;
    this.visibleArticles = this.articles.slice(index, index + 4);
  }
}


