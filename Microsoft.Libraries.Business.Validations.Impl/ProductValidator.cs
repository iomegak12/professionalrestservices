using Microsoft.Libraries.Business.Validations.Interfaces;
using Microsoft.Libraries.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Libraries.Business.Validations.Impl
{
    public class ProductValidator : IBusinessValidator<Product>
    {
        private const int MIN_UNIT_PRICE = 1;
        public bool Validate(Product tObject)
        {
            var validation = tObject != default(Product) &&
                !string.IsNullOrEmpty(tObject.ProductName) &&
                tObject.UnitPrice >= MIN_UNIT_PRICE &&
                tObject.IsActive;

            return validation;
        }
    }
}
