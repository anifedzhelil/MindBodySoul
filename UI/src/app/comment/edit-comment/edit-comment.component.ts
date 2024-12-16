import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ArticleComment } from 'src/app/models/comment/article-comment';

@Component({
  selector: 'app-edit-comment',
  templateUrl: './edit-comment.component.html',
  styleUrls: ['./edit-comment.component.css'],
})
export class EditCommentComponent {
  // @Input() currentComment:  | undefined;
  @Input() isHidden: boolean = true;

  @Input() comment!: ArticleComment;

  @Output() submit = new EventEmitter<ArticleComment>();
  @Output() cancel = new EventEmitter<void>();

  hide(): void {
    this.isHidden = true;
  }

  onSubmit(): void {
    this.submit.emit(this.comment);
  }

  onCancel(): void {
    this.cancel.emit();
    this.hide();
  }
}
