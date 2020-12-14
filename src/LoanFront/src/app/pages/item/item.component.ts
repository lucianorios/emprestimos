import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { GridModel } from 'src/app/models/shared/grid.model';
import { ItemFilterModel } from 'src/app/models/item/item-filter.model';
import { ItemModel } from 'src/app/models/item/item.model';
import { ItemService } from 'src/app/services/item.service';
import { MessageService } from 'src/app/services/message.service';
import { FabMenuComponentEvents } from 'src/app/shared/components/fab-menu/fab-menu.events';
import { RequestSearchModel } from 'src/app/models/http/request-search.model';
import { ItemDialogComponent } from './item-dialog/item-dialog.component';

@Component({
  selector: 'app-item',
  templateUrl: './item.component.html',
  styleUrls: ['./item.component.scss']
})
export class ItemComponent implements OnInit {

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  public dataSource: MatTableDataSource<ItemModel>;
  public gridModel: GridModel<ItemModel>;
  public isLoading: boolean = true;

  private menuEvent: Subscription;

  constructor(private itemService: ItemService,
    public dialog: MatDialog,
    private menuComponentEvents: FabMenuComponentEvents,
    private messageService: MessageService) {
      this.menuEvent = this.menuComponentEvents.event$.subscribe(
        index => {
          if(+index == 1)
            this.getItens(true);
        }
      );
    }

  ngOnInit(): void {
    this._iniciarElementos();

    this.getItens();
  }

  _iniciarElementos(){
    this.gridModel = new GridModel<ItemModel>();
    this.gridModel.setColumns(['nome', 'acoes']);

    this.dataSource = new MatTableDataSource(this.gridModel.source);
    this.dataSource.paginator = this.paginator;
    this.isLoading = false;
  }

  getItens(zerar: boolean = false){
    this.isLoading = true;

    if(zerar)
      this.paginator.pageIndex = 0;

    let requestSearch = new RequestSearchModel<ItemFilterModel>(this.paginator.pageIndex, this.paginator.pageSize);

    requestSearch.search = new ItemFilterModel;

    this.itemService.getItens(requestSearch).subscribe(response => {
      this.gridModel.setSource(response.data.items, response.data.total);
      this.dataSource.data = this.gridModel.source;
      this.isLoading = false;
    },
    err => {
      this.messageService.showError(err.message);
      this.isLoading = false;
    });
  }

  removerItem(item: ItemModel){
    this.isLoading = false;
    this.itemService.remover(item.id).subscribe(response => {
      if(response.success){
        this.messageService.showSuccess('Item removido com sucesso');
        this.getItens(true);
      } else {
        this.messageService.showError(response.messages.join('<br>'));
      }
      this.isLoading = false;
    },
    err => {
      this.messageService.showError(err.message);
      this.isLoading = false;
    });
  }

  editarItem(item: ItemModel){
    const dialogRef = this.dialog.open(ItemDialogComponent, {
      width: '450px',
      data: { itemId:item.id, item: item }
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result)
        this.getItens();
    });
  }

}
