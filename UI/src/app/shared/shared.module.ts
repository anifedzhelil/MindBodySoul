import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DeleteConformationComponent } from './components/delete-conformation/delete-conformation.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ErrorMessageComponent } from './components/error-message/error-message.component';
import { AwesomeIconsComponent } from './components/awesome-icons/awesome-icons.component';
import { SpinnerComponent } from './components/spinner/spinner/spinner.component';
import { PasswordValidatorDirective } from './validators/password/password-validator.directive';
import { SlicesPipe } from './pipes/slices.pipe';
import { FormatTimePipe } from './pipes/format-time.pipe';
import { ElapsedTimePipe } from './pipes/elapsed-time.pipe';
import { RouterModule } from '@angular/router';
import {ArticlesComponent} from './components/articles/articles.component';
import { LoaderComponent } from './components/loader/loader.component';

@NgModule({
  declarations: [
    DeleteConformationComponent,
    ErrorMessageComponent,
    AwesomeIconsComponent,
    SpinnerComponent,
    PasswordValidatorDirective,
    SlicesPipe,
    FormatTimePipe,
    ElapsedTimePipe,
    ArticlesComponent,
    LoaderComponent
  ],
  imports: [CommonModule, FontAwesomeModule, RouterModule],
  exports: [
    DeleteConformationComponent,
    ErrorMessageComponent,
    AwesomeIconsComponent,
    SpinnerComponent,
    PasswordValidatorDirective,
    SlicesPipe,
    FormatTimePipe,
    ElapsedTimePipe,
    ArticlesComponent,
    LoaderComponent
  ],
})
export class SharedModule {}
