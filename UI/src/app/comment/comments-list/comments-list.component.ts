import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ArticleComment } from 'src/app/models/comment/article-comment';
import { faTrashCan, faPenToSquare } from '@fortawesome/free-solid-svg-icons';
import { User } from 'src/app/models/user/user.module';
import { CommentService } from 'src/app/services/comment/comment.service';
import { UpdateCommentRequest } from 'src/app/models/comment/update-comment-request';

@Component({
  selector: 'app-comments-list',
  templateUrl: './comments-list.component.html',
  styleUrls: ['./comments-list.component.css'],
})
export class CommentsListComponent {
  @Input() allComments: ArticleComment[] | null = null;
  @Input() user?: User;
  isPopupHidden: boolean = true;
  selectedId: string = '';

  faPenToSquare = faPenToSquare;
  faTrashCan = faTrashCan;

  @Output() submitUpdate = new EventEmitter<void>();
  @Output() commentUpdated = new EventEmitter<void>();

  constructor(private commentService: CommentService) {}

  deleteComment(commentId: string) {
    
    this.commentService.deleteComment(commentId).subscribe({
      next: () => {
        this.commentUpdated.emit(); 
      },
      error: (err) => {
        console.error('Грешка при изтриване на коментара:', err);
      }
    });
  }

  selectedComment: ArticleComment | null = null;

  onPopupClosed() {
    this.isPopupHidden = true;
  }

  showEditPopup(commentId: string): void {
    this.commentService.getCommentById(commentId).subscribe({
      next: (response) => {
        this.selectedComment = response;
      },
      error: (err) => {
        console.error('Грешка при зареждане на коментара:', err);
      },
    });

    this.isPopupHidden = false;
  }
/*
  editComment(comment: ArticleComment): void {
    this.selectedComment = { ...comment }; 
  }
*/
  onCommentUpdated(updatedComment: ArticleComment): void {
    this.selectedComment = null; 
    const request: UpdateCommentRequest = {
      id: updatedComment.id,
      content: updatedComment.content,
      updatedDate: new Date(),
    };

    this.commentService.updateComment(request).subscribe({
      next: () => {
        this.commentUpdated.emit(); 
      },
      error: (err) => {
        console.error('Грешка при редактиране на коментара:', err);
      }
    });
  }
}
