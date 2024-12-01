import {
  Component,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
} from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Tag } from 'src/app/models/tag/tag.model';
import { TagService } from 'src/app/services/tag/tag.service';

@Component({
  selector: 'app-edit-tag',
  templateUrl: './edit-tag.component.html',
  styleUrls: ['./edit-tag.component.css'],
})
export class EditTagComponent implements OnChanges {
  errorMessage: string = '';
  @Input() isHidden!: boolean;
  @Input() tagId: string = '';

  @Output() show = new EventEmitter<void>();
  @Output() submit = new EventEmitter<void>();
  @Output() cancel = new EventEmitter<void>();

  private editTagSubscription?: Subscription;

  model: Tag = {
    name: '',
    id: '',
  };

  constructor(private tagService: TagService, private router: Router) {}

  ngOnChanges(): void {
    this.tagService.getTagById(this.tagId).subscribe({
      next: (tag) => {
        this.model = tag;
        this.errorMessage = '';
      },
    });
  }

  onSubmit(): void {
    this.editTagSubscription = this.tagService
      .updateTag(this.tagId, this.model)
      .subscribe({
        next: () => {
          this.submit.emit();
        },
        error: (err) => {
          if (err.status == 400) {
            this.errorMessage = err.error;
          }
        },
      });
  }

  onCancel(): void {
    this.cancel.emit();
  }
}
