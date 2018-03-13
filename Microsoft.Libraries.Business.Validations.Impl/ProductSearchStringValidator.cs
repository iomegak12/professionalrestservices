using Microsoft.Libraries.Business.Validations.Interfaces;
using System;
using System.Linq;

namespace Microsoft.Libraries.Business.Validations.Impl
{
    public class ProductSearchStringValidator : IBusinessValidator<string>
    {
        private const int MIN_SEARCH_LENGTH = 3;
        public bool Validate(string tObject)
        {
            var badKeywords = new string[] { "bad", "worse", "awful", "not good" };
            var validation = !badKeywords.Contains(tObject) &&
                tObject.Length >= MIN_SEARCH_LENGTH;

            return validation;
        }
    }
}
