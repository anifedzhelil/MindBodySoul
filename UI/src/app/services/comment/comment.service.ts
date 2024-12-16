import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AddCommentRequest } from 'src/app/models/comment/add-comment-request.model';
import { ArticleComment } from 'src/app/models/comment/article-comment';
import { UpdateCommentRequest } from 'src/app/models/comment/update-comment-request';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class CommentService {
  constructor(private http: HttpClient) {}

  getAllComments(articleId: string): Observable<ArticleComment[]> {
    return this.http.get<ArticleComment[]>(
      `${environment.apiBaseUrl}/api/comments/allComments/${articleId}`
    );
  }

  addComment(request: AddCommentRequest): Observable<ArticleComment> {
    return this.http.post<ArticleComment>(
      `${environment.apiBaseUrl}/api/comments?addAuth=true`,
      request
    );
  }

  getCommentById(id: string): Observable<ArticleComment> {
    return this.http.get<ArticleComment>(
      `${environment.apiBaseUrl}/api/comments/${id}`
    );
  }

  updateComment(request: UpdateCommentRequest): Observable<ArticleComment> {
    return this.http.put<ArticleComment>(
      `${environment.apiBaseUrl}/api/comments?addAuth=true`,
      request
    );
  }

  deleteComment(id: string): Observable<Comment> {
    return this.http.delete<Comment>(
      `${environment.apiBaseUrl}/api/comments/${id}?addAuth=true`
    );
  }
}
