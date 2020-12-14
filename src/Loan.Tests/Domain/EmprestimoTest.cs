using Loan.WebApi.Domain.Entities;
using FluentAssertions;
using Xunit;
using System.Linq;
using System;

namespace Loan.Tests.Domain
{
    public class EmprestimoTest
    {
        public Pessoa PessoaValida { get; set; }
        public Pessoa PessoaInvalida { get; set; }

        public Item ItemValido { get; set; }
        public Item ItemInvalido { get; set; }

        public EmprestimoTest()
        {
            this.PessoaValida = new Pessoa(1, "Nome 1", "27999999999", "email@email.com");
            this.PessoaInvalida = new Pessoa();

            this.ItemValido = new Item(1, "Item 1");
            this.ItemInvalido = new Item();
        }

        [Fact]
        public void DadosObrigatorio()
        {
            var emprestimo = new Emprestimo();

            emprestimo.Validar();

            emprestimo.Valido.Should().BeFalse();
        }

        public void DadosValidos()
        {
            var emprestimo = new Emprestimo();

            emprestimo.SetData(DateTime.Now);
            emprestimo.SetItem(this.ItemValido);
            emprestimo.SetPessoa(this.PessoaValida);

            emprestimo.Validar();

            emprestimo.Valido.Should().BeTrue();
        }

        [Fact]
        public void DeveExistirMensagens()
        {
            var emprestimo = new Emprestimo();

            emprestimo.Validar();

            emprestimo.Notifications.Should().NotHaveCount(0);
        }

        [Fact]
        public void PessoaObrigatoria()
        {
            var emprestimo = new Emprestimo();

            emprestimo.SetData(DateTime.Now);
            emprestimo.SetItem(this.ItemValido);

            emprestimo.Validar();

            emprestimo.Notifications.Should().NotHaveCount(0);

            emprestimo.Notifications.ElementAt(0).Value.Should().Be("Informe uma pessoa");
        }

        [Fact]
        public void ItemObrigatorio()
        {
            var emprestimo = new Emprestimo();

            emprestimo.SetData(DateTime.Now);
            emprestimo.SetPessoa(this.PessoaValida);

            emprestimo.Validar();

            emprestimo.Notifications.Should().NotHaveCount(0);

            emprestimo.Notifications.ElementAt(0).Value.Should().Be("Informe um item");
        }

        [Fact]
        public void PessoaObrigatoria_DadosInvalidos()
        {
            var emprestimo = new Emprestimo();

            emprestimo.SetData(DateTime.Now);
            emprestimo.SetItem(this.ItemValido);
            emprestimo.SetPessoa(this.PessoaInvalida);

            emprestimo.Validar();

            emprestimo.Notifications.Should().NotHaveCount(0);

            emprestimo.Notifications.ElementAt(0).Value.Should().Be("Informe uma pessoa");
        }

        [Fact]
        public void ItemObrigatorio_DadosInvalidos()
        {
            var emprestimo = new Emprestimo();

            emprestimo.SetData(DateTime.Now);
            emprestimo.SetItem(this.ItemInvalido);
            emprestimo.SetPessoa(this.PessoaValida);

            emprestimo.Validar();

            emprestimo.Notifications.Should().NotHaveCount(0);

            emprestimo.Notifications.ElementAt(0).Value.Should().Be("Informe um item");
        }


        [Fact]
        public void DataDevolucaoInvaida()
        {
            var emprestimo = new Emprestimo();

            emprestimo.SetData(DateTime.Now);
            emprestimo.SetItem(this.ItemValido);
            emprestimo.SetPessoa(this.PessoaValida);

            emprestimo.SetDevolucao(DateTime.Now.AddMonths(-1));

            emprestimo.Validar();

            emprestimo.Notifications.Should().NotHaveCount(0);

            emprestimo.Notifications.ElementAt(0).Value.Should().Be("A data de devolução deve ser superior a data do Empréstimo");
        }
    }
}
