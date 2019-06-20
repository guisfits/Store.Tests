using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Store.Domain.Entities;
using Store.Domain.Queries;

namespace Store.Tests.Queries
{
    [TestFixture]
    public class ProductQueriesTests
    {
        #region Setup / Teardown 

        private IList<Product> _products;

        [SetUp]
        public void Setup()
        {
            _products = new List<Product>
            {
                new Product("Produto 01", 10, true),
                new Product("Produto 02", 10, true),
                new Product("Produto 03", 10, true),
                new Product("Produto 04", 10, false),
                new Product("Produto 05", 10, false),
            };
        }

        [TearDown]
        public void Teardown()
        {
            _products = null;
        }

        #endregion

        [Test]
        [Category("Queries")]
        public void DadoAConsultaDeProdutosAtivos_DeveRetornar3()
        {
            var produtosAtivos = _products.AsQueryable().Where(ProductQueries.GetActiveProducts());
            Assert.That(produtosAtivos.Count(), Is.EqualTo(3));
        }

        [Test]
        [Category("Queries")]
        public void DadoAConsultaDeProdutosInativos_DeveRetornar2()
        {
            var produtosAtivos = _products.AsQueryable().Where(ProductQueries.GetInactiveProducts());
            Assert.That(produtosAtivos.Count(), Is.EqualTo(2));
        }
    }
}
