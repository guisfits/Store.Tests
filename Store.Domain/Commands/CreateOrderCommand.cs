using System;
using System.Collections.Generic;
using System.Linq;
using Flunt.Notifications;
using Flunt.Validations;
using Store.Domain.Commands.Interfaces;

namespace Store.Domain.Commands
{
    public class CreateOrderCommand : Notifiable, ICommand
    {
        public CreateOrderCommand()
        {
            Items = new List<CreateOrderItemCommand>();
        }

        public CreateOrderCommand(string customerId, string zipCode, string promoCode, IList<CreateOrderItemCommand> items)
        {
            CustomerId = customerId;
            ZipCode = zipCode;
            PromoCode = promoCode;
            Items = items;
        }

        public string CustomerId { get; set; }
        public string ZipCode { get; set; }
        public string PromoCode { get; set; }
        public IList<CreateOrderItemCommand> Items { get; set; }

        public IEnumerable<Guid> ExtractProductsIdsFromItems()
        {
            return Items.Select(x => x.ProductId).ToArray();
        }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                .Requires()
                .HasLen(CustomerId, 11, "CustomerId", "Cliente inválido")
                .HasLen(ZipCode, 8, "ZipCode", "CEP inválido")
            );
        }
    }
}
