import { Component, OnInit, ViewChild } from '@angular/core';
import { EmprestimoModel } from 'src/app/models/emprestimo/emprestimo.model';
import { GridModel } from 'src/app/models/shared/grid.model';
import * as moment from 'moment';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { EmprestimoFilterModel } from 'src/app/models/emprestimo/emprestimo-filter.model';
import { RequestSearchModel } from 'src/app/models/http/request-search.model';
import { EmprestimoService } from 'src/app/services/emprestimo.service';
import { MessageService } from 'src/app/services/message.service';
import { MatDialog } from '@angular/material/dialog';
import { PessoaDialogComponent } from 'src/app/pages/pessoa/pessoa-dialog/pessoa-dialog.component';
import { PessoaModel } from 'src/app/models/pessoa/pessoa.model';
import { Subscription } from 'rxjs';
import { FabMenuComponentEvents } from 'src/app/shared/components/fab-menu/fab-menu.events';

moment.locale('pt-br');

@Component({
  selector: 'app-home',
  templateUrl: './emprestimo.component.html',
  styleUrls: ['./emprestimo.component.scss']
})
export class EmprestimoComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  private menuEvent: Subscription;

  public dataSource: MatTableDataSource<EmprestimoModel>;
  public gridModel: GridModel<EmprestimoModel>;
  public filterModel: EmprestimoFilterModel;
  public isLoading: boolean = true;

  constructor(private emprestimoService: EmprestimoService,
    private messageService: MessageService,
    private menuComponentEvents: FabMenuComponentEvents,
    public dialog: MatDialog){
      this.menuEvent = this.menuComponentEvents.event$.subscribe(
        index => {
          if(+index == 3)
            this.getEmprestimos(true);
        }
      );
    }

  _iniciarElementos(){
    this.gridModel = new GridModel<EmprestimoModel>();
    this.gridModel.setColumns(['pessoa', 'item', 'data', 'devolucao', 'tempo', 'acoes']);
    this.filterModel = new EmprestimoFilterModel();

    this.dataSource = new MatTableDataSource(this.gridModel.source);
    this.dataSource.paginator = this.paginator;
    this.isLoading = false;
  }

  ngOnInit(): void {
    this._iniciarElementos();

    this.getEmprestimos();
  }

  getEmprestimos(zerar: boolean = false){
    this.isLoading = true;

    if(zerar)
      this.paginator.pageIndex = 0;

    let requestSearch = new RequestSearchModel<EmprestimoFilterModel>(this.paginator.pageIndex, this.paginator.pageSize);

    requestSearch.search = this.filterModel;

    this.emprestimoService.getEmprestimos(requestSearch).subscribe(response => {
      this.gridModel.setSource(response.data.items, response.data.total);
      this.dataSource.data = this.gridModel.source;
      this.isLoading = false;
    },
    err => {
      this.messageService.showError(err.message);
      this.isLoading = false;
    });
  }

  openPessoaDialog(pessoa: PessoaModel): void {
    const dialogRef = this.dialog.open(PessoaDialogComponent, {
      width: '450px',
      data: {pessoaId: pessoa.id, pessoa: pessoa }
    });
  }

  finalizar(id: number){
    this.isLoading = true;
    this.emprestimoService.finalizar(id).subscribe(response => {
      this.isLoading = false;

      this.getEmprestimos();
    },
    err => {
      this.messageService.showError(err.message);
      this.isLoading = false;
    });
  }
}
