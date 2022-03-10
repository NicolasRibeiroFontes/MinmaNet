using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Entities;

namespace Store.Infra.Mapping
{
public class CustomerMapping : IEntityTypeConfiguration<Customer>
{
public void Configure(EntityTypeBuilder<Customer> builder)
{
builder.HasKey(key => key.Id);
}
}
}
