using Microsoft.Libraries.Models;
using System;
using System.Collections.Generic;

namespace Microsoft.Libraries.Business.Interfaces
{
    public interface IProductsBusinessComponent : IDisposable
    {
        IEnumerable<Product> GetProducts(string productName = default(string));
        Product GetProductDetail(int productId);
        bool AddNewProduct(Product product);
    }
}
