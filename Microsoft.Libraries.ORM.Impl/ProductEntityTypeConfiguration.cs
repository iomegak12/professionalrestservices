using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Libraries.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Libraries.ORM.Impl
{
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(model => model.ProductId);

            builder.Property(
                model => model.ProductId).UseSqlServerIdentityColumn();

            builder.ToTable("Products");
        }
    }
}
