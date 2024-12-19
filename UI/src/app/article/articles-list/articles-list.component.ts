import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { ArticleList } from 'src/app/models/article/article-list-response.model';
import { ArticleService } from 'src/app/services/article/article.service';
import { TagService } from 'src/app/services/tag/tag.service';

@Component({
  selector: 'app-articles-list',
  templateUrl: './articles-list.component.html',
  styleUrls: ['./articles-list.component.css'],
})
export class ArticlesListComponent implements OnInit {
  articles$: Observable<ArticleList[]> = new Observable<ArticleList[]>();
  search: string | null = null;
  constructor(
    private articleService: ArticleService,
    private tagService: TagService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (params) => {
        var tagId = params.get('tagId');

        if (tagId) {
           this.tagService.getTagById(tagId).subscribe({
            next: (tag) => {
              this.search = tag.name;
            },
          });

          this.articles$ = this.articleService.getAllArticlesByTag(tagId);
        } else {
          var search = params.get('search');
          if (search) {
            this.search = search;
            this.articles$ = this.articleService.getFilteredArticles(search);
          } else {
            this.articles$ = this.articleService.getAllArticles();
          }
        }
      },
      error: (err) => {
        console.error;
      },
    });
  }
}
