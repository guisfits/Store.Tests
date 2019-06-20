using System.Linq;
using Flunt.Notifications;
using Store.Domain.Commands;
using Store.Domain.Commands.Interfaces;
using Store.Domain.Entities;
using Store.Domain.Handlers.Interfaces;
using Store.Domain.Repositories;

namespace Store.Domain.Handlers
{
    public class OrderHandler : 
        Notifiable, 
        IHandler<CreateOrderCommand>
    {
        #region Construtor / Repositories

        private readonly ICustomerRepository _customerRepository;
        private readonly IDeliveryFeeRepository _deliveryFeeRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderHandler(
            ICustomerRepository customerRepository,
            IDeliveryFeeRepository deliveryFeeRepository,
            IDiscountRepository discountRepository,
            IProductRepository productRepository,
            IOrderRepository orderRepository)
        {
            _customerRepository = customerRepository;
            _deliveryFeeRepository = deliveryFeeRepository;
            _discountRepository = discountRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        #endregion

        public ICommandResult Handle(CreateOrderCommand command)
        {
            var result = new GenericCommandResult();

            command.Validate();
            if(command.Invalid)
            {
                result.Fail(command.Notifications);
                return result;
            }

            var customer = _customerRepository.Get(command.CustomerId);
            var deliveryFee = _deliveryFeeRepository.Get(command.ZipCode);
            var discount = _discountRepository.Get(command.PromoCode);

            var products = _productRepository.Get(command.ExtractProductsIdsFromItems());
            var order = new Order(customer, deliveryFee, discount);
            
            foreach(var item in command.Items) 
            {
                var product = products.FirstOrDefault(x => x.Id == item.ProductId);
                order.AddItem(product, item.Quantity);
            }

            AddNotifications(order.Notifications);

            if(Invalid)
                result.Fail(this.Notifications);
            else
                _orderRepository.Save(order);

            return result;
        }
    }
}
