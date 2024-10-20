import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Category } from 'src/app/models/category/category.model';
import { CategoryService } from 'src/app/services/categories/category.service';
import {  faPen } from '@fortawesome/free-solid-svg-icons';
import { faTrashCan, faPenToSquare } from '@fortawesome/free-regular-svg-icons';

@Component({
  selector: 'app-admin-categories-list',
  templateUrl: './admin-categories-list.component.html',
  styleUrls: ['./admin-categories-list.component.css']
})
export class AdminCategoriesListComponent {
  categories$?: Observable<Category[]>;

  faPen = faPen; // Solid icon
  faPenToSquare = faPenToSquare; // Regular icon
  faTrashCan = faTrashCan;
  
  constructor(
    private categoryService: CategoryService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.categories$ = 
    this.categoryService.getAllCategories();
  }

  addCategory(): void {
    
    this.router.navigateByUrl('/admin/add-category');
  }
}
