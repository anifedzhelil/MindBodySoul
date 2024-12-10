import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Category } from 'src/app/models/category/category.model';
import { UpdateCategoryRequest } from 'src/app/models/category/update-category-request.model';
import { CategoryService } from 'src/app/services/categories/category.service';
import { CloudinaryService } from 'src/app/services/cloudinary/cloudinary.service';

@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrls: ['./edit-category.component.css'],
})
export class EditCategoryComponent implements OnInit {
  category: Category = {
    name: '',
    image: '',
    urlHandle: '',
    id: ' '
  };
  editCategorySubscription?: Subscription;
  id: string | null = null;
  previewUrl: string | null = null;
  file: File | null = null;
  categoryNotFound: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private categoryService: CategoryService,
    private cloudinaryService: CloudinaryService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');
        if (this.id != null) {
          this.categoryService.getCategoryById(this.id).subscribe({
            next: (response) => {
              this.category = response;
              this.categoryNotFound = false;
            },
            error: (err) =>{
              this.categoryNotFound = true;
            }
          });
        }
      },
    });

  }

  onFormSubmit(): void {
    if (this.previewUrl && this.file) {
      this.cloudinaryService.uploadImage(this.file).subscribe({
        next: (response: any) => {
          this.updateCategory(response.secure_url);
        },
      });
    } else {
      this.updateCategory(this.category?.image);
    }
  }

  updateCategory(imageUrl?: string) {
    const updateCategoryRequest: UpdateCategoryRequest = {
      name: this.category?.name ?? '',
      UrlHandle: this.category?.urlHandle ?? '',
      image: imageUrl ?? '',
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
  onFileSelected(event: Event): void {
    const fileInput = event.target as HTMLInputElement;
    if (fileInput.files && fileInput.files.length > 0) {
      this.file = fileInput.files[0];
      this.previewUrl = URL.createObjectURL(this.file);
    }
  }
}
