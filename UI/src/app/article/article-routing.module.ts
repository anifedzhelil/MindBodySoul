import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddArticleComponent } from './add-article/add-article.component';
import { authGuard } from '../core/guards/auth.guard';
import { ArticleDetailsComponent } from './article-details/article-details.component';
import { ArticlesListComponent } from './articles-list/articles-list.component';
import { EditArticleComponent } from './edit-article/edit-article.component';

const routes: Routes = [
  {
    path: '',
    component: ArticlesListComponent,
  },
  {
    path: 'search/:search',
    component: ArticlesListComponent,
  },
  {
    path: 'articles-details/:id',
    component: ArticleDetailsComponent,
  },
  {
    path: 'byTag/:tagId',
    component: ArticlesListComponent,
  },
  {
    path: 'add-article',
    component: AddArticleComponent,
    canActivate: [authGuard],
  },
  {
    path: 'edit-article/:id',
    component: EditArticleComponent,
    canActivate: [authGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ArticleRoutingModule {}
