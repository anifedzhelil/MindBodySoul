import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ArticleDetails } from 'src/app/models/article/article-details.model';
import { ArticleList } from 'src/app/models/article/article-list-response.model';
import { Article } from 'src/app/models/article/article.model';
import { LatestArticle } from 'src/app/models/article/latest-article.model';
import { UpdateArticleRequest } from 'src/app/models/article/update-article-request.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ArticleService {
  constructor(private http: HttpClient) { }

  getAllArticles(search?: string): Observable<ArticleList[]> {
    let params = new HttpParams();

    if (search?.trim() != null) {
      params = params.set("search", search.trim());
    }

    return this.http.get<ArticleList[]>(
      `${environment.apiBaseUrl}/api/articles/getAll`, { params }
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


  getLatestArticles(limit: number): Observable<LatestArticle[]> {
    let params = new HttpParams().set("limit", limit);

    return this.http.get<LatestArticle[]>(
      `${environment.apiBaseUrl}/api/articles/getLatestArticles`, {params}
    );
  }
  
  getArticleById(id: string): Observable<ArticleDetails> {
    return this.http.get<ArticleDetails>(
      `${environment.apiBaseUrl}/api/articles/${id}`
    );
  }
}
