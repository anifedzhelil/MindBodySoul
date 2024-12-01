import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { Category } from 'src/app/models/category/category.model';
import { AddSubCategoryRequest } from 'src/app/models/subcategory/add-subcategory-request.model';
import { CategoryService } from 'src/app/services/categories/category.service';
import { SubCategoryService } from 'src/app/services/subcategories/subcategory.service';

@Component({
  selector: 'app-add-subcategory',
  templateUrl: './add-subcategory.component.html',
  styleUrls: ['./add-subcategory.component.css'],
})
export class AddSubCategoryComponent implements OnInit {
  categories$?: Observable<Category[]>;
  private addSubCategorySubscrision?: Subscription;

  model: AddSubCategoryRequest = {
    name: '',
    categoryId: '',
    urlHandle: '',
    icon: '',
  };

  constructor(
    private categoryService: CategoryService,
    private subCategoryService: SubCategoryService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.categories$ = this.categoryService.getAllCategories();
  }
  onFormSubmit(): void {
    this.addSubCategorySubscrision = this.subCategoryService
      .addCategory(this.model)
      .subscribe({
        next: () => {
          this.router.navigateByUrl('/admin/subcategories');
        },
        error: (err) => {
          console.log(err);
        },
      });
  }
}
