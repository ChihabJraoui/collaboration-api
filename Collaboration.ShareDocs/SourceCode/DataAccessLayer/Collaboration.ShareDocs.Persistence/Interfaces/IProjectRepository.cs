using Collaboration.ShareDocs.Persistence.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Persistence.Interfaces
{
    public interface IProjectRepository: IRepositoryBase<Project>
    {
      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyWord">GetProjectByKeyWord</param>
        /// <returns>Projects</returns>
        Task<List<string>> GetByKeyWordAsync(string keyWord, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyWord">GetProjectsByWorkspaceId</param>
        /// <returns>Projects</returns>
        Task<List<Project>> GetByWorkspaceIdAsync(Guid workspaceId, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId">GetProjectsByCreatedId</param>
        /// <returns>Projects</returns>
        Task<List<Project>> GetByCreatedAsync(Guid userId, CancellationToken cancellationToken);
    }
}
