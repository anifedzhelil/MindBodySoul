import { Component } from '@angular/core';
import { faPen } from '@fortawesome/free-solid-svg-icons';
import { faTrashCan, faPenToSquare } from '@fortawesome/free-regular-svg-icons';
import { SubCategoryService } from 'src/app/services/subcategory.service';
import { Router } from '@angular/router';
import { SubCategory } from 'src/app/models/subcategory/subcategory.model';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-admin-subcategories-list',
  templateUrl: './admin-subcategories-list.component.html',
  styleUrls: ['./admin-subcategories-list.component.css'],
})
export class AdminSubCategoriesListComponent {
  subCategories$?: Observable<SubCategory[]>;
  faPen = faPen; // Solid icon
  faPenToSquare = faPenToSquare; // Regular icon
  faTrashCan = faTrashCan;

  constructor(
    private subCategoryService: SubCategoryService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.subCategories$ = this.subCategoryService.getAllSubCategories();
  }

  addSubCategory(): void {
    this.router.navigateByUrl('/admin/add-subcategory');
  }
}
