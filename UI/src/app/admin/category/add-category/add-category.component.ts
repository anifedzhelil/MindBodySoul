import { Component, OnDestroy } from '@angular/core';
import { NgForm } from '@angular/forms';
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
  previewUrl: string | null = null;
  file?: File;
  invalid: boolean = false;

  ngOnInit(): void {}

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

  onFormSubmit(form: NgForm) {
    if (form.invalid) {
      this.invalid = true;
      return;
    }
    this.invalid = false;

    if (this.file) {
      this.cloudinaryService
        .uploadImage(this.file)
        .subscribe((response: any) => {
          this.model.image = response.secure_url;
          this.model.urlHandle = 'urlHandle';

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
    if (element.files && element.files.length > 0) {
      this.file = element.files?.[0];
      this.previewUrl = URL.createObjectURL(this.file);
    } else {
      this.file = undefined;
    }
  }
}
