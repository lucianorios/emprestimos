import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EmprestimoComponent } from './pages/emprestimo/emprestimo.component';
import { ItemComponent } from './pages/item/item.component';
import { LoginComponent } from './pages/login/login.component';
import { PessoaComponent } from './pages/pessoa/pessoa.component';
import { AuthGuard } from './shared/auth/auth-guard.service';


const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: '',
    component: EmprestimoComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'pessoa',
    component: PessoaComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'jogo',
    component: ItemComponent,
    canActivate: [AuthGuard]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
