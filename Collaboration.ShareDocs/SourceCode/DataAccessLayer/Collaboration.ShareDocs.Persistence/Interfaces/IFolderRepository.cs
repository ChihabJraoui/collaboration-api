using Collaboration.ShareDocs.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Persistence.Interfaces
{
    public interface IFolderRepository : IRepositoryBase<Folder>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Guid,cancellationToken">CancellationToken</param>
        /// <returns>List of Foldes</returns>
        Task<List<Folder>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">string</param>
        /// <returns>Boolean</returns>
        Task<Boolean> UniqueName(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request,cancellationToken">GetAllFoldersQuery,CancellationToken</param>
        /// <returns>GetFoldersDtoLists</returns>
        Task<string> RenameAsync(Folder folder, string name, CancellationToken cancellationToken);

        Task<List<Folder>> GetByProjectIdAsync(Guid projectId, CancellationToken cancellationToken);
        Task<List<Folder>> GetByCreatedAsync(Guid userId, CancellationToken cancellationToken);
    }
}
