import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddArticleComponent } from './add-article/add-article.component';
import { FormsModule } from '@angular/forms';
import { ArticleRoutingModule } from './article-routing.module';
import { NgxEditorModule } from 'ngx-editor';
import { Select2Module } from 'ng-select2-component';
import { OverlayModule } from '@angular/cdk/overlay';
import { ArticlesListComponent } from './articles-list/articles-list.component';
import { ArticleDetailsComponent } from './article-details/article-details.component';
import { RouterModule } from '@angular/router';
import { SharedModule } from "../shared/shared.module";
import { EditArticleComponent } from './edit-article/edit-article.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';


@NgModule({
  declarations: [
    AddArticleComponent,
    ArticlesListComponent,
    ArticleDetailsComponent,
    EditArticleComponent
  ],
  
  imports: [
    CommonModule,
    ArticleRoutingModule,
    FormsModule,
    NgxEditorModule,
    Select2Module,
    OverlayModule,
    FontAwesomeModule,
    SharedModule
]
})
export class ArticleModule { }
