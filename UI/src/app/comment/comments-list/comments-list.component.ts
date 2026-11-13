import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ArticleComment } from 'src/app/models/comment/article-comment';
import { faTrashCan, faPenToSquare } from '@fortawesome/free-solid-svg-icons';
import { User } from 'src/app/models/user/user.module';
import { CommentService } from 'src/app/services/comment/comment.service';
import { UpdateCommentRequest } from 'src/app/models/comment/update-comment-request';
import { connect } from 'rxjs';

@Component({
  selector: 'app-comments-list',
  templateUrl: './comments-list.component.html',
  styleUrls: ['./comments-list.component.css'],
  standalone: false
})
export class CommentsListComponent {
  @Input() allComments: ArticleComment[] | null = null;
  @Input() user?: User;
  isPopupHidden: boolean = true;
  selectedId: string = '';
  errorMessage: string = '';
  isDeletePopupVisible = false;
  commentIdToDelete: string | null = null;

  faPenToSquare = faPenToSquare;
  faTrashCan = faTrashCan;

  @Output() submitUpdate = new EventEmitter<void>();
  @Output() commentUpdated = new EventEmitter<void>();

  constructor(private commentService: CommentService) { }

  showDeletePopup(commentId: string): void {
    this.commentIdToDelete = commentId;
    this.isDeletePopupVisible = true;
  }

  onDeleteCanceled(): void {
    this.commentIdToDelete = null;
    this.isDeletePopupVisible = false;
  }

  onDeleteConfirmed() {
    if (!this.commentIdToDelete) {
      return;
    }

    const commentId = this.commentIdToDelete;
    this.onDeleteCanceled();
    this.errorMessage = '';

    this.commentService.deleteComment(commentId).subscribe({
      next: () => {
        this.commentUpdated.emit();
      },
      error: (err) => {
        if (err.status === 404) {
          // Already deleted elsewhere, just reload the list
          this.commentUpdated.emit();
        } else {
          this.errorMessage = 'Възникна грешка при изтриването, опитайте отново.';
        }
      }
    });
  }

  selectedComment: ArticleComment | null = null;

  onPopupClosed() {
    this.isPopupHidden = true;
  }

  showEditPopup(comment: ArticleComment): void {
    this.selectedComment = { ...comment };
    this.isPopupHidden = false;
  }

  onCommentUpdated(updatedComment: ArticleComment): void {
    if (!updatedComment.content.trim()) {
      this.errorMessage = "Коментарът не може да е празен.";
      return;
    }

    const request: UpdateCommentRequest = {
      id: updatedComment.id,
      content: updatedComment.content,
    };

    if (updatedComment.content.trim()) {

    }

    this.commentService.updateComment(request).subscribe({
      next: () => {
        this.selectedComment = null;
        this.errorMessage = '';
        this.commentUpdated.emit();
        this.isPopupHidden = true;
      },
      error: (err) => {
        if (err.status === 401) {
          this.errorMessage = 'Сесията ви изтече, влезте отново.';
        } else if (err.status === 404) {
          this.errorMessage = 'Коментарът вече не съществува.';
          // The comment is gone, so close the popup and reload the list
          this.selectedComment = null;
          this.isPopupHidden = true;
          this.commentUpdated.emit();
        } else {
          this.errorMessage = 'Възникна грешка, опитайте отново.';
        }
      }
    });
  }
}
