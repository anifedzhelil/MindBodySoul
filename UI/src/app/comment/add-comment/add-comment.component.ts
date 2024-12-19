import { Component, EventEmitter, Input, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AddCommentRequest } from 'src/app/models/comment/add-comment-request.model';
import { CommentService } from 'src/app/services/comment/comment.service';
import { User } from 'src/app/models/user/user.module';

@Component({
  selector: 'app-add-comment',
  templateUrl: './add-comment.component.html',
  styleUrls: ['./add-comment.component.css'],
})
export class AddCommentComponent {
  comment: string = '';
  @Input() articleId: string | null = null;
  @Input() user?: User;
  errorMessage: string = '';
  @Output() commentAdded = new EventEmitter<void>();

  constructor(
    private commentService: CommentService,
  ) {}

  onFormSubmit(form: NgForm): void {
    if (this.user && this.articleId) {
      if (form.invalid) {
        return;
      }
      var request: AddCommentRequest = {
        userId: this.user?.userId ? this.user.userId : '',
        articleId: this.articleId,
        content: this.comment,
        createdDate: new Date(new Date().getTime() + 2 * 60 * 60 * 1000)
      };

      this.commentService.addComment(request).subscribe({
        next: () => {
          this.comment = 'Добавете нов коментар...'; 
          this.commentAdded.emit(); 
        },
        error:(err) =>{
          console.error(err);
        }
      });
    } else {
      this.errorMessage = 'Само регистрирани потребители могат да кометират!';
    }
  }
}
