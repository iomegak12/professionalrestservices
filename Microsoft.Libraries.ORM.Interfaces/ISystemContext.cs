using System;

namespace Microsoft.Libraries.ORM.Interfaces
{
    public interface ISystemContext : IDisposable
    {
        bool CommitChanges();
    }
}
