import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DeleteConformationComponent } from './components/delete-conformation/delete-conformation.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ErrorMessageComponent } from './components/error-message/error-message.component';
import { AwesomeIconsComponent } from './components/awesome-icons/awesome-icons.component';



@NgModule({
  declarations: [DeleteConformationComponent, ErrorMessageComponent, AwesomeIconsComponent],
  imports: [
    CommonModule,
    FontAwesomeModule
  ],
  exports: [DeleteConformationComponent, ErrorMessageComponent, AwesomeIconsComponent]
})
export class SharedModule { }
