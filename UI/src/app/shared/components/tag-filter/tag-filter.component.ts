import {
  Component,
  Input,
  Output,
  EventEmitter,
  SimpleChanges,
  HostListener,
} from '@angular/core';
import { Tag } from 'src/app/models/tag/tag.model';
import { faSearch } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-tag-filter',
  templateUrl: './tag-filter.component.html',
  styleUrl: './tag-filter.component.css',
  standalone: false,
})
export class TagFilterComponent {
  @Input() tags: Tag[] = [];
  filteredTags: Tag[] = [];
  faSearch = faSearch;
  @Input() activeTagId: string | null = null;
  @Output() tagSelected = new EventEmitter<string>();
  isOpen = true;
  isAllTagsShown = false;
  visibleTagsCount = 10;
  
  @HostListener('window:resize')
onResize() {
  this.isOpen = window.innerWidth > 768;
}

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['tags']) {
      this.filteredTags = [...this.tags, ].slice(0, this.visibleTagsCount);
    }
  }

  toggleAccordion() {
    this.isOpen = !this.isOpen;
  }

  filterTags(tag: string, event?: Event) {
    if(tag.trim()){
    this.filteredTags = this.tags.filter((t) => t.name.includes(tag));
    this.isAllTagsShown = true;
      alert('Моля, въведете текст за търсене на тагове.');

    }
    else
    {
      alert('Моля, въведете текст за търсене на тагове.');
       this.filteredTags = [...this.tags].slice(0, this.visibleTagsCount);
       this.isAllTagsShown = false;
    }
  }

  showAllTags() {
    this.isAllTagsShown = true;
    this.filteredTags = [...this.tags];
  }

  selectNewTag(tagId: string) {
    this.tagSelected.emit(tagId);
  }

}
