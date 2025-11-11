import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AddArticleRequest } from 'src/app/models/article/add-article-request.mode';
import { ArticleDetails } from 'src/app/models/article/article-details.model';
import { ArticleList } from 'src/app/models/article/article-list-response.model';
import { Article } from 'src/app/models/article/article.model';
import { UpdateArticleRequest } from 'src/app/models/article/update-article-request.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ArticleService {
  constructor(private http: HttpClient) {}

  addArticle(model: AddArticleRequest): Observable<void> {
    return this.http.post<void>(
      `${environment.apiBaseUrl}/api/articles?addAuth=true`,
      model
    );
  }

  getAllArticles(): Observable<ArticleList[]> {
      return this.http.get<ArticleList[]>(
        `${environment.apiBaseUrl}/api/articles/getAll`
      );
  }

  getFilteredArticles(search: string): Observable<ArticleList[]> {
    return this.http.get<ArticleList[]>(
      `${environment.apiBaseUrl}/api/articles/getAll/${search}`
    );
  }

  getAllArticlesByCategory(categoryId: string): Observable<ArticleList[]> {
    return this.http.get<ArticleList[]>(
      `${environment.apiBaseUrl}/api/articles/byCategory/${categoryId}`
    );
  }

  getAllArticlesBySubCategory(
    subCategoryId: string
  ): Observable<ArticleList[]> {
    return this.http.get<ArticleList[]>(
      `${environment.apiBaseUrl}/api/articles/bySubCategory/${subCategoryId}`
    );
  }

  getAllArticlesByTag(tagId: string): Observable<ArticleList[]> {
    return this.http.get<ArticleList[]>(
      `${environment.apiBaseUrl}/api/articles/byTag/${tagId}`
    );
  }

  getArticleById(id: string): Observable<ArticleDetails> {
    return this.http.get<ArticleDetails>(
      `${environment.apiBaseUrl}/api/articles/${id}`
    );
  }

  updateArticle(id: string, model: UpdateArticleRequest) {
    return this.http.put<void>(
      `${environment.apiBaseUrl}/api/articles/${id}?addAuth=true`,
      model
    );
  }

  deleteArticle(id: string): Observable<Article> {
    return this.http.delete<Article>(
      `${environment.apiBaseUrl}/api/articles/${id}?addAuth=true`
    );
  }
}
