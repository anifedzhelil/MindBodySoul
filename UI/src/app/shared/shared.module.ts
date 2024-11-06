import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DeleteConformationComponent } from './components/delete-conformation/delete-conformation.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';



@NgModule({
  declarations: [DeleteConformationComponent],
  imports: [
    CommonModule,
    FontAwesomeModule
  ],
  exports: [DeleteConformationComponent]
})
export class SharedModule { }
