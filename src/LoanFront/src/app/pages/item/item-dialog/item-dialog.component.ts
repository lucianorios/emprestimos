import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ItemModel } from 'src/app/models/item/item.model';
import { ItemService } from 'src/app/services/item.service';
import { MessageService } from 'src/app/services/message.service';

@Component({
  selector: 'app-item-dialog',
  templateUrl: './item-dialog.component.html',
  styleUrls: ['./item-dialog.component.scss']
})
export class ItemDialogComponent implements OnInit {

  public isLoading: boolean = false;
  public itemModel: ItemModel;

  constructor(private dialogRef: MatDialogRef<ItemDialogComponent>,
    private itemService: ItemService,
    private messageService: MessageService,
    @Inject(MAT_DIALOG_DATA) public data: { itemId: number, item: ItemModel }) { }

  ngOnInit(): void {
    this.itemModel = new ItemModel();
  }

  salvar(){
    if(this.itemModel.isValid()){
      this.isLoading = true;
      this.itemService.salvar(this.itemModel).subscribe(
        response => {
          if(response.success){
            this.messageService.showSuccess('Item cadastrado com sucesso');
            this.dialogRef.close(true);
          } else {
            this.messageService.showError(response.messages.join('<br>'));
          }
          this.isLoading = false;
        },
        err => {
          this.messageService.showError(err.message);
          this.isLoading = false;
        }
      );
    } else {
      this.messageService.showWarning(this.itemModel.notifications.join('<br>'));
      this.itemModel.clearNorifications();
    }
  }
}
