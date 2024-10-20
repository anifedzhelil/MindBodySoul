import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Category } from 'src/app/models/category/category.model';
import { UpdateCategoryRequest } from 'src/app/models/category/update-category-request.model';
import { CategoryService } from 'src/app/services/categories/category.service';

@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrls: ['./edit-category.component.css']
})
export class EditCategoryComponent implements OnInit{
  category?: Category;
  editCategorySubscription?: Subscription;
  id: string | null = null;

  constructor(   private route: ActivatedRoute,
    private categoryService: CategoryService,
    private router: Router)
    {}

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');
        if (this.id != null) {
          this.categoryService.getCategoryById(this.id).subscribe({
            next: (response) => {
              this.category = response;
            },
          });
        }
      },
    });
  }

  onFormSubmit(): void {
    const updateCategoryRequest: UpdateCategoryRequest = {
      name: this.category?.name ?? '',
      UrlHandle: this.category?.urlHandle ?? '',
      image: ''
    };

    if (this.id) {
      this.editCategorySubscription = this.categoryService
        .updateCategory(this.id, updateCategoryRequest)
        .subscribe({
          next: (response) => {
            this.router.navigateByUrl('/admin/categories');
          },
        });
    }
  }

}
