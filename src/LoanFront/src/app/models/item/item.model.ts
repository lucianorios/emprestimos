export class ItemModel{
  id: number;
  nome: string;
  tipo: 0 | 1;

  notifications: string[] = [];

  constructor(){
    this.notifications = [];
  }

  isValid(){
    if(!this.nome)
      this.notifications.push('Informe um nome');

    return this.notifications.length == 0;
  }

  clearNorifications(){
    this.notifications = [];
  }
}
