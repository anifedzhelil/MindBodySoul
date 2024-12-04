import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddArticleComponent } from './add-article/add-article.component';
import { authGuard } from '../core/guards/auth.guard';


const routes: Routes = [
  {
    path: 'add-article',
    component: AddArticleComponent,
    canActivate: [authGuard],
  }
];


@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ArticleRoutingModule { }
