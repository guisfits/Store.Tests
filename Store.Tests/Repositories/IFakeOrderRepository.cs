using Store.Domain.Entities;
using Store.Domain.Repositories;

namespace Store.Tests.Repositories
{
    public class IFakeOrderRepository : IOrderRepository
    {
        public void Save(Order order)
        {
        }
    }
}
