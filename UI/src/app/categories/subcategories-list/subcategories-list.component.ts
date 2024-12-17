import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { ArticleList } from 'src/app/models/article/article-list-response.model';
import { Article } from 'src/app/models/article/article.model';
import { SubCategory } from 'src/app/models/subcategory/subcategory.model';
import { ArticleService } from 'src/app/services/article/article.service';
import { SubCategoryService } from 'src/app/services/subcategories/subcategory.service';

@Component({
  selector: 'app-subcategories-list',
  templateUrl: './subcategories-list.component.html',
  styleUrls: ['./subcategories-list.component.css'],
})
export class SubCategoriesListComponent {
  subCategories: SubCategory[] | undefined;
  articles$: Observable<ArticleList[]> = new Observable<ArticleList[]>();

  categoryId: string | null = null;
  selectedSubCategory: boolean = false;

  selectedArticle: string | null = null;
  emptyList: boolean = true;
  constructor(
    private subCategoryService: SubCategoryService,
    private articleServie: ArticleService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (params) => {
        this.categoryId = params.get('categoryId');
        if (this.categoryId)
          this.subCategoryService
            .getSubCategoriesByCategoryId(this.categoryId)
            .subscribe({
              next: (subCategories) => {
                this.subCategories = subCategories;
                if (subCategories.length > 0) {
                  this.emptyList = false;
                }
              },
            });

        this.loadArticles();
      },
    });
  }

  loadArticles(): void {
    if (this.categoryId)
      this.articles$ = this.articleServie.getAllArticlesByCategory(
        this.categoryId
      );
  }

  selectSubCategory(subCategoryId: string): void {
    this.selectedSubCategory = true;
    this.articles$ =
      this.articleServie.getAllArticlesBySubCategory(subCategoryId);
  }
}
