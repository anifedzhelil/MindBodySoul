import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DeleteConformationComponent } from './components/delete-conformation/delete-conformation.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ErrorMessageComponent } from './components/error-message/error-message.component';
import { AwesomeIconsComponent } from './components/awesome-icons/awesome-icons.component';
import { SpinnerComponent } from './components/spinner/spinner/spinner.component';

@NgModule({
  declarations: [
    DeleteConformationComponent,
    ErrorMessageComponent,
    AwesomeIconsComponent,
    SpinnerComponent,
  ],
  imports: [CommonModule, FontAwesomeModule],
  exports: [
    DeleteConformationComponent,
    ErrorMessageComponent,
    AwesomeIconsComponent,
    SpinnerComponent
  ],
})
export class SharedModule {}
