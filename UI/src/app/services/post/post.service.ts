import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PostList } from 'src/app/models/instagram/post/post-list.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  constructor(private http: HttpClient) { }

  getAllPosts(search?: string): Observable<PostList[]> {
    return this.http.get<PostList[]>(
      `${environment.apiBaseUrl}/api/posts/getAllPosts/?addAuth=true`,  {
    params: search ? { search } : {}
    });
  }

}
