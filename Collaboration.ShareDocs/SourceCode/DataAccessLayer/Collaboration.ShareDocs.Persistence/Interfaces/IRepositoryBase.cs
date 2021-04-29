using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Persistence.Interfaces
{
    public interface IRepositoryBase<T>
    {
        Task<T> CreateAsync(T obj, CancellationToken cancellationToken);
        Task<T> GetAsync(Guid id, CancellationToken cancellationToken);
        Task<T> UpdateAsync(T obj, CancellationToken cancellationToken);
        bool Delete(T obj);
    }
}
