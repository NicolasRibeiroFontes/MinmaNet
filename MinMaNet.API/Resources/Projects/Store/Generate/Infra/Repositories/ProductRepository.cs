using Microsoft.EntityFrameworkCore;
using Store.Entities;
using Store.Infra.Context;

namespace Store.Infra.Repositories
{
public class ProductRepository : IProductRepository
{
private readonly DBContext context;

public ProductRepository(DBContext context)
{
this.context = context;
}

public Product Create(Product model)
{
this.context.Products.Add(model);
this.context.SaveChanges();
return model;
}

public Product? Get(int id)
{
return context.Products.FirstOrDefault(x => x.Id == id);
}

public IQueryable<Product> Get()
{
return context.Products;
}

public Product Update(Product model)
{
var entry = context.Entry(model);
this.context.Products.Attach(model);
entry.State = EntityState.Modified;
context.SaveChanges();
return model;
}

public void Delete(Product model)
{
context.Products.Remove(user);
context.SaveChanges();
}
}
}
