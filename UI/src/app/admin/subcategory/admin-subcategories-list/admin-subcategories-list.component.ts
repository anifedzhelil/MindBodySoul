import { Component } from '@angular/core';
import {  faPen } from '@fortawesome/free-solid-svg-icons';
import { faTrashCan, faPenToSquare } from '@fortawesome/free-regular-svg-icons';

@Component({
  selector: 'app-admin-subcategories-list',
  templateUrl: './admin-subcategories-list.component.html',
  styleUrls: ['./admin-subcategories-list.component.css']
})
export class AdminSubcategoriesListComponent {
  faPen = faPen; // Solid icon
  faPenToSquare = faPenToSquare; // Regular icon
  faTrashCan = faTrashCan;
  
}
