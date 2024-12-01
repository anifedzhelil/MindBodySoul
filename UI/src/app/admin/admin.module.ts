import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { AddCategoryComponent } from './category/add-category/add-category.component';
import { EditCategoryComponent } from './category/edit-category/edit-category.component';
import { AdminCategoriesListComponent } from './category/admin-categories-list/admin-categories-list.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { FormsModule } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { AddSubCategoryComponent } from './subcategory/add-subcategory/add-subcategory.component';
import { AdminSubCategoriesListComponent } from './subcategory/admin-subcategories-list/admin-subcategories-list.component';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { EditSubCategoryComponent } from './subcategory/edit-subcategory/edit-subcategory.component';
import { CoreModule } from '../core/core.module';
import { AddTagComponent } from './tag/add-tag/add-tag.component';
import { AdminTagListComponent } from './tag/admin-tag-list/admin-tag-list.component';
import { EditTagComponent } from './tag/edit-tag/edit-tag.component';

@NgModule({
  declarations: [
    AddCategoryComponent,
    EditCategoryComponent,
    AdminDashboardComponent,
    AdminCategoriesListComponent,
    AddSubCategoryComponent,
    AdminSubCategoriesListComponent,
    EditSubCategoryComponent,
    AddTagComponent,
    AdminTagListComponent,
    EditTagComponent,
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    FontAwesomeModule, // Ensure this is added to the imports array
    RouterModule,
    FormsModule,
    SharedModule,
    CoreModule,
  ],
})
export class AdminModule {}
