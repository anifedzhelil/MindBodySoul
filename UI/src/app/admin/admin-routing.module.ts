import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddCategoryComponent } from './category/add-category/add-category.component';
import { EditCategoryComponent } from './category/edit-category/edit-category.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { AdminCategoriesListComponent } from './category/admin-categories-list/admin-categories-list.component';
import { AdminSubCategoriesListComponent } from './subcategory/admin-subcategories-list/admin-subcategories-list.component';
import { AddSubCategoryComponent } from './subcategory/add-subcategory/add-subcategory.component';
import { EditSubCategoryComponent } from './subcategory/edit-subcategory/edit-subcategory.component';

const adminRoutes: Routes = [
  {
    path: 'admin',
    component: AdminDashboardComponent, 
    children: [
      { path: '', component: AdminCategoriesListComponent }, 
      { path: 'categories', component: AdminCategoriesListComponent }, 
      { path: 'add-category', component: AddCategoryComponent }, 
      { path: 'add-subcategory', component: AddSubCategoryComponent }, 
      { path: 'edit-category/:id', component: EditCategoryComponent },
      { path: 'edit-subcategory/:id', component: EditSubCategoryComponent },
      { path: 'subcategories', component: AdminSubCategoriesListComponent },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(adminRoutes)], 
  exports: [RouterModule],
})
export class AdminRoutingModule {}
