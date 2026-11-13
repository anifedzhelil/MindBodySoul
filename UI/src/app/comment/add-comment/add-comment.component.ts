import { Component, EventEmitter, Input, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AddCommentRequest } from 'src/app/models/comment/add-comment-request.model';
import { CommentService } from 'src/app/services/comment/comment.service';
import { User } from 'src/app/models/user/user.module';

@Component({
  selector: 'app-add-comment',
  templateUrl: './add-comment.component.html',
  styleUrls: ['./add-comment.component.css'],
  standalone: false
})
export class AddCommentComponent {
  comment: string = '';
  @Input() articleId: string | null = null;
  @Input() user?: User;
  errorMessage: string = '';
  @Output() commentAdded = new EventEmitter<void>();

  constructor(
    private commentService: CommentService,
  ) { }

  onFormSubmit(form: NgForm): void {
    if (this.user && this.articleId) {
      if (form.invalid) {
        form.control.markAllAsTouched();
        return;
      }
      else if (!this.comment.trim()) {
        this.errorMessage = "Коментарът не може да е празен.";
        return;
      }
      var request: AddCommentRequest = {
        articleId: this.articleId,
        content: this.comment,
      };

      this.commentService.addComment(request).subscribe({
        next: () => {
          this.errorMessage = '';
          form.resetForm();
          this.commentAdded.emit();
        },
        error: (err) => {
          if (err.status === 404) {
            this.errorMessage = 'Статията не е намерана';
          }
        }
      });
    } else {
      this.errorMessage = 'Само регистрирани потребители могат да кометират!';
    }
  }
}
