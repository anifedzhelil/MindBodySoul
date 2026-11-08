import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { Category } from 'src/app/models/category/category.model';
import { AddSubCategoryRequest } from 'src/app/models/subcategory/add-subcategory-request.model';
import { CategoryService } from 'src/app/services/categories/category.service';
import { IconsService } from 'src/app/services/icons/icon-service.service';
import { SubCategoryService } from 'src/app/services/subcategories/subcategory.service';

@Component({
  selector: 'app-add-subcategory',
  templateUrl: './add-subcategory.component.html',
  styleUrls: ['./add-subcategory.component.css'],
  standalone: false
})
export class AddSubCategoryComponent implements OnInit {
  categories$?: Observable<Category[]>;
  invalid: boolean = false;
  errorMessage = '';
  model: AddSubCategoryRequest = {
    name: '',
    categoryId: '',
    urlHandle: '',
    icon: '',
  };

  constructor(
    private categoryService: CategoryService,
    private subCategoryService: SubCategoryService,
    private router: Router,
  ) { }

  ngOnInit(): void {
    this.categories$ = this.categoryService.getAllCategories();
  }

  onFormSubmit(form: NgForm): void {
    if (form.invalid || !this.model.icon) {
      this.invalid = true;
      return;
    }
    this.invalid = false;

    this.subCategoryService.addSubCategory(this.model).subscribe({
      next: () => {
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
