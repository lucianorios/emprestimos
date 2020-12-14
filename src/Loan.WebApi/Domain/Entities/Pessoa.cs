using System;
using System.Collections.Generic;
using System.Text;

namespace Loan.WebApi.Domain.Entities
{
    public class Pessoa: Entity
    {
        public string Nome { get; protected set; }
        public string Email { get; protected set; }
        public string Telefone { get; protected set; }

        public IEnumerable<Emprestimo> Emprestimos => _emprestimos.AsReadOnly();

        private List<Emprestimo> _emprestimos = new List<Emprestimo>();

        public Pessoa() { }

        public Pessoa(long id, string nome, string email, string telefone)
        {
            this.Id = id;
            this.Nome = nome;
            this.Email = email;
            this.Telefone = telefone;
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(this.Nome))
                this.AddNotification("Informe um nome");

            if (string.IsNullOrEmpty(this.Email))
                this.AddNotification("Informe um email");

            if (string.IsNullOrEmpty(this.Telefone))
                this.AddNotification("Informe um telefone");
        }

        public void SetNome(string nome) => this.Nome = nome;
        public void SetTelefone(string telefone) => this.Telefone = telefone;
        public void SetEmail(string email) => this.Email = email;
    }
}
