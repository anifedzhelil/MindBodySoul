import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddCategoryComponent } from './category/add-category/add-category.component';
import { EditCategoryComponent } from './category/edit-category/edit-category.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { AdminCategoriesListComponent } from './category/admin-categories-list/admin-categories-list.component';

const adminRoutes: Routes = [
  {
    path: 'admin',
    component: AdminDashboardComponent, // Admin Dashboard as the main component for admin area
    //  canActivate: [AdminGuard], // Protect the entire admin route with AdminGuard
    children: [
      { path: '', component: AdminCategoriesListComponent }, // Admin-only categories management
      { path: 'add-category', component: AddCategoryComponent }, // Admin-only categories management
      { path: 'edit-category/:id', component: EditCategoryComponent }, // Admin-only categories management
      //{ path: 'subcategories', component: AdminSubcategoriesListComponent }, // Admin-only subcategories management
      // Add more admin routes here as needed
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(adminRoutes)], // Use forChild to register the child routes for the admin module
  exports: [RouterModule],
})
export class AdminRoutingModule {}

