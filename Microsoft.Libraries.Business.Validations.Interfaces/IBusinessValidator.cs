using System;

namespace Microsoft.Libraries.Business.Validations.Interfaces
{
    public interface IBusinessValidator<T>
    {
        bool Validate(T tObject);
    }
}
