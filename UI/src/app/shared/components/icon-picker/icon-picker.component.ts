import { Component, Output, EventEmitter, Input } from '@angular/core';
import { IconDefinition, faXmark } from '@fortawesome/free-solid-svg-icons';
import { IconsService } from 'src/app/services/icons/icon-service.service';

@Component({
  selector: 'app-icon-picker',
  templateUrl: './icon-picker.component.html',
  styleUrls: ['./icon-picker.component.css'],
  standalone: false,
})
export class IconPickerComponent {
  @Output() iconSelected = new EventEmitter<string>();
  @Input() set initialIcon(value: string) {
    if (value) this.selectedIcon = value;
  }
  selectedIcon: string = '';
  searchTerm: string = '';
  isOpen: boolean = false;

  constructor(private iconsService: IconsService) {}

  icons = this.iconsService.getIcons();

  get filteredKeys(): string[] {
    const term = this.searchTerm.toLowerCase();
    return Object.keys(this.icons).filter((k) =>
      k.toLowerCase().includes(term),
    );
  }

  getIcon(iconKey: string): IconDefinition {
    return this.icons[iconKey as keyof typeof this.icons] || faXmark;
  }

  selectIcon(key: string): void {
    this.selectedIcon = key;
    this.iconSelected.emit(key);
    this.isOpen = false;
    this.searchTerm = '';
  }

  toggleDropdown(): void {
    this.isOpen = !this.isOpen;
  }
}
