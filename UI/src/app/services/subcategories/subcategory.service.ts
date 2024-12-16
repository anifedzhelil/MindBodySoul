import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SubCategory } from '../../models/subcategory/subcategory.model';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { AddSubCategoryRequest } from '../../models/subcategory/add-subcategory-request.model';
import { UpdateSubCategoryRequest } from '../../models/subcategory/update-subcategory-request.model';

@Injectable({
  providedIn: 'root',
})
export class SubCategoryService {
  constructor(private http: HttpClient) {}

  getAllSubCategories(): Observable<SubCategory[]> {
    return this.http.get<SubCategory[]>(
      `${environment.apiBaseUrl}/api/subcategories`
    );
  }

  getSubCategoriesByCategoryId(categoryId: string): Observable<SubCategory[]> {
    return this.http.get<SubCategory[]>(
      `${environment.apiBaseUrl}/api/subcategories/Category/${categoryId}`
    );
  }

  getSubCategoryById(id: string): Observable<SubCategory> {
    return this.http.get<SubCategory>(
      `${environment.apiBaseUrl}/api/subcategories/${id}`
    );
  }

  addCategory(model: AddSubCategoryRequest): Observable<void> {
    return this.http.post<void>(
      `${environment.apiBaseUrl}/api/subcategories?addAuth=true`,
      model
    );
  }

  updateSubCategory(id: string, model: UpdateSubCategoryRequest) {
    return this.http.put<void>(
      `${environment.apiBaseUrl}/api/subcategories/${id}?addAuth=true`,
      model
    );
  }

  deleteSubCategory(id: string): Observable<SubCategory> {
    return this.http.delete<SubCategory>(
      `${environment.apiBaseUrl}/api/subcategories/${id}?addAuth=true`
    );
  }
}
