import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Tag } from 'src/app/models/tag/tag.model';
import { TagService } from 'src/app/services/tag/tag.service';

@Component({
    selector: 'app-add-tag',
    templateUrl: './add-tag.component.html',
    styleUrls: ['./add-tag.component.css'],
    standalone: false
})
export class AddTagComponent {
  errorMessage: string = '';
  @Input() isHidden!: boolean;

  @Output() show = new EventEmitter<void>();
  @Output() submit = new EventEmitter<void>();
  @Output() cancel = new EventEmitter<void>();

  private addTagSubcrition?: Subscription;

  model: Tag = {
    name: '',
    id: '',
  };

  constructor(private tagService: TagService, private router: Router) {}

  onShow(): void {
    this.show.emit();
  }

  onCancel(): void {
    this.clearForm();
    this.cancel.emit();
  }

  onSubmit(): void {
    this.addTagSubcrition = this.tagService.addTag(this.model).subscribe({
      next: () => {
        this.submit.emit();
        this.clearForm();
      },
      error: (err) => {
        if (err.status == 400) {
          this.errorMessage = err.error;
        }
      },
    });
  }

  clearForm(): void {
    this.errorMessage = '';
    this.model.name = '';
  }
}
