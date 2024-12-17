import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { SubCategoriesListComponent } from './subcategories-list/subcategories-list.component';
import { CategoriesListComponent } from './categories-list/categories-list.component';

const routes: Routes = [
  {
    path: '',
    component: CategoriesListComponent,
  },
  {
    path: ':categoryId',
    component: SubCategoriesListComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CategoriesRoutingModule {}
