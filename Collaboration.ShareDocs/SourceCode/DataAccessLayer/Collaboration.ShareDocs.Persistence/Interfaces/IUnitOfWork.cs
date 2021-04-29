using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Persistence.Interfaces
{
    public interface IUnitOfWork
    {
        IProjectRepository ProjectRepository { get; }

        IWorkspaceRepository WorkspaceRepository { get; }

        void Save();

        Task<int> SaveChangesAsync( CancellationToken cancellationToken = new CancellationToken( ) );
    }
}
