using Collaboration.ShareDocs.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Persistence.Interfaces
{
    public interface IWorkspaceRepository : IRepositoryBase<Workspace>
    {
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns>Workspaces</returns>
        Task<List<Workspace>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Workspace</returns>
        Task<Workspace> GetLastAsync(CancellationToken cancellationToken);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Workspace</returns>
        Task<Workspace> GetLastModifiedAsync(CancellationToken cancellationToken); 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyWord">string</param>
        /// <returns>Workspaces</returns>
        Task<List<Workspace>> GetByKeyWord(string keyWord);

        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns>int</returns>
        Task<int> GetCount();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workspaceId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> DeleteAsync( Guid workspaceId, CancellationToken cancellationToken );
    }
}
