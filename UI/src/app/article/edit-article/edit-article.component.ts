import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Select2UpdateEvent } from 'ng-select2-component';
import { Editor } from 'ngx-editor';
import { UpdateArticleRequest } from 'src/app/models/article/update-article-request.model';
import { Category } from 'src/app/models/category/category.model';
import { SubCategoryList } from 'src/app/models/subcategory/subcategory-list.model';
import { AdminArticleService } from 'src/app/services/article/admin-article.service';
import { CategoryService } from 'src/app/services/categories/category.service';
import { CloudinaryService } from 'src/app/services/cloudinary/cloudinary.service';
import { SubCategoryService } from 'src/app/services/subcategories/subcategory.service';
import { TagService } from 'src/app/services/tag/tag.service';
import { faXmark } from '@fortawesome/free-solid-svg-icons';
import { ValidationErrorService } from 'src/app/services/validation-error/validation-error.service';
import { SlugService } from 'src/app/services/slug/slug.service';

@Component({
  selector: 'app-edit-article',
  templateUrl: './edit-article.component.html',
  styleUrls: ['./edit-article.component.css'],
  standalone: false
})
export class EditArticleComponent implements OnInit {
  id: string | null = null;
  selectedCategoryId: string | null = null;
  selectedSubCategoryId: string | null = null;
  previewUrl: string | null = null;
  errorMessage: string = '';
  isFormSubmitted: boolean = false;

  article: UpdateArticleRequest | undefined;

  editor: Editor = new Editor();
  selectedFile: File | null = null;

  tags: any[] = [];
  deletedTags: string[] = [];
  updatedTags: any[] = [];
  categories: Category[] = [];
  subCategories: SubCategoryList[] = [];
  isSlugChanged: boolean = false;

  faXmark = faXmark;

  constructor(
    private adminArticleService: AdminArticleService,
    private categoryService: CategoryService,
    private subCategoryService: SubCategoryService,
    private tagService: TagService,
    private cloudinaryService: CloudinaryService,
    private router: Router,
    private errorService: ValidationErrorService,
    private slugService: SlugService,
    private route: ActivatedRoute,
  ) { }

  titleOnInput(): void {
    if (this.article && !this.isSlugChanged)
      this.article.slug = this.slugService.toSlug(this.article.title);
  }

  slugOnInput(): void {
    if (!this.article) {
      return;
    }

    if (this.article.slug === '') {
      this.isSlugChanged = false;
    }
    else
      this.isSlugChanged = true;
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');
        if (this.id != null) {
          this.adminArticleService.getArticleForEdit(this.id).subscribe({
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
    this.isFormSubmitted = true;

    if (form.invalid) {
      Object.keys(form.controls).forEach((field) => {
        const control = form.controls[field];
        if (control.invalid) {
          console.log(`Полето "${field}" е невалидно.`);
          console.log('Грешки:', control.errors);
        }
      });

      this.errorMessage = 'Попълнете всички задължитени полета!';
      return;
    }
    else if (this.selectedFile) {
      this.cloudinaryService
        .uploadImage(this.selectedFile)
        .subscribe({
          next: (response: any) => {
            if (this.article) {
              this.article.imageUrl = response.secure_url;
              this.updateArticle();
            }
          },
          error: (err) => {
            this.errorMessage = "Снимката не беше качена";
          }
        }
        );
    } else {
      this.updateArticle();
    }
  }

  updateArticle(): void {
    if (this.article && this.id) {
      this.article.deletedTags = this.deletedTags;
      if (this.selectedSubCategoryId)
        this.article.subCategoryId = this.selectedSubCategoryId;
      this.adminArticleService.updateArticle(this.id, this.article).subscribe({
        next: (response) => {
          this.errorMessage = '';
          this.router.navigateByUrl('/articles');
        },
        error: (err) => {
          if (err.status === 409) {
            this.errorMessage = 'Този URL адрес вече е зает. Променете го.';
          }
          else
            this.errorMessage = this.errorService.getErrorMessage(err.error?.errors);
        },
      });
    }
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
    if (!this.deletedTags.includes(tagId)) {
      this.deletedTags.push(tagId);
    }

    //Logic to remove the tag
    if (this.article)
      this.article.tags = this.article.tags.filter((tag) => tag.id !== tagId);
  }
}
