import { Component, EventEmitter, Input, Output } from '@angular/core';
import { faTrashCan } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-delete-conformation',
  templateUrl: './delete-conformation.component.html',
  styleUrls: ['./delete-conformation.component.css']
})
export class DeleteConformationComponent {
@Input() isHidden!: boolean;

@Output() delete = new EventEmitter<void>();  
@Output() cancel = new EventEmitter<void>(); 

faTrashCan  = faTrashCan;

  onDeleteConfirm(): void{
    this.delete.emit(); 
  }

  onCancel(): void {
    this.cancel.emit(); 
  }

}
