using System;
using System.Linq.Expressions;
using Store.Domain.Entities;

namespace Store.Domain.Queries
{
    public static class ProductQueries
    {
        public static Expression<Func<Product, bool>> GetActiveProducts()
        {
            return product => product.Active;
        }

        public static Expression<Func<Product, bool>> GetInactiveProducts()
        {
            return product => product.Active == false;
        }
    }
}
