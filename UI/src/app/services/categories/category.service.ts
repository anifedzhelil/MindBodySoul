import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AddCategoryRequest } from 'src/app/models/category/add-category-request.model';
import { Category } from 'src/app/models/category/category.model';

import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { UpdateCategoryRequest } from 'src/app/models/category/update-category-request.model';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  constructor(private http: HttpClient) {}

  getAllCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(
      `${environment.apiBaseUrl}/api/Categories`
    );
  }

  addCategory(model: AddCategoryRequest): Observable<void> {
    return this.http.post<void>(
      `${environment.apiBaseUrl}/api/categories?addAuth=true`,
      model
    );
  }

  getCategoryById(id: string): Observable<Category> {
    return this.http.get<Category>(
      `${environment.apiBaseUrl}/api/categories/${id}`
    );
  }

  updateCategory(
    id: string,
    updateCategoryRequest: UpdateCategoryRequest
  ): Observable<Category> {
    return this.http.put<Category>(
      `${environment.apiBaseUrl}/api/categories/${id}?addAuth=true`,
      updateCategoryRequest
    );
  }

  deleteCategory(id: string): Observable<Category> {
    return this.http.delete<Category>(
      `${environment.apiBaseUrl}/api/categories/${id}?addAuth=true`
    );
  }
}
