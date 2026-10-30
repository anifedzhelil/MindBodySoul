import { Component, forwardRef, Input } from '@angular/core';
import { ControlValueAccessor, NG_VALIDATORS, NG_VALUE_ACCESSOR } from '@angular/forms';
import { SlugService } from 'src/app/services/slug/slug.service';


@Component({
  selector: 'app-url-handle',
  templateUrl: './url-handle.component.html',
  styleUrl: './url-handle.component.css',
  standalone: false,
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => UrlHandleComponent),
    multi: true,
  }]
})
export class UrlHandleComponent implements ControlValueAccessor {
  value = '';
  private _title = '';
   onTouched: () => void = () => { };

  private onChange: (value: string) => void = () => { };

  constructor(private slugService: SlugService) { }

  private titleInitialized = false;

  @Input()
  set title(value: string) {
    if (!this.titleInitialized) {
      this._title = value;
      this.titleInitialized = true;
      return;
    }

    if (value === this._title) {
      return;
    }

    this._title = value;
    this.value = this.slugService.toSlug(value);
    this.onChange(this.value);
  }
  get title(): string {
    return this._title;
  }

  onInput(event: Event): void {
    const input = event.target as HTMLInputElement;
    this.value = input.value;
    this.onChange(this.value);
  }

  writeValue(obj: any): void {
    this.value = obj ?? '';
  }

  registerOnChange(fn: (value: string) => void): void {
    this.onChange = fn;
  }


  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  setDisabledState?(isDisabled: boolean): void {
  }
}