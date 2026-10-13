import { Component, OnInit } from '@angular/core';
import { ErrorNotificationService } from './services/error-notification/error-notification.service';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'],
    standalone: false
})
export class AppComponent implements OnInit {
  hideMessageError: boolean = false;
  errorMessage: string = '';

  constructor(private errorService: ErrorNotificationService) {}
  
  ngOnInit(): void {
    this.errorService.message$.subscribe(
      {
        next: (message) => {
          this.hideMessageError = true;
          this.errorMessage = message;
        } 
      }
    );

  }

  title = 'MindBodySoul';

  handleClose(): void {
    this.hideMessageError = false;
  }
}
