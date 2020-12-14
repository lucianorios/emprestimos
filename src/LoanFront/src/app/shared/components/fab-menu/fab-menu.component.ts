import { MatFabMenu } from '@angular-material-extensions/fab-menu';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { EmprestimoDialogComponent } from 'src/app/pages/emprestimo/emprestimo-dialog/emprestimo-dialog.component';
import { ItemDialogComponent } from 'src/app/pages/item/item-dialog/item-dialog.component';
import { PessoaDialogComponent } from 'src/app/pages/pessoa/pessoa-dialog/pessoa-dialog.component';
import { FabMenuComponentEvents } from './fab-menu.events';

@Component({
  selector: 'fab-menu',
  templateUrl: './fab-menu.component.html',
  styleUrls: ['./fab-menu.component.scss']
})
export class FabMenuComponent implements OnInit {

  public fabButtonsRandom: MatFabMenu[] = []

  constructor(private router: Router,
    private menuComponentEvents: FabMenuComponentEvents,
    public dialog: MatDialog) { }

  ngOnInit(): void {
    this._initializeMenu();
  }

  _initializeMenu(){
    this.fabButtonsRandom  = [
      {
        id: 1,
        icon: 'videogame_asset',
        tooltip: 'Jogos'
      },
      {
        id: 2,
        icon: 'person',
        tooltip: 'Pessoas'
      },
      {
        id: 3,
        icon: 'menu_book',
        tooltip: 'Empréstimos'
      },
    ];
  }

  isLoginPage() {
    return this.router.url.match('^/login$');
  }

  onFabMenuItemSelected(event){
    switch(event){
      case 1: this.openItemDialog(); break; //this.router.navigate(['/jogo']); break;
      case 2: this.openPessoaDialog(); break; //this.router.navigate(['/pessoa']); break;
      case 3: this.openEmprestimoDialog(); break; //this.router.navigate(['/']); break;
    }
  }

  openPessoaDialog(): void {
    const dialogRef = this.dialog.open(PessoaDialogComponent, {
      width: '450px',
      data: {pessoaId: 0 }
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result)
        this.menuComponentEvents.atualizar(2);
    });
  }

  openItemDialog(): void {
    const dialogRef = this.dialog.open(ItemDialogComponent, {
      width: '450px',
      data: {itemId: 0 }
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result)
        this.menuComponentEvents.atualizar(1);
    });
  }

  openEmprestimoDialog(): void {
    const dialogRef = this.dialog.open(EmprestimoDialogComponent, {
      width: '450px',
      data: {emprestimoId: 0 }
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result)
        this.menuComponentEvents.atualizar(3);
    });
  }
}
