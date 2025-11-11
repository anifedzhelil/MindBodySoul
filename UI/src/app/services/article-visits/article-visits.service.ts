import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ArticleVisitsService {
  constructor(private http: HttpClient) {}


  registerVisit(articleId: string): Observable<void> {
    var url =  `${environment.apiBaseUrl}/api/articlevisits/register/${articleId}` ;
    console.log("URL: " + url);
    return this.http.put<void>(
      `${environment.apiBaseUrl}/api/articlevisits/register/${articleId}?addAuth=true`, null
    );
  }

  test(): Observable<void> {
    debugger;
    console.log("TEST");
    return this.http.get<void>(
      `${environment.apiBaseUrl}/api/articlevisits/test`
    );
  }
}
