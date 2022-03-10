using Microsoft.EntityFrameworkCore;
using Store.Entities;
using Store.Infra.Mapping;

namespace Store.Infra.Context
{
public class DBContext : DbContext
{
public DBContext(DbContextOptions<MySQLContext> options) : base(options) { }

public DbSet<Product> Products { get; set; }
public DbSet<Customer> Customers { get; set; }
public DbSet<Category> Categorys { get; set; }


protected override void OnModelCreating(ModelBuilder modelBuilder)
{
base.OnModelCreating(modelBuilder);

_mappings_

}
}
}
