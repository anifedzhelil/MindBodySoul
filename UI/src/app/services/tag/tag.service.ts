import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Tag } from 'src/app/models/tag/tag.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class TagService {
  constructor(private http: HttpClient) {}

  getAllTags(): Observable<Tag[]> {
    return this.http.get<Tag[]>(`${environment.apiBaseUrl}/api/Tags`);
  }

  addTag(model: Tag): Observable<void>{
    return this.http.post<void>(
      `${environment.apiBaseUrl}/api/tags?addAuth=true`, model
    );
  }

  getTagById(id: string): Observable<Tag> {
    return this.http.get<Tag>(
      `${environment.apiBaseUrl}/api/tags/${id}`
    );
  }

  updateTag(id: string, model: Tag): Observable<Tag>{
    return this.http.put<Tag>(
      `${environment.apiBaseUrl}/api/tags/${id}?addAuth=true`,  model
    )
  }

  deleteTag(id: string): Observable<Tag>{
    return this.http.delete<Tag>(
      `${environment.apiBaseUrl}/api/tags/${id}?addAuth=true`
    );
  }
}
