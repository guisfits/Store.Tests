using Store.Domain.Entities;
using Store.Domain.Repositories;

namespace Store.Tests.Repositories
{
    public class FakeCustomerRepository : ICustomerRepository
    {
        public Customer Get(string document)
        {
            if(document == "12345678900")
                return new Customer("Usuário Teste", "user@test.com");

            return null;
        }
    }
}
