import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ArticleDetails } from 'src/app/models/article/article-details.model';
import { ArticleService } from 'src/app/services/article/article.service';

@Component({
  selector: 'app-article-details',
  templateUrl: './article-details.component.html',
  styleUrls: ['./article-details.component.css'],
})
export class ArticleDetailsComponent implements OnInit {
  article: ArticleDetails | undefined;
  id: string | null = null;

  constructor(
    private articleService: ArticleService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    console.log("Details");
    this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');
        if (this.id != null) {
          this.articleService.getArticleById(this.id).subscribe({
            next: (response) => {
              this.article = response;
            },
            error: (err)=>{
              console.error(err)
            }
          });
        }
      },
    });
  }
}
