import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AddArticleRequest } from 'src/app/models/article/add-article-request.mode';
import { ArticleDetails } from 'src/app/models/article/article-details.model';
import { ArticleListResponse } from 'src/app/models/article/article-list-response.model';
import { environment } from 'src/environments/environment.development';

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

  getAllArticles(): Observable<ArticleListResponse[]> {
    return this.http.get<ArticleListResponse[]>(
      `${environment.apiBaseUrl}/api/articles`
    );
  }

  getArticleById(id: string): Observable<ArticleDetails> {
    debugger;
    return this.http.get<ArticleDetails>(
      `${environment.apiBaseUrl}/api/articles/${id}`,
    );
  }
}
