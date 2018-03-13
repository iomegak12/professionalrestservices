using Microsoft.Libraries.Business.Interfaces;
using Microsoft.Libraries.Business.Validations.Interfaces;
using Microsoft.Libraries.Models;
using Microsoft.Libraries.Repositories.Interfaces;
using System;
using System.Collections.Generic;

namespace Microsoft.Libraries.Business.Impl
{
    public class ProductsBusinessComponent : IProductsBusinessComponent
    {
        private const string INVALID_PRODUCTS_REPOSITORY = @"Invalid Product(s) Repository Specified!";
        private const string BUSINESS_VALIDATION_FAILED = @"Products Business Validation Failed!";
        private const string INVALID_ARGUMENTS = @"Invalid Argument(s) Specified!";

        private IProductsRepository productsRepository = default(IProductsRepository);
        private IBusinessValidator<string> productNameValidator = default(IBusinessValidator<string>);
        private IBusinessValidator<Product> productValidator = default(IBusinessValidator<Product>);

        public ProductsBusinessComponent(IProductsRepository productsRepository,
            IBusinessValidator<string> productNameValidator,
            IBusinessValidator<Product> productValidator)
        {
            if (productsRepository == default(IProductsRepository))
                throw new ArgumentException(INVALID_PRODUCTS_REPOSITORY);

            if (productNameValidator == default(IBusinessValidator<string>) &&
                productValidator == default(IBusinessValidator<Product>))
                throw new ArgumentException(INVALID_ARGUMENTS);

            this.productsRepository = productsRepository;
            this.productNameValidator = productNameValidator;
            this.productValidator = productValidator;
        }

        public bool AddNewProduct(Product product)
        {
            var validation = product != default(Product) &&
                this.productValidator.Validate(product);

            if (!validation)
                throw new ApplicationException(BUSINESS_VALIDATION_FAILED);

            var status = this.productsRepository.AddEntity(product);

            return status;
        }

        public void Dispose() => this.productsRepository?.Dispose();

        public Product GetProductDetail(int productId)
        {
            var validation = productId != default(int);

            if (!validation)
                throw new ArgumentException(INVALID_ARGUMENTS);

            var filteredProduct = this.productsRepository.GetEntityByKey(productId);

            return filteredProduct;
        }

        public IEnumerable<Product> GetProducts(string productName = null)
        {
            var products = default(IEnumerable<Product>);

            if (string.IsNullOrEmpty(productName))
                products = this.productsRepository.GetEntities();
            else
            {
                var businessValidation = this.productNameValidator.Validate(productName);

                if (!businessValidation)
                    throw new ApplicationException(BUSINESS_VALIDATION_FAILED);

                products = this.productsRepository.GetProductsByName(productName);
            }

            return products;
        }
    }
}
