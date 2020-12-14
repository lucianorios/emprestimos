import { ItemModel } from '../item/item.model';
import { PessoaModel } from '../pessoa/pessoa.model';

export class EmprestimoModel{
  id: number;
  data: Date;
  devolucao?: Date;
  item: ItemModel;
  pessoa: PessoaModel;
  diasDecorridos: number;

  notifications: string[] = [];

  constructor(){
    this.notifications = [];
  }

  isValid(){
    if(!this.pessoa)
      this.notifications.push('Informe uma pessoa');

    if(!this.item)
      this.notifications.push('Informe um item');

    if(!this.data)
      this.notifications.push('Informe uma data');

    return this.notifications.length == 0;
  }

  clearNorifications(){
    this.notifications = [];
  }
}
