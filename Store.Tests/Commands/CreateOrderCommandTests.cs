using System;
using System.Collections.Generic;
using NUnit.Framework;
using Store.Domain.Commands;

namespace Store.Tests.Commands
{
    [TestFixture]
    public class CreateOrderCommandTests
    {
        [Test]
        [Category("Handlers")]
        public void DadoUmComandoInvalido_OPedidoNaoDeveSerGerado()
        {
            // Arrange
            var command = new CreateOrderCommand
            {
                CustomerId = "",
                ZipCode = "13411080",
                PromoCode = "12345678",
                Items = new List<CreateOrderItemCommand>
                {
                    new CreateOrderItemCommand(Guid.NewGuid(), 1),
                    new CreateOrderItemCommand(Guid.NewGuid(), 1),
                }
            };

            // Act
            command.Validate();

            // Assert
            Assert.That(command.Valid, Is.False);
        }
    }
}
