import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Category } from 'src/app/models/category/category.model';
import { SubCategory } from 'src/app/models/subcategory/subcategory.model';
import { UpdateSubCategoryRequest } from 'src/app/models/subcategory/update-subcategory-request.model';
import { CategoryService } from 'src/app/services/categories/category.service';
import { IconsService } from 'src/app/services/icons/icon-service.service';
import { SubCategoryService } from 'src/app/services/subcategories/subcategory.service';

@Component({
  selector: 'app-edit-subcategory',
  templateUrl: './edit-subcategory.component.html',
  styleUrls: ['./edit-subcategory.component.css'],
  standalone: false
})
export class EditSubCategoryComponent implements OnInit {
  subCategory?: SubCategory;
  categories$?: Observable<Category[]>;

  id: string | null = null;
  isLoading = true;
  errorMessage = '';
  iconsKeys = this.iconsService.getIconKeys();

  constructor(
    private categoryService: CategoryService,
    private subCategoryService: SubCategoryService,
    private router: Router,
    private route: ActivatedRoute,
    private iconsService: IconsService
  ) { }

  ngOnInit(): void {
    this.categories$ = this.categoryService.getAllCategories();
    this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');
        if (this.id != null) {
          this.subCategoryService.getSubCategoryById(this.id).subscribe({
            next: (response) => {
              this.subCategory = response;
              this.isLoading = false;
            },
            error: () => {
              this.isLoading = false;
            },
          });
        }
      },
    });
  }

  onFormSubmit(form: NgForm): void {
    if (form.invalid) {
      return;
    }

    const updateSubCategoryRequest: UpdateSubCategoryRequest = {
      name: this.subCategory?.name ?? '',
      UrlHandle: this.subCategory?.urlHandle ?? '',
      icon: this.subCategory?.icon ?? '',
      categoryId: this.subCategory?.categoryId ?? '',
    };

    if (this.id) {
      this.subCategoryService
        .updateSubCategory(this.id, updateSubCategoryRequest)
        .subscribe({
          next: (response) => {
            this.router.navigateByUrl('/admin/subcategories');
          },
          error: (err) => {
            if (err.status === 404) {
              this.errorMessage = 'Подкатегорията не е намерена!';
            } else if (err.status === 409) {
              this.errorMessage = err.error;
            } else {
              this.errorMessage = 'Възникна грешка. Опитайте отново.';
            }
          },
        });
    }
  }
}
