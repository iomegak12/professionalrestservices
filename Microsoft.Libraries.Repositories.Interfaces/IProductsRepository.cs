using Microsoft.Libraries.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Libraries.Repositories.Interfaces
{
    public interface IProductsRepository : IRepository<Product, int>
    {
        IEnumerable<Product> GetProductsByName(string productName);
    }
}
