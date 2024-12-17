import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Category } from 'src/app/models/category/category.model';
import { CategoryService } from 'src/app/services/categories/category.service';
import { faPen } from '@fortawesome/free-solid-svg-icons';
import { faTrashCan, faPenToSquare } from '@fortawesome/free-regular-svg-icons';
import { LoaderService } from 'src/app/services/loader/loader.service';

@Component({
  selector: 'app-admin-categories-list',
  templateUrl: './admin-categories-list.component.html',
  styleUrls: ['./admin-categories-list.component.css'],
})
export class AdminCategoriesListComponent {
  categories: Category[] = [];
  hideDeleteConformation: boolean = false;
  hideMessageError: boolean = false;
  errorMessage: string = '';
  categoryId: string | undefined;

  faPen = faPen;
  faPenToSquare = faPenToSquare;
  faTrashCan = faTrashCan;

  constructor(
    private categoryService: CategoryService,
    private loaderService: LoaderService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadCategories();
  }

  addCategory(): void {
    this.router.navigateByUrl('/admin/add-category');
  }

  onDelete(id: string): void {
    this.categoryId = id;
    this.hideDeleteConformation = true;
  }

  handleDelete(): void {
    this.hideDeleteConformation = false;

    if (this.categoryId) {
      this.categoryService.deleteCategory(this.categoryId).subscribe({
        next: (response) => {
          this.loadCategories();
        },
        error: (err) => {
          if (err.status === 400) {
            console.log(err.error);
            this.hideMessageError = true;
            this.errorMessage = err.error;
          }
        },
      });
    }
  }

  handleCancel(): void {
    this.hideDeleteConformation = false;
  }

  loadCategories(): void {
    this.loaderService.showLoader();
      this.categoryService.getAllCategories().subscribe({
        next: (categories) => {
          this.categories = categories;
          this.loaderService.hideLoader();
        },
      });
  }

  handleClose(): void {
    this.hideMessageError = false;
  }
}
