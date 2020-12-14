import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { empty } from 'rxjs/internal/observable/empty';
import { map, startWith, distinctUntilChanged, switchMap, tap } from 'rxjs/operators';
import { EmprestimoModel } from 'src/app/models/emprestimo/emprestimo.model';
import { ItemFilterModel } from 'src/app/models/item/item-filter.model';
import { ItemModel } from 'src/app/models/item/item.model';
import { PessoaFilterModel } from 'src/app/models/pessoa/pessoa-filter.model';
import { PessoaModel } from 'src/app/models/pessoa/pessoa.model';
import { EmprestimoService } from 'src/app/services/emprestimo.service';
import { ItemService } from 'src/app/services/item.service';
import { MessageService } from 'src/app/services/message.service';
import { PessoaService } from 'src/app/services/pessoa.service';
import { RequestSearchModel } from 'src/app/models/http/request-search.model';

@Component({
  selector: 'app-emprestimo-dialog',
  templateUrl: './emprestimo-dialog.component.html',
  styleUrls: ['./emprestimo-dialog.component.scss']
})
export class EmprestimoDialogComponent implements OnInit {

  public emprestimoModel: EmprestimoModel;
  public pessoasControl = new FormControl();
  public pessoasOptions: Observable<any[]>;
  public itensControl = new FormControl();
  public itensOptions: Observable<any[]>;

  public pessoaNome: string = "";
  public itemNome: string = "";

  public isLoading: boolean = false;

  constructor(private pessoaService: PessoaService,
    private messageService: MessageService,
    private emprestimoService: EmprestimoService,
    private dialogRef: MatDialogRef<EmprestimoDialogComponent>,
    private itemService: ItemService) { }

  ngOnInit(): void {
    this._iniciarElementos();

    this.pessoasOptions = this.pessoasControl.valueChanges.pipe(
      startWith(''),
      distinctUntilChanged(),
      switchMap(val => {
        return this._filtrarPessoas(val || '')
      })
    );

    this.itensOptions = this.itensControl.valueChanges.pipe(
      startWith(''),
      distinctUntilChanged(),
      switchMap(val => {
        return this._filtrarItens(val || '')
      })
    );
  }

  _iniciarElementos(){
    this.emprestimoModel = new EmprestimoModel();
  }

  _filtrarPessoas(key: string): Observable<any[]> {
    //this.emprestimoModel.pessoa = new PessoaModel();
    if (key.length >= 3) {
      let request = new RequestSearchModel<PessoaFilterModel>(0, 40);
      request.search = new PessoaFilterModel();
      request.search.nome = key;
      const pessoas = this.pessoaService.getPessoas(request)
        .pipe(map(response => { return response.data.items }));
      return pessoas;
    }
    else {
      return empty().pipe(tap(t => []))
    }
  }

  _filtrarItens(key: string): Observable<any[]> {
    //this.emprestimoModel.item = new ItemModel();
    if (key.length >= 1) {
      let request = new RequestSearchModel<ItemFilterModel>(0, 40);
      request.search = new ItemFilterModel();
      request.search.nome = key;
      request.search.disponivel = true;
      const pessoas = this.itemService.getItens(request)
        .pipe(map(response => { return response.data.items }));
      return pessoas;
    }
    else {
      return empty().pipe(tap(t => []))
    }
  }

  salvar(){
    if(this.emprestimoModel.isValid()){
      this.isLoading = true;
      this.emprestimoService.salvar(this.emprestimoModel).subscribe(
        response => {
          if(response.success){
            this.messageService.showSuccess('Empréstimo realizado com sucesso');
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
      this.messageService.showWarning(this.emprestimoModel.notifications.join('<br>'));
      this.emprestimoModel.clearNorifications();
    }
  }

  selecionarCliente($event){
    if($event.option?.value){
      const pessoa = $event.option.value as PessoaModel;
      this.pessoaNome = pessoa.nome;
      this.emprestimoModel.pessoa = Object.assign({}, pessoa);
    }
  }

  selecionarItem($event){
    if($event.option?.value){
      const item = $event.option.value as ItemModel;
      this.itemNome = item.nome;
      this.emprestimoModel.item = Object.assign({}, item);
    }
  }
}
