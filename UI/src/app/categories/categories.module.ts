import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CategoriesListComponent } from './categories-list/categories-list.component';
import { SubCategoriesListComponent } from './subcategories-list/subcategories-list.component';
import { CategoriesRoutingModule } from './categories-routing.module';
import { SharedModule } from '../shared/shared.module';
import { CoreModule } from '../core/core.module';



@NgModule({
  declarations: [
    CategoriesListComponent,
    SubCategoriesListComponent,
  ],
  imports: [
    CategoriesRoutingModule,
    CoreModule,
    SharedModule,
    CommonModule
  ]
})
export class CategoriesModule { }
