using Microsoft.Libraries.Models;
using Microsoft.Libraries.ORM.Interfaces;
using Microsoft.Libraries.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Libraries.Repositories.Impl
{
    public class ProductsRepository : IProductsRepository
    {
        private const string INVALID_PRODUCTS_CONTEXT = @"Invalid Product Context Specified!";
        private const string INVALID_ARGUMENTS = @"Invalid Argument(s) Specified!";

        private IProductsContext productsContext = default(IProductsContext);

        public ProductsRepository(IProductsContext productsContext)
        {
            if (productsContext == default(IProductsContext))
                throw new ArgumentException(INVALID_PRODUCTS_CONTEXT);

            this.productsContext = productsContext;
        }

        public bool AddEntity(Product entityObject)
        {
            var status = default(bool);
            var validation = entityObject != default(Product);

            if (!validation)
                throw new ArgumentException(INVALID_ARGUMENTS);

            this.productsContext.Products.Add(entityObject);
            status = this.productsContext.CommitChanges();

            return status;
        }

        public void Dispose() => this.productsContext?.Dispose();

        public IEnumerable<Product> GetEntities()
        {
            var validation = this.productsContext != default(IProductsContext);

            if (!validation)
                throw new ArgumentException(INVALID_PRODUCTS_CONTEXT);

            var products = this.productsContext.Products.ToList();

            return products;
        }

        public Product GetEntityByKey(int entityKey)
        {
            var validation = entityKey != default(int);

            if (!validation)
                throw new ArgumentException(INVALID_ARGUMENTS);

            var filteredProduct = this.productsContext
                .Products
                .Where(product => product.ProductId.Equals(entityKey))
                .FirstOrDefault();

            return filteredProduct;
        }

        public IEnumerable<Product> GetProductsByName(string productName)
        {
            var validation = !string.IsNullOrEmpty(productName);

            if (!validation)
                throw new ArgumentException(INVALID_ARGUMENTS);

            var filteredProdcuts = this.productsContext
                .Products
                .Where(product => product.ProductName.Contains(productName))
                .ToList();

            return filteredProdcuts;
        }
    }
}
