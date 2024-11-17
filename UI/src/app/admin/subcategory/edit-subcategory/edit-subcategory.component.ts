import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { Category } from 'src/app/models/category/category.model';
import { SubCategory } from 'src/app/models/subcategory/subcategory.model';
import { UpdateSubCategoryRequest } from 'src/app/models/subcategory/update-subcategory-request.model';
import { CategoryService } from 'src/app/services/categories/category.service';
import { SubCategoryService } from 'src/app/services/subcategories/subcategory.service';

@Component({
  selector: 'app-edit-subcategory',
  templateUrl: './edit-subcategory.component.html',
  styleUrls: ['./edit-subcategory.component.css'],
})
export class EditSubCategoryComponent implements OnInit {
  subCategory?: SubCategory;
  categories$?: Observable<Category[]>;

  editSubCategorySubscription?: Subscription;
  id: string | null = null;

  constructor(
    private categoryService: CategoryService,
    private subCategoryService: SubCategoryService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.categories$ = this.categoryService.getAllCategories();
    this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');
        if (this.id != null) {
          this.subCategoryService.getSubCategoryById(this.id).subscribe({
            next: (response) => {
              this.subCategory = response;
            },
          });
        }
      },
    });
  }

  onFormSubmit(): void {
    const updateSubCategoryRequest: UpdateSubCategoryRequest = {
      name: this.subCategory?.name ?? '',
      UrlHandle: this.subCategory?.urlHandle ?? '',
      icon: this.subCategory?.icon ?? '',
      categoryId: this.subCategory?.categoryId ?? '',
    };

    if (this.id) {
      this.editSubCategorySubscription = this.subCategoryService
        .updateSubCategory(this.id, updateSubCategoryRequest)
        .subscribe({
          next: (response) => {
            this.router.navigateByUrl('/admin/subcategories');
          },
        });
    }
  }
}
