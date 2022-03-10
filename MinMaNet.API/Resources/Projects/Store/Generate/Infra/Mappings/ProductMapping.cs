using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Entities;

namespace Store.Infra.Mapping
{
public class ProductMapping : IEntityTypeConfiguration<Product>
{
public void Configure(EntityTypeBuilder<Product> builder)
{
builder.HasKey(key => key.Id);
}
}
}
