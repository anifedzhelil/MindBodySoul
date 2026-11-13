import { Component, EventEmitter, Input, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ArticleComment } from 'src/app/models/comment/article-comment';

@Component({
  selector: 'app-edit-comment',
  templateUrl: './edit-comment.component.html',
  styleUrls: ['./edit-comment.component.css'],
  standalone: false
})
export class EditCommentComponent {
  // @Input() currentComment:  | undefined;
  @Input() isHidden: boolean = true;
  @Input() errorMessage = '';
  @Input() comment!: ArticleComment;

  @Output() saved = new EventEmitter<ArticleComment>();
  @Output() cancel = new EventEmitter<void>();


  hide(): void {
    this.isHidden = true;
  }

  onSaved(form: NgForm): void {
    this.saved.emit(this.comment);
  }

  onCancel(): void {
    this.cancel.emit();
    this.hide();
  }
}
