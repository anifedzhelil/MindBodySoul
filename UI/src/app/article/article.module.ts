import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddArticleComponent } from './add-article/add-article.component';
import { FormsModule } from '@angular/forms';
import { ArticleRoutingModule } from './article-routing.module';



@NgModule({
  declarations: [
    AddArticleComponent
  ],
  imports: [
    CommonModule,
    ArticleRoutingModule,
    FormsModule,
  ]
})
export class ArticleModule { }
