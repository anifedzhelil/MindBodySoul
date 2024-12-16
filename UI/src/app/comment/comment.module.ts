import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddCommentComponent } from './add-comment/add-comment.component';
import { CommentsListComponent } from './comments-list/comments-list.component';
import { FormsModule } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { EditCommentComponent } from './edit-comment/edit-comment.component';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [AddCommentComponent, CommentsListComponent, EditCommentComponent],
  imports: [
    CommonModule,
    FormsModule,
    FontAwesomeModule,
    SharedModule
  ],
  exports: [AddCommentComponent, CommentsListComponent]
})
export class CommentModule { }
