import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Select2UpdateEvent } from 'ng-select2-component';
import { Editor } from 'ngx-editor';
import { UpdateArticleRequest } from 'src/app/models/article/update-article-request.model';
import { Category } from 'src/app/models/category/category.model';
import { SubCategoryList } from 'src/app/models/subcategory/subcategory-list.model';
import { ArticleService } from 'src/app/services/article/article.service';
import { AuthService } from 'src/app/services/auth/auth.service';
import { CategoryService } from 'src/app/services/categories/category.service';
import { CloudinaryService } from 'src/app/services/cloudinary/cloudinary.service';
import { SubCategoryService } from 'src/app/services/subcategories/subcategory.service';
import { TagService } from 'src/app/services/tag/tag.service';
import { faXmark } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-edit-article',
  templateUrl: './edit-article.component.html',
  styleUrls: ['./edit-article.component.css'],
})
export class EditArticleComponent implements OnInit {
  id: string | null = null;
  selectedCategoryId: string | null = null;
  selectedSubCategoryId: string | null = null;
  previewUrl: string | null = null;
  errorMessage: string = '';

  article: UpdateArticleRequest | undefined;

  editor: Editor = new Editor();
  selectedFile: File | null = null;

  tags: any[] = [];
  deletedTags: string[] = [];
  updatedTags: any[] = [];
  categories: Category[] = [];
  subCategories: SubCategoryList[] = [];

  faXmark = faXmark;

  constructor(
    private articleService: ArticleService,
    private categoryService: CategoryService,
    private subCategoryService: SubCategoryService,
    private tagService: TagService,
    private cloudinaryService: CloudinaryService,
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');
        if (this.id != null) {
          this.articleService.getArticleById(this.id).subscribe({
            next: (response) => {
              this.article = response;

              this.selectedCategoryId = response.categoryId;
              this.loadCategories();

              this.loadSubCategories(this.selectedCategoryId);
              this.selectedSubCategoryId = response.subCategoryId;

              this.loadTags();
            },
            error: (err) => {
              console.error(err);
            },
          });
        }
      },
    });
  }

  onCategoryChange(event: Event): void {
    const categoryId = (event.target as HTMLSelectElement).value;

    if (categoryId) {
      this.subCategoryService
        .getSubCategoriesByCategoryId(categoryId)
        .subscribe({
          next: (response) => {
            this.subCategories = response;
            this.selectedSubCategoryId = '';
          },
        });
    } else {
      this.subCategories = [];
    }
  }

  onFileUploadChange(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.selectedFile = input.files[0];
      this.previewUrl = URL.createObjectURL(this.selectedFile);
    } else {
      this.selectedFile = null;
    }
  }

  /* onTagsUpdate(event: Select2UpdateEvent): void {
    if (this.article) {
      this.tags = [];
      event.options.map((option) => {
        this.tags.push({option});
      });
    }
  }*/

  onTagsUpdate(event: Select2UpdateEvent): void {
    if (this.article) {
      this.article.tagsIDs = [];
    }

    event.options.map((option) => {
      if (this.article) {
        this.article.tagsIDs?.push(String(option.value));
      }
    });
  }

  updateArticleSubmit(form: NgForm): void {
    console.log(this.article)
  }

  loadSubCategories(categorId: string): void {
    this.subCategoryService.getSubCategoriesByCategoryId(categorId).subscribe({
      next: (response) => {
        this.subCategories = response;

        if (this.selectedSubCategoryId) {
          const exists = this.subCategories.some(
            (subCategory) => subCategory.id === this.selectedSubCategoryId
          );
          if (!exists) {
            this.selectedSubCategoryId = null;
          }
        }
      },
    });
  }

  loadCategories(): void {
    this.categoryService.getAllCategories().subscribe({
      next: (response) => {
        this.categories = response;
      },
    });
  }

  loadTags(): void {
    this.tagService.getAllTags().subscribe({
      next: (response) => {
        this.tags = response.map((tag) => ({
          value: tag.id,
          label: tag.name,
        }));
      },
    });
  }

  removeTag(tagId: string) {
    this.deletedTags.push(tagId);
    //Logic to remove the tag
    if (this.article)
      this.article.tags = this.article.tags.filter((tag) => tag.id !== tagId);
  }
}
