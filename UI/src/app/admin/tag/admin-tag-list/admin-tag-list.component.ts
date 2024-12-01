import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Tag } from 'src/app/models/tag/tag.model';
import { LoaderService } from 'src/app/services/loader/loader.service';
import { TagService } from 'src/app/services/tag/tag.service';
import { faPen } from '@fortawesome/free-solid-svg-icons';
import { faTrashCan, faPenToSquare } from '@fortawesome/free-regular-svg-icons';

@Component({
  selector: 'app-admin-tag-list',
  templateUrl: './admin-tag-list.component.html',
  styleUrls: ['./admin-tag-list.component.css'],
})
export class AdminTagListComponent {
  tags: Tag[] = [];
  tagId: string = '';

  errorMessage: string = '';

  hideAddForm: boolean = false;
  hideUpdateForm: boolean = false;
  hideMessageError: boolean = false;
  hideDeleteConformation: boolean = false;

  faPen = faPen;
  faPenToSquare = faPenToSquare;
  faTrashCan = faTrashCan;

  constructor(
    private tagService: TagService,
    private loaderService: LoaderService
  ) {}

  ngOnInit(): void {
    this.loadTags();
  }

  addTag(): void {
    this.hideAddForm = true;
  }

  onCancel(): void {
    this.hideAddForm = false;
  }
  onSubmit(): void {
    this.loadTags();
  }

  loadTags(): void {
    this.loaderService.showLoader();
    this.tagService.getAllTags().subscribe({
      next: (tags) => {
        this.tags = tags;
        this.loaderService.hideLoader();
        this.hideAddForm = false;
        this.hideUpdateForm = false;
      },
    });
  }

  handleDelete(): void {
    this.hideDeleteConformation = false;

    if (this.tagId) {
      this.tagService.deleteTag(this.tagId).subscribe({
        next: (response) => {
          this.loadTags();
        },
        error: (err) => {
          if (err.status === 400) {
            console.log(err.error);
            this.hideMessageError = true;
            this.errorMessage = err.error;
          }
        },
      });
    }
  }

  onDelete(id: string): void {
    this.tagId = id;
    this.hideDeleteConformation = true;
  }

  handleCancel(): void {
    this.hideDeleteConformation = false;
  }

  onUpdateSubmit(): void {
    this.loadTags();
  }

  onUpdate(tagId: string): void {
    this.tagId = tagId;
    this.hideUpdateForm = true;
  }

  onUpdateCancel(): void {
    this.hideUpdateForm = false;
  }
}
