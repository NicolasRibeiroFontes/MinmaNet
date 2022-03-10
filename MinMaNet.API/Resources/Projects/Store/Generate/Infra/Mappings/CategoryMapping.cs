using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Entities;

namespace Store.Infra.Mapping
{
public class CategoryMapping : IEntityTypeConfiguration<Category>
{
public void Configure(EntityTypeBuilder<Category> builder)
{
builder.HasKey(key => key.Id);
}
}
}
