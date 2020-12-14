using Loan.WebApi.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loan.WebApi.Domain.Entities
{
    public class Item: Entity
    {
        public string Nome { get; protected set; }
        public bool Cedido { get; set; }
        public TipoItem Tipo { get; protected set; }

        public Item()
        {
            this.Tipo = TipoItem.Jogo;
        }

        public Item(long id, string nome)
        {
            this.Id = id;
            this.Tipo = TipoItem.Jogo;
            this.Nome = nome;
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(this.Nome))
                this.AddNotification("Informe um nome");
        }

        public void SetNome(string nome) => this.Nome = nome;
        public void SetCedido(bool cedido) => this.Cedido = cedido;
    }
}
