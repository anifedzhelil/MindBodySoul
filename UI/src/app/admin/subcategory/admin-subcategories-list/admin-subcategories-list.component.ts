import { Component } from '@angular/core';
import { faPen } from '@fortawesome/free-solid-svg-icons';
import { faTrashCan, faPenToSquare } from '@fortawesome/free-regular-svg-icons';
import { SubCategoryService } from 'src/app/services/subcategories/subcategory.service';
import { Router } from '@angular/router';
import { SubCategory } from 'src/app/models/subcategory/subcategory.model';
import { Observable } from 'rxjs';
import { LoaderService } from 'src/app/services/loader/loader.service';

@Component({
    selector: 'app-admin-subcategories-list',
    templateUrl: './admin-subcategories-list.component.html',
    styleUrls: ['./admin-subcategories-list.component.css'],
    standalone: false
})
export class AdminSubCategoriesListComponent {
  subCategories: SubCategory[] = [];
  subCategoryId: string | undefined;

  faPen = faPen;
  faPenToSquare = faPenToSquare;
  faTrashCan = faTrashCan;

  hideDeleteConformation: boolean = false;
  hideMessageError: boolean = false;
  errorMessage: string = '';
  
  constructor(
    private subCategoryService: SubCategoryService,
    private router: Router,
    private loaderService: LoaderService
  ) {}

  ngOnInit(): void {
    this.loadSubCategories();
  }

  addSubCategory(): void {
    this.router.navigateByUrl('/admin/add-subcategory');
  }

  onDeleteSubcategory(subCategoryId: string) {
    this.subCategoryId = subCategoryId;
    this.hideDeleteConformation = true;
  }

  handleCancel(): void {
    this.hideDeleteConformation = false;
  }

  handleDelete(): void {
    this.hideDeleteConformation = false;

    if (this.subCategoryId) {
      this.subCategoryService.deleteSubCategory(this.subCategoryId).subscribe({
        next: (response) => {
          this.loadSubCategories();
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

  loadSubCategories(): void{
    this.loaderService.showLoader();
    this.subCategoryService.getAllSubCategories().subscribe({
      next: (subCategories) => {
        this.subCategories = subCategories;
        this.loaderService.hideLoader();
      },
    });
  }

  handleClose(): void {
    this.hideMessageError = false;
  }
}
