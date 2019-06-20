using System;
using System.Collections.Generic;
using NUnit.Framework;
using Store.Domain.Commands;
using Store.Domain.Handlers;
using Store.Domain.Repositories;
using Store.Tests.Repositories;

namespace Store.Tests.Handlers
{
    [TestFixture]
    public class OrderHandlerTests
    {
        #region Repositories / SetUp / TearDown

        private ICustomerRepository _customerRepository;
        private IDeliveryFeeRepository _deliveryFeeRepository;
        private IDiscountRepository _discountRepository;
        private IOrderRepository _orderRepository;
        private IProductRepository _productRepository;

        [SetUp]
        public void Initialize()
        {
            _customerRepository = new FakeCustomerRepository();
            _deliveryFeeRepository = new FakeDeliveryFeeRepository();
            _discountRepository = new FakeDiscountRepository();
            _orderRepository = new FakeOrderRepository();
            _productRepository = new FakeProductRepository();
        }

        [TearDown]
        public void Finalize()
        {
            _customerRepository = null;
            _deliveryFeeRepository = null;
            _discountRepository = null;
            _orderRepository = null;
            _productRepository = null;
        }

        #endregion

        [Test]
        [Category("Handlers")]
        public void DadoUmClienteInexistente_OPedidoNaoDeveSerGerado()
        {
            Assert.Fail();
        }

        [Test]
        [Category("Handlers")]
        public void DadoUmUmCepInvalido_OPedidoDeveSerGeradoNormalmente()
        {
            Assert.Fail();
        }

        [Test]
        [Category("Handlers")]
        public void DadoUmPromocodeInexistente_OPedidoDeveSerGeradoNormalmente()
        {
            Assert.Fail();
        }

        [Test]
        [Category("Handlers")]
        public void DadoUmPedidoSemitems_EsteNaoDeveSerGerado()
        {
            Assert.Fail();
        }

        [Test]
        [Category("Handlers")]
        public void DadoUmComandoInvalido_OPedidoNaoDeveSerGerado()
        {
            Assert.Fail();
        }

        [Test]
        [Category("Handlers")]
        public void DadoUmComandoValido_OPedidoDeveSerGeradoNormalmente()
        {
            var command = new CreateOrderCommand
            {
                CustomerId = "12345678",
                ZipCode = "13411080",
                PromoCode = "12345678",
                Items = new List<CreateOrderItemCommand>
                {
                    new CreateOrderItemCommand(Guid.NewGuid(), 1),
                    new CreateOrderItemCommand(Guid.NewGuid(), 1),
                }
            };

            var handler = GetOrderHandler();
            handler.Handle(command);

            Assert.That(handler.Valid, Is.True);
        }

        #region Helpers

        private OrderHandler GetOrderHandler()
        {
            return new OrderHandler(
                _customerRepository,
                _deliveryFeeRepository,
                _discountRepository,
                _productRepository,
                _orderRepository
            );
        }

        #endregion
    }
}
