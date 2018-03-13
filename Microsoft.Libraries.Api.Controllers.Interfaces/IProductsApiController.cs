using Microsoft.AspNetCore.Mvc;
using Microsoft.Libraries.Models;
using System;

namespace Microsoft.Libraries.Api.Controllers.Interfaces
{
    public interface IProductsApiController : IDisposable
    {
        IActionResult GetProducts(int noOfRecords = default(int));
        IActionResult SearchProducts(string productName);
        IActionResult GetProduct(int productId);
        IActionResult SaveProduct(Product product);
    }
}
