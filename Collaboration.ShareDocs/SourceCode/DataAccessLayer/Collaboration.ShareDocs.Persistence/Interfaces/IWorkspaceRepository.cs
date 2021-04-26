using Collaboration.ShareDocs.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Persistence.Interfaces
{
    public interface IWorkspaceRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="workspace,currentUser">CreateWorkspaceCommand</param>
        /// <returns>Guid</returns>
        Task<Guid> CreateAsync(Workspace workspace);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workspaceId">WorkspaceId</param>
        /// <returns>Workspace Entity "//TODO dto"</returns>
        Task<Workspace> GetAsync(Guid workspaceId);

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
        /// <param name="workspace">UpdateWorkspaceCommand</param>
        /// <returns>Workspace</returns>
        Task<Workspace> UpdataAsync(Workspace workspace, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workspaceId,cancellationToken">Guid</param>
        /// <returns>DeleteBy</returns>
        Task<string> DeleteAsync(Guid workspaceId,CancellationToken cancellationToken);

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
    }
}
