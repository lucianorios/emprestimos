export class PessoaModel {
  id: number;
  nome: string;
  telefone: string;
  email: string;

  notifications: string[] = [];

  constructor(){
    this.notifications = [];
  }

  isValid(){
    if(!this.nome)
      this.notifications.push('Informe um nome');

    if(!this.telefone)
      this.notifications.push('Informe um telefone');

    if(!this.email)
      this.notifications.push('Informe um e-mail');

    return this.notifications.length == 0;
  }

  clearNorifications(){
    this.notifications = [];
  }
}
