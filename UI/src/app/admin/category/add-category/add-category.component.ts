import { Component, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AddCategoryRequest } from 'src/app/models/category/add-category-request.model';
import { CategoryService } from 'src/app/services/categories/category.service';
import { CloudinaryService } from 'src/app/services/cloudinary/cloudinary.service';

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.css'],
})
export class AddCategoryComponent implements OnDestroy {
  model: AddCategoryRequest;

  private addCategorySubscrision?: Subscription;
  private file?: File;

  ngOnInit(): void {
    console.log('add category');
  }
  constructor(
    private categoryService: CategoryService,
    private cloudinaryService: CloudinaryService,
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
    if (this.file) {
      this.cloudinaryService
        .uploadImage(this.file)
        .subscribe((response: any) => {
          this.model.image = response.secure_url;

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
        });
    }
  }

  onFileUploadChange(event: Event): void {
    const element = event.currentTarget as HTMLInputElement;
    this.file = element.files?.[0];
  }
}
