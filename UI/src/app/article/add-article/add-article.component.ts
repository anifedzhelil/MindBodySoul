import { Component, OnDestroy, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { Select2UpdateEvent } from 'ng-select2-component';
import { Editor } from 'ngx-editor';
import { AddArticleRequest } from 'src/app/models/article/add-article-request.mode';
import { Category } from 'src/app/models/category/category.model';
import { SubCategoryList } from 'src/app/models/subcategory/subcategory-list.model';
import { AdminArticleService } from 'src/app/services/article/admin-article.service';
import { CategoryService } from 'src/app/services/categories/category.service';
import { CloudinaryService } from 'src/app/services/cloudinary/cloudinary.service';
import { SlugService } from 'src/app/services/slug/slug.service';
import { SubCategoryService } from 'src/app/services/subcategories/subcategory.service';
import { TagService } from 'src/app/services/tag/tag.service';
import { ValidationErrorService } from 'src/app/services/validation-error/validation-error.service';

@Component({
  selector: 'app-add-article',
  templateUrl: './add-article.component.html',
  styleUrls: ['./add-article.component.css'],
  standalone: false
})
export class AddArticleComponent implements OnInit, OnDestroy {
  previewUrl: string | null = null;
  errorMessage: string = '';
  isFormSubmited: boolean = false;

  article: AddArticleRequest = {
    title: '',
    slug: '',
    subCategoryId: '',
    content: '',
    imageUrl: '',
    tagsIDs: [],
    isPublished: false,
  };

  editor: Editor = new Editor();
  selectedFile: File | null = null;

  tags: any[] = [];
  categories: Category[] = [];
  subCategories: SubCategoryList[] = [];
  isSlugChanged: boolean = false;

  constructor(
    private adminArticleService: AdminArticleService,
    private categoryService: CategoryService,
    private subCategoryService: SubCategoryService,
    private tagService: TagService,
    private cloudinaryService: CloudinaryService,
    private errorService: ValidationErrorService,
    private slugService: SlugService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.loadTags();
    this.loadCategories();
  }

  titleOnblur(): void {
    if (!this.isSlugChanged)
      this.article.slug = this.slugService.toSlug(this.article.title);
  }

  slugOnInput(): void {
    if (this.article.slug === '') {
      this.isSlugChanged = false;
    }
    else
      this.isSlugChanged = true;
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

  loadCategories(): void {
    this.categoryService.getAllCategories().subscribe({
      next: (response) => {
        this.categories = response;
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
          },
        });
    } else {
      this.subCategories = [];
    }
  }

  onFileUploadChange(event: Event): void {
    const input = event.target as HTMLInputElement;

    if (this.previewUrl) {
      URL.revokeObjectURL(this.previewUrl);
    }

    if (input.files && input.files.length > 0) {
      this.selectedFile = input.files[0];
      this.previewUrl = URL.createObjectURL(this.selectedFile);
    } else {
      this.selectedFile = null;
      this.previewUrl = null;
    }
  }

  ngOnDestroy(): void {
    this.editor.destroy();
    if (this.previewUrl) {
      URL.revokeObjectURL(this.previewUrl);
    }
  }

  addArticleSubmit(form: NgForm): void {
    this.isFormSubmited = true;

    if (form.invalid) {
      this.errorMessage = 'Попълнете всички задължитени полета!';
      return;
    } else if (this.selectedFile) {
      this.errorMessage = '';

      this.cloudinaryService
        .uploadImage(this.selectedFile)
        .subscribe({
          next: (response: any) => {

            this.article.imageUrl = response.secure_url;
            this.adminArticleService.addArticle(this.article).subscribe({
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
          },
          error: (err) => {
            this.errorMessage = "Снимката не беше качена";
          }
        });
    }
  }

  onTagsUpdate(event: Select2UpdateEvent): void {
    this.article.tagsIDs = event.options.map((option) => String(option.value));
  }
}
