using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Persistence.Interfaces
{
    public interface IRepositoryBase<T>
    {
        Task<T> CreateAsync(T workspace, CancellationToken cancellationToken);
        Task<T> GetAsync(Guid projectId, CancellationToken cancellationToken);
        Task<T> UpdateAsync(T project, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(T project,CancellationToken cancellationToken); 
    }
}
