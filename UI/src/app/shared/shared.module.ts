import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DeleteConformationComponent } from './components/delete-conformation/delete-conformation.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ErrorMessageComponent } from './components/error-message/error-message.component';



@NgModule({
  declarations: [DeleteConformationComponent, ErrorMessageComponent],
  imports: [
    CommonModule,
    FontAwesomeModule
  ],
  exports: [DeleteConformationComponent, ErrorMessageComponent]
})
export class SharedModule { }
