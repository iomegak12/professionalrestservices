using System;
using System.Collections.Generic;

namespace Microsoft.Libraries.Repositories.Interfaces
{
    public interface IRepository<EntityType, EntityKey> : IDisposable
    {
        IEnumerable<EntityType> GetEntities();
        EntityType GetEntityByKey(EntityKey entityKey);
        bool AddEntity(EntityType entityObject);
    }
}
