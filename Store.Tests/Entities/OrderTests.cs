using System;
using NUnit.Framework;
using Store.Domain.Entities;
using Store.Domain.Enums;

namespace Store.Tests.Entities
{
    [TestFixture]
    public class OrderTests
    {
        #region SetUp / TearDown 

        private Customer _customer;
        private Product _product;
        private Discount _discount;

        [SetUp]
        public void Setup()
        {
            _customer = new Customer("Fulano da Silva", "test@email.com");
            _product = new Product("Produto 1", 10, true);
            _discount = new Discount(10, DateTime.Now.AddDays(1));
        }

        [TearDown]
        public void Teardown()
        {
            _customer = null;
            _product = null;
            _discount = null;
        }

        #endregion

        [Test]
        [Category("Domain")]
        public void DadoUmNovoPedidoValido_DeveGerarUmNumeroCom8Caracters()
        {
            // Arrange
            var order = GetOrderWithoutFeeAndDiscount();

            // Assert
            Assert.That(order.Number.Length, Is.EqualTo(8));
        }

        [Test]
        [Category("Domain")]
        public void DadoUmNovoPedido_SeuStatusDeveSerAguardandoPagamento()
        {
            // Arrange
            var order = GetOrderWithoutFeeAndDiscount();

            // Assert
            Assert.That(order.Status, Is.EqualTo(EOrderStatus.WaitingPayment));
        }

        [Test]
        [Category("Domain")]
        public void DadoUmPagamentoDoPedido_SeuStatusDeveSerAguardandoEntrega()
        {
            // Arrange
            var order = GetOrderWithoutFeeAndDiscount();

            // Act
            order.AddItem(_product, 1);
            order.Pay(10);

            // Assert
            Assert.That(order.Status, Is.EqualTo(EOrderStatus.WaitingDelivery));
        }

        [Test]
        [Category("Domain")]
        public void DadoUmPedidoCancelado_SeuStatusDeveSerCancelado()
        {
            // Arrange
            var order = GetOrderWithoutFeeAndDiscount();
            
            // Act
            order.Cancel();

            // Assert
            Assert.That(order.Status, Is.EqualTo(EOrderStatus.Canceled));
        }

        [Test]
        [Category("Domain")]
        public void DadoUmNovoItemSemProduto_EsteNaoDeveSerAdicionado()
        {
            // Arrange
            var order = GetOrderWithoutFeeAndDiscount();

            // Act
            order.AddItem(null, 1);

            // Assert
            Assert.That(order.Items.Count, Is.EqualTo(0));
        }

        [Test]
        [Category("Domain")]
        public void DadoUmNovoItemComQuantidadeMenorOuIgualAZero_NenhumItemDeveSerAdicionado()
        {
            // Arrange
            var order = GetOrderWithoutFeeAndDiscount();

            // Act
            order.AddItem(_product, -1);

            // Assert
            Assert.That(order.Items.Count, Is.EqualTo(0));
        }

        [Test]
        [Category("Domain")]
        public void DadoUmNovoPedidoValido_SeuTotalDeveSer50()
        {
            // Arrange
            var order = new Order(_customer, 10, _discount);

            // Act
            order.AddItem(_product, 5);

            // Assert
            Assert.That(order.Total, Is.EqualTo(50));
        }

        [Test]
        [Category("Domain")]
        public void DadoUmDescontoExpirado_OValorDoPedidoDeveSer60()
        {  
            // Arrange
            var discountExpired = new Discount(10, DateTime.Now.AddDays(-1));
            var order = new Order(_customer, 10, discountExpired);

            // Act
            order.AddItem(_product, 5);

            // Assert
            Assert.That(order.Total, Is.EqualTo(60));
        }

        [Test]
        [Category("Domain")]
        public void DadoUmDescontoInvalido_OValorDopedidoDeveSer60()
        {  
            // Arrange
            var order = new Order(_customer, 10, null);

            // Act
            order.AddItem(_product, 5);

            // Assert
            Assert.That(order.Total, Is.EqualTo(60));
        }

        [Test]
        [Category("Domain")]
        public void DadoUmDescontoDe10_OValorDoPedidoDeveSer50()
        {  
            // Arrange
            var discount = new Discount(10, DateTime.Now.AddDays(1));
            var order = new Order(_customer, 10, discount);

            // Act
            order.AddItem(_product, 5);

            // Assert
            Assert.That(order.Total, Is.EqualTo(50));
        }

        [Test]
        [Category("Domain")]
        public void DadoUmaTaxaDeEntregaDe10_OValorDoPedidoDeveSer60()
        {  
            // Arrange
            var order = new Order(_customer, 10, null);

            // Act
            order.AddItem(_product, 5);

            // Assert
            Assert.That(order.Total(), Is.EqualTo(60));
        }

        [Test]
        [Category("Domain")]
        public void DadoUmPedidoSemCliente_OMesmoDeveSerInvalido()
        {  
            // Arrange
            var order = new Order(null, 10, _discount);

            // Assert
            Assert.That(order.Valid, Is.False);
        }

        #region Helpers

        private Order GetOrderWithoutFeeAndDiscount()
        {
            return new Order(_customer, 0, null);
        }

        #endregion 
    }
}
