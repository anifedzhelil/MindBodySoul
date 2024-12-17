import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { ArticleList } from 'src/app/models/article/article-list-response.model';
import { ArticleService } from 'src/app/services/article/article.service';

@Component({
  selector: 'app-articles-list',
  templateUrl: './articles-list.component.html',
  styleUrls: ['./articles-list.component.css'],
})
export class ArticlesListComponent implements OnInit {
    articles$: Observable<ArticleList[]> = new Observable<ArticleList[]>();

  constructor(private articleService: ArticleService){}
  ngOnInit(): void {
   this.articles$ = this.articleService.getAllArticles();
  }
}
