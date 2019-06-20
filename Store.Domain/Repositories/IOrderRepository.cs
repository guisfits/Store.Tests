using Store.Domain.Entities;

namespace Store.Domain.Repositories
{
    public interface IOrderRepository
    {
         void Save(Order order);
         
    }
}
