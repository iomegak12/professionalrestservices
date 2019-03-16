using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Libraries.Api.Controllers.Interfaces;
using Microsoft.Libraries.Business.Interfaces;
using Microsoft.Libraries.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Libraries.Api.Controllers.Impl
{
    /// <summary>
    /// Products REST Service that provides Inventory Operations
    /// </summary>
    [Produces("application/json")]
    [Route("api/products")]
    /// [Authorize]
    public class ProductsApiController : Controller, IProductsApiController
    {
        private const int DEFAULT_NO_OF_RECORDS = 25;
        private const string INVALID_BUSINESS_COMPONENT = @"Invalid Business Component Specified!";

        private IProductsBusinessComponent productsBusinessComponent = default(IProductsBusinessComponent);

        /// <summary>
        /// Constructor which accepts business component dependencies
        /// </summary>
        /// <param name="productsBusinessComponent">Valid Business Component</param>
        public ProductsApiController(IProductsBusinessComponent productsBusinessComponent)
        {
            if (productsBusinessComponent == default(IProductsBusinessComponent))
                throw new ArgumentException(INVALID_BUSINESS_COMPONENT);

            this.productsBusinessComponent = productsBusinessComponent;
        }

        /// <summary>
        /// Gets a Product Detail by Product Id
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <returns>Detail of the Product</returns>
        [HttpGet]
        [Route("detail/{productId}")]
        public IActionResult GetProduct(int productId)
        {
            var filteredProduct = this.productsBusinessComponent.GetProductDetail(productId);

            if (filteredProduct == default(Product))
                return NotFound();

            return Ok(filteredProduct);
        }

        /// <summary>
        /// Gets all products
        /// </summary>
        /// <returns>An array of products</returns>
        [HttpGet]
        public IActionResult GetProducts([FromQuery] int noOfProducts = DEFAULT_NO_OF_RECORDS)
        {
            var products = this.productsBusinessComponent
                .GetProducts()
                .Take(noOfProducts);

            return Ok(products);
        }

        /// <summary>
        /// Saves a New Product
        /// </summary>
        /// <param name="product">Product Detail</param>
        /// <returns>Status of the Operation</returns>
        [HttpPost]
        public IActionResult SaveProduct([FromBody] Product product)
        {
            var validation = product != default(Product);

            if (!validation)
                return BadRequest();

            var status = this.productsBusinessComponent.AddNewProduct(product);

            return Ok(status);
        }

        /// <summary>
        /// Searches products by Product Name partially
        /// </summary>
        /// <param name="productName">Partial Product Name</param>
        /// <returns>An array of products that match partial search string.</returns>
        [HttpGet]
        [Route("search/{productName}")]
        public IActionResult SearchProducts(string productName)
        {
            var validation = !string.IsNullOrEmpty(productName);

            if (!validation)
                return BadRequest();

            var filteredProducts = this.productsBusinessComponent.GetProducts(productName);

            if (filteredProducts == default(IEnumerable<Product>))
                return NotFound();

            return Ok(filteredProducts);
        }
    }
}
