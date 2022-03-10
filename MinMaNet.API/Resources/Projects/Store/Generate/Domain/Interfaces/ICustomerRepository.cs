using Store.Entities;

namespace Store.Infra.Interfaces
{
public interface ICustomerRepository 
{
Customer Create(Customer model);
Customer? Get(int id);
IQueryable<Customer> Get();
Customer Update(Customer model);
void Delete(Customer model);
}
}
