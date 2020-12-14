import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { GridModel } from 'src/app/models/shared/grid.model';
import { PessoaFilterModel } from 'src/app/models/pessoa/pessoa-filter.model';
import { PessoaModel } from 'src/app/models/pessoa/pessoa.model';
import { MessageService } from 'src/app/services/message.service';
import { PessoaService } from 'src/app/services/pessoa.service';
import { FabMenuComponentEvents } from 'src/app/shared/components/fab-menu/fab-menu.events';
import { RequestSearchModel } from 'src/app/models/http/request-search.model';
import { PessoaDialogComponent } from './pessoa-dialog/pessoa-dialog.component';

@Component({
  selector: 'app-pessoa',
  templateUrl: './pessoa.component.html',
  styleUrls: ['./pessoa.component.scss']
})
export class PessoaComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  public filterModel: PessoaFilterModel;
  public isLoading: boolean = true;
  public dataSource: MatTableDataSource<PessoaModel>;
  public gridModel: GridModel<PessoaModel>;

  private menuEvent: Subscription;

  constructor(private pessoaService: PessoaService,
    public dialog: MatDialog,
    private menuComponentEvents: FabMenuComponentEvents,
    private messageService: MessageService) {
      this.menuEvent = this.menuComponentEvents.event$.subscribe(
        index => {
          if(+index == 2)
            this.getPessoas(true);
        }
      );
     }

  _iniciarElementos(){
    this.gridModel = new GridModel<PessoaModel>();
    this.gridModel.setColumns(['nome', 'telefone', 'email', 'acoes']);
    this.filterModel = new PessoaFilterModel();

    this.dataSource = new MatTableDataSource(this.gridModel.source);
    this.dataSource.paginator = this.paginator;
    this.isLoading = false;
  }

  ngOnInit(): void {
    this._iniciarElementos();

    this.getPessoas();
  }

  getPessoas(zerar: boolean = false){
    this.isLoading = true;

    if(zerar)
      this.paginator.pageIndex = 0;

    let requestSearch = new RequestSearchModel<PessoaFilterModel>(this.paginator.pageIndex, this.paginator.pageSize);

    requestSearch.search = this.filterModel;

    this.pessoaService.getPessoas(requestSearch).subscribe(response => {
      this.gridModel.setSource(response.data.items, response.data.total);
      this.dataSource.data = this.gridModel.source;
      this.isLoading = false;
    },
    err => {
      this.messageService.showError(err.message);
      this.isLoading = false;
    });
  }

  removerPessoa(pessoa: PessoaModel){
    this.isLoading = false;
    this.pessoaService.remover(pessoa.id).subscribe(response => {
      if(response.success){
        this.messageService.showSuccess('Pessoa removida com sucesso');
        this.getPessoas(true);
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

  editarPessoa(pessoa: PessoaModel){
    const dialogRef = this.dialog.open(PessoaDialogComponent, {
      width: '450px',
      data: { pessoaId:pessoa.id, pessoa: pessoa }
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result)
        this.getPessoas();
    });
  }
}
