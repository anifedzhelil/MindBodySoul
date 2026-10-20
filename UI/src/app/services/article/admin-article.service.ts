import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AddArticleRequest } from 'src/app/models/article/add-article-request.mode';
import { ArticleEdit } from 'src/app/models/article/article-edit.model';
import { Article } from 'src/app/models/article/article.model';
import { UpdateArticleRequest } from 'src/app/models/article/update-article-request.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AdminArticleService {

  constructor(private http: HttpClient) { }

  getArticleForEdit(id: string): Observable<ArticleEdit> {
    return this.http.get<ArticleEdit>(
      `${environment.apiBaseUrl}/api/adminarticles/${id}?addAuth=true`
    );
  }

  addArticle(model: AddArticleRequest): Observable<void> {
    return this.http.post<void>(
      `${environment.apiBaseUrl}/api/adminarticles?addAuth=true`,
      model
    );
  }

  archiveArticle(id: string, isArchived: boolean) {
    return this.http.put<void>(
      `${environment.apiBaseUrl}/api/adminarticles/archive/${id}?addAuth=true`,
      isArchived
    );
  }

  updateArticle(id: string, model: UpdateArticleRequest) {
    return this.http.put<void>(
      `${environment.apiBaseUrl}/api/adminarticles/${id}?addAuth=true`,
      model
    );
  }

  deleteArticle(id: string): Observable<Article> {
    return this.http.delete<Article>(
      `${environment.apiBaseUrl}/api/adminarticles/${id}?addAuth=true`
    );
  }

}
