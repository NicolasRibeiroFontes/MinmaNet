using Store.Entities;

namespace Store.Infra.Interfaces
{
public interface ICategoryRepository 
{
Category Create(Category model);
Category? Get(int id);
IQueryable<Category> Get();
Category Update(Category model);
void Delete(Category model);
}
}
