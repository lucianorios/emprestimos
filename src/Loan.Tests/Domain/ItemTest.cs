using Loan.WebApi.Domain.Entities;
using FluentAssertions;
using Xunit;
using System.Linq;

namespace Loan.Tests.Domain
{
    public class ItemTest
    {
        [Fact]
        public void DadosObrigatorio()
        {
            var item = new Item();

            item.Validar();

            item.Valido.Should().BeFalse();
        }

        public void DadosValidos()
        {
            var item = new Item(1, "Nome 1");

            item.Validar();

            item.Valido.Should().BeTrue();
        }

        [Fact]
        public void DeveExistirMensagens()
        {
            var item = new Item();

            item.Validar();

            item.Notifications.Should().NotHaveCount(0);
        }

        [Fact]
        public void NomeObrigatorio()
        {
            var item = new Item(0, "");

            item.Validar();

            item.Notifications.Should().NotHaveCount(0);

            item.Notifications.ElementAt(0).Value.Should().Be("Informe um nome");
        }
    }
}
