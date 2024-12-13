import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddArticleComponent } from './add-article/add-article.component';
import { authGuard } from '../core/guards/auth.guard';
import { ArticleDetailsComponent } from './article-details/article-details.component';
import { ArticlesListComponent } from './articles-list/articles-list.component';

const routes: Routes = [
  {
    path: 'articles',
    component: ArticlesListComponent,
  },
  {
    path: 'articles/:id',
    component: ArticleDetailsComponent,
  },

  {
    path: 'add-article',
    component: AddArticleComponent,
    canActivate: [authGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ArticleRoutingModule {}
