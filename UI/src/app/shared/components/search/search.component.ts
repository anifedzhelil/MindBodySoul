import { Component, EventEmitter, Input, Output } from '@angular/core';
import { faSearch } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrl: './search.component.css',
  standalone: false
})
export class SearchComponent {
  @Output() searchSubmitted = new EventEmitter<string>();
  @Input() placeholder = 'Търсене...';

  faSearch = faSearch;

  onSearch(value: string) {
    this.searchSubmitted.emit(value);
  }

  onInput(value: string) {
    if (value === '') {
      this.searchSubmitted.emit(value);
    }
  }
}
