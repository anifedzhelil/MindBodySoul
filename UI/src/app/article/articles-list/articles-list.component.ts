import { Component, OnInit } from '@angular/core';
import { ArticleListResponse } from 'src/app/models/article/article-list-response.model';
import { ArticleService } from 'src/app/services/article/article.service';

@Component({
  selector: 'app-articles-list',
  templateUrl: './articles-list.component.html',
  styleUrls: ['./articles-list.component.css'],
})
export class ArticlesListComponent implements OnInit {
  articles: ArticleListResponse[] | undefined;

  constructor(private articleService: ArticleService){}
  ngOnInit(): void {
    this.articleService.getAllArticles().subscribe({
      next: (response) => {
        this.articles = response;
      },
    });
  }


}
