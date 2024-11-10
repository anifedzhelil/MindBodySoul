import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SubCategory } from '../models/subcategory/subcategory.model';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { AddSubCategoryRequest } from '../models/subcategory/add-subcategory-request.model';

@Injectable({
  providedIn: 'root',
})
export class SubCategoryService {
  constructor(private http: HttpClient) {}

  getAllCategories(): Observable<SubCategory[]> {
    return this.http.get<SubCategory[]>(
      `${environment.apiBaseUrl}/api/SubCategories`
    );
  }

  
  addCategory(model: AddSubCategoryRequest): Observable<void> {
    return this.http.post<void>(
      `${environment.apiBaseUrl}/api/subcategories`,
      model
    );
  }
}
