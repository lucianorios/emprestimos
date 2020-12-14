using Loan.WebApi.Domain.Entities;
using FluentAssertions;
using Xunit;
using System.Linq;

namespace Loan.Tests.Domain
{
    public class PessoaTest
    {
        [Fact]
        public void DadosObrigatorio()
        {
            var pessoa = new Pessoa();

            pessoa.Validar();

            pessoa.Valido.Should().BeFalse();
        }

        public void DadosValidos()
        {
            var pessoa = new Pessoa(1, "Nome 1", "27999999999", "email@email.com");

            pessoa.Validar();

            pessoa.Valido.Should().BeTrue();
        }

        [Fact]
        public void DeveExistirMensagens()
        {
            var pessoa = new Pessoa();

            pessoa.Validar();

            pessoa.Notifications.Should().NotHaveCount(0);
        }

        [Fact]
        public void NomeObrigatorio()
        {
            var pessoa = new Pessoa(1, "", "27999999999", "email@email.com");

            pessoa.Validar();

            pessoa.Notifications.Should().NotHaveCount(0);

            pessoa.Notifications.ElementAt(0).Value.Should().Be("Informe um nome");
        }

        [Fact]
        public void EmailObrigatorio()
        {
            var pessoa = new Pessoa(1, "Nome 1", null, "27999999999");

            pessoa.Validar();

            pessoa.Notifications.Should().NotHaveCount(0);

            pessoa.Notifications.ElementAt(0).Value.Should().Be("Informe um email");
        }

        [Fact]
        public void TelefoneObrigatorio()
        {
            var pessoa = new Pessoa(1, "Nome 2", "email@email.com", "");

            pessoa.Validar();

            pessoa.Notifications.Should().NotHaveCount(0);

            pessoa.Notifications.ElementAt(0).Value.Should().Be("Informe um telefone");
        }
    }
}
