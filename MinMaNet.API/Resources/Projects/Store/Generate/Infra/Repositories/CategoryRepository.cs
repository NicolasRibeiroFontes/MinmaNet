using Microsoft.EntityFrameworkCore;
using Store.Entities;
using Store.Infra.Context;

namespace Store.Infra.Repositories
{
public class CategoryRepository : ICategoryRepository
{
private readonly DBContext context;

public CategoryRepository(DBContext context)
{
this.context = context;
}

public Category Create(Category model)
{
this.context.Categorys.Add(model);
this.context.SaveChanges();
return model;
}

public Category? Get(int id)
{
return context.Categorys.FirstOrDefault(x => x.Id == id);
}

public IQueryable<Category> Get()
{
return context.Categorys;
}

public Category Update(Category model)
{
var entry = context.Entry(model);
this.context.Categorys.Attach(model);
entry.State = EntityState.Modified;
context.SaveChanges();
return model;
}

public void Delete(Category model)
{
context.Categorys.Remove(user);
context.SaveChanges();
}
}
}
