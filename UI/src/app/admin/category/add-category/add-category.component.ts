import { Component, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AddCategoryRequest } from 'src/app/models/category/add-category-request.model';
import { CategoryService } from 'src/app/services/categories/category.service';

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.css'],
})
export class AddCategoryComponent implements OnDestroy {
  model: AddCategoryRequest;

  private addCategorySubscrision?: Subscription;

  ngOnInit(): void {
console.log("add category");
  }
  constructor(
    private categoryService: CategoryService,
    private router: Router
  ) {
    this.model = {
      name: '',
      urlHandle: '',
      image: '',
    };
  }
  ngOnDestroy(): void {
    this.addCategorySubscrision?.unsubscribe();
  }


  onFormSubmit() {
    this.addCategorySubscrision = this.categoryService
      .addCategory(this.model)
      .subscribe({
        next: (response) => {
          this.router.navigateByUrl('/admin');
        },
        error: (err) => {
          console.log(err);
        },
      });
  }
}
