using Collaboration.ShareDocs.Persistence.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Persistence.Interfaces
{
    public interface IProjectRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command,workspace,currentUserService">project,Workspace,ICurrentUser</param>
        /// <returns>string</returns>
        Task<Project> CreateAsync(Project project);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId">GUID</param>
        /// <returns>Project</returns>
        Task<Project> GetAsync(Guid projectId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query,currentUserService,project">UpdateProjectCommand,ICurrentUserService,Project</param>
        /// <returns>Unit</returns>
        Task<Project> UpdateAsync(Project project, ICurrentUser currentUserService);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query,currentUserService">DeleteProjectCommand, ICurrentUserService</param>
        /// <returns>DeleteProjectDto</returns>
        Task<string> DeleteAsync(Project project);
    }
}
