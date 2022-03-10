using Store.Entities;

namespace Store.Infra.Interfaces
{
public interface IProductRepository 
{
Product Create(Product model);
Product? Get(int id);
IQueryable<Product> Get();
Product Update(Product model);
void Delete(Product model);
}
}
