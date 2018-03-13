using Microsoft.EntityFrameworkCore;
using Microsoft.Libraries.Models;
using Microsoft.Libraries.ORM.Interfaces;
using System;

namespace Microsoft.Libraries.ORM.Impl
{
    public class ProductsContext : DbContext, IProductsContext
    {
        private const int MIN_ROWS_AFFECTED = 1;
        public ProductsContext(DbContextOptions<ProductsContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Product> Products { get; set; }

        public bool CommitChanges()
        {
            var noOfRowsAffected = this.SaveChanges();

            return noOfRowsAffected >= MIN_ROWS_AFFECTED;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration<Product>(new ProductEntityTypeConfiguration());
        }
    }
}
