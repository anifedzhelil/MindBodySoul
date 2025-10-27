import { Component, EventEmitter, Input, Output } from '@angular/core';
import { faTriangleExclamation } from '@fortawesome/free-solid-svg-icons';

@Component({
    selector: 'app-error-message',
    templateUrl: './error-message.component.html',
    styleUrls: ['./error-message.component.css'],
    standalone: false
})
export class ErrorMessageComponent {
  @Input() isHidden!: boolean;
  @Input() errorMessage!: string;

  @Output() close = new EventEmitter<void>(); 

  faTriangleExclamation = faTriangleExclamation;

  onClose(): void{
    this.close.emit();
  }
}
