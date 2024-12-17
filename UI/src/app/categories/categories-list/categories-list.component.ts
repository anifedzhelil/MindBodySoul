import { Component, OnInit } from '@angular/core';
import { Category } from 'src/app/models/category/category.model';
import { CategoryService } from 'src/app/services/categories/category.service';
import { LoaderService } from 'src/app/services/loader/loader.service';

@Component({
  selector: 'app-categories-list',
  templateUrl: './categories-list.component.html',
  styleUrls: ['./categories-list.component.css']
})
export class CategoriesListComponent implements OnInit {
  categories: Category[] = [];

  constructor(private categoryService: CategoryService, private loaderService: LoaderService){}
  
  ngOnInit(): void {
    this.loaderService.showLoader();
    this.categoryService.getAllCategories().subscribe({
      next: (categories) => {
        this.categories = categories;
        this.loaderService.hideLoader();
      },
    });
  }

}


