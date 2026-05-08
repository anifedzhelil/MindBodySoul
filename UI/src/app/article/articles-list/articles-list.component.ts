import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { ArticleList } from 'src/app/models/article/article-list-response.model';
import { Tag } from 'src/app/models/tag/tag.model';
import { ArticleService } from 'src/app/services/article/article.service';
import { TagService } from 'src/app/services/tag/tag.service';

@Component({
    selector: 'app-articles-list',
    templateUrl: './articles-list.component.html',
    styleUrls: ['./articles-list.component.css'],
    standalone: false
})
export class ArticlesListComponent implements OnInit {
  articles$: Observable<ArticleList[]> = new Observable<ArticleList[]>();
  search: string | null = null;
  allTags: Tag[] = [];
  activeTagId: string | null = null;

  constructor(
    private articleService: ArticleService,
    private tagService: TagService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {

    this.tagService.getAllTags().subscribe({
      next: (tags) => {
        this.allTags = tags;
        console.log('Tags loaded successfully');
        console.log(this.allTags);
      },
      error: (err) => {
        console.error(err);
      }
    });

    this.route.paramMap.subscribe({
      next: (params) => {
        this.activeTagId  = params.get('tagId');

        if (this.activeTagId) {
           this.tagService.getTagById(this.activeTagId).subscribe({
            next: (tag) => {
              this.search = tag.name;
            },
          });

          this.articles$ = this.articleService.getAllArticlesByTag(this.activeTagId);
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

  onTagSelected = (tagId: string) => {
    this.activeTagId = tagId;
    this.articles$ = this.articleService.getAllArticlesByTag(tagId);
  }
}
