import { Component, Input } from '@angular/core';
import { ArticleList } from 'src/app/models/article/article-list-response.model';

@Component({
    selector: 'app-articles',
    templateUrl: './articles.component.html',
    styleUrls: ['./articles.component.css'],
    standalone: false
})
export class ArticlesComponent {
  @Input() articles: ArticleList[] | null = null;

}
