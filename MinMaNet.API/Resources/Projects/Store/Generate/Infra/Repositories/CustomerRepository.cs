using Microsoft.EntityFrameworkCore;
using Store.Entities;
using Store.Infra.Context;

namespace Store.Infra.Repositories
{
public class CustomerRepository : ICustomerRepository
{
private readonly DBContext context;

public CustomerRepository(DBContext context)
{
this.context = context;
}

public Customer Create(Customer model)
{
this.context.Customers.Add(model);
this.context.SaveChanges();
return model;
}

public Customer? Get(int id)
{
return context.Customers.FirstOrDefault(x => x.Id == id);
}

public IQueryable<Customer> Get()
{
return context.Customers;
}

public Customer Update(Customer model)
{
var entry = context.Entry(model);
this.context.Customers.Attach(model);
entry.State = EntityState.Modified;
context.SaveChanges();
return model;
}

public void Delete(Customer model)
{
context.Customers.Remove(user);
context.SaveChanges();
}
}
}
