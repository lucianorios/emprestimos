using System;

namespace Loan.WebApi.Domain.Entities
{
    public class Emprestimo: Entity
    {
        public DateTime Data { get; protected set; }
        public DateTime? Devolucao { get; protected set; }
        public Item Item { get; protected set; }
        public Pessoa Pessoa { get; protected set; }
        public long PessoaId { get; protected set; }
        public long ItemId { get; protected set; }

        public Emprestimo() { }

        public Emprestimo(DateTime data, Item item, Pessoa pessoa)
        {
            this.Data = data;
            this.Pessoa = pessoa;
            this.Item = item;
        }

        public void Validar()
        {
            if (this.Pessoa == null || this.Pessoa.Id == 0)
                this.AddNotification("Informe uma pessoa");
            if (this.Item == null || this.Item.Id == 0)
                this.AddNotification("Informe um item");
            if(Devolucao != null && Devolucao<Data)
                this.AddNotification("A data de devolução deve ser superior a data do Empréstimo");
        }

        public void SetPessoa(Pessoa pessoa)
        {
            this.Pessoa = pessoa;
            this.PessoaId = pessoa.Id;
        }

        public void SetItem(Item item)
        {
            this.Item = item;
            this.ItemId = item.Id;
        }
        
        public void SetData(DateTime data) => this.Data = data;
        public void SetDevolucao(DateTime dataDevolucao) => this.Devolucao = dataDevolucao;

    }
}
