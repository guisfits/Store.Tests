using System;
using Flunt.Notifications;
using Flunt.Validations;
using Store.Domain.Commands.Interfaces;

namespace Store.Domain.Commands
{
    public class CreateOrderItemCommand : Notifiable, ICommand
    {
        public CreateOrderItemCommand()
        {
        }

        public CreateOrderItemCommand(Guid productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }

        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .HasLen(ProductId.ToString(), 32, "Product", "Id do Produto Inválido")
                    .IsGreaterThan(Quantity, 0, "Quantity", "Quantidade inválida")
            );
        }
    }
}
