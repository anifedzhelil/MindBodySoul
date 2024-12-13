import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DeleteConformationComponent } from './components/delete-conformation/delete-conformation.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ErrorMessageComponent } from './components/error-message/error-message.component';
import { AwesomeIconsComponent } from './components/awesome-icons/awesome-icons.component';
import { SpinnerComponent } from './components/spinner/spinner/spinner.component';
import { PasswordValidatorDirective } from './validators/password/password-validator.directive';
import { SlicesPipe } from './pipes/slices.pipe';
import { ElapsedTimePipe } from './pipes/elapsed-time.pipe';

@NgModule({
  declarations: [
    DeleteConformationComponent,
    ErrorMessageComponent,
    AwesomeIconsComponent,
    SpinnerComponent,
    PasswordValidatorDirective,
    SlicesPipe,
    ElapsedTimePipe,
  ],
  imports: [CommonModule, FontAwesomeModule],
  exports: [
    DeleteConformationComponent,
    ErrorMessageComponent,
    AwesomeIconsComponent,
    SpinnerComponent,
    PasswordValidatorDirective,
    SlicesPipe,
    ElapsedTimePipe
  ],
})
export class SharedModule {}
