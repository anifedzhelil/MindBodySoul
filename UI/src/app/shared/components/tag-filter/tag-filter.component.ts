import {
  Component,
  Input,
  Output,
  EventEmitter,
  SimpleChanges,
  HostListener,
  OnInit,
} from '@angular/core';
import { faSearch } from '@fortawesome/free-solid-svg-icons';
import { TagsWithArticleCount } from 'src/app/models/tag/tags-with-article-count';

@Component({
  selector: 'app-tag-filter',
  templateUrl: './tag-filter.component.html',
  styleUrl: './tag-filter.component.css',
  standalone: false,
})
export class TagFilterComponent implements OnInit {
  @Input() tags: TagsWithArticleCount[] = [];
  filteredTags: TagsWithArticleCount[] = [];
  faSearch = faSearch;
  @Input() activeTagId: string | null = null;
  @Output() tagSelected = new EventEmitter<string>();
  isOpen = true;
  isAllTagsShown = false;
  isSearching = false;
  visibleTagsCount = 10;
  innerWidth = window.innerWidth;

  @HostListener('window:resize')
  onResize() {
    if (this.innerWidth != window.innerWidth) {
      this.updateIsOpen();
      this.innerWidth = window.innerWidth;
    }
  }

  ngOnInit(): void {
    this.updateIsOpen();
    console.log(window.innerWidth, this.isOpen);
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['tags']) {
      this.filteredTags = [...this.tags,].slice(0, this.visibleTagsCount);
    }
  }

  updateIsOpen() {
    this.isOpen = window.innerWidth > 800;
  }

  toggleAccordion() {
    this.isOpen = !this.isOpen;
  }

  filterTags(tag: string, event?: Event) {
    if (tag.trim()) {
      this.filteredTags = this.tags.filter((t) => t.name.includes(tag));
      this.isSearching = true;
    }
    else {
      this.filteredTags = [...this.tags].slice(0, this.visibleTagsCount);
      this.isAllTagsShown = false;
      this.isSearching = false;
    }
  }

  showAllTags() {
    this.isAllTagsShown = true;
    this.filteredTags = [...this.tags];
  }

  showLessTags() {
    this.isAllTagsShown = false;
    this.filteredTags = [...this.tags].slice(0, this.visibleTagsCount);
  }

  selectNewTag(tagId: string) {
    this.tagSelected.emit(tagId);
  }
}
