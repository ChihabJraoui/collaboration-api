using Application.Common.Interfaces;
using Application.Workspace.Commands;
using Application.Workspace.Commands.DeleteWorkspace;
using Application.Workspace.Commands.UpdateWorkspace;
using Application.Workspace.Queries.GetAllWorkspaces;
using Application.Workspace.Queries.GetLastModifiedWorkspace;
using Application.Workspace.Queries.GetLastWorkspace;
using Application.Workspace.Queries.GetWorkspace;
using Application.Workspace.Queries.GetWorkspaceByKeyWord;
using Application.Workspace.Queries.GetWorkspacesCount;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IWorkspaceRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="workspace,currentUser">CreateWorkspaceCommand,ICurrentUserService</param>
        /// <returns>Guid</returns>
        Task<Guid> CreateAsync(CreateWorkspaceCommand workspace, ICurrentUserService currentUser);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="workspaceId">GetWorkspaceByIdQuery</param>
        /// <returns>Workspace Entity "//TODO dto"</returns>
        Task<Core.Entities.Workspace> GetAsync(GetWorkspaceByIdQuery workspaceId);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="request,cancellationToken">GetAllWorkspaceQuery,CancellationToken</param>
        /// <returns>WorkspacesDTOLists</returns>
        Task<WorkspacesDTOLists> GetAllAsync(GetAllWorkspaceQuery request, CancellationToken cancellationToken);



        /// <summary>
        /// 
        /// </summary>
        /// <param name="request,cancellationToken">GetLastWorkspaceQuery,CancellationToken</param>
        /// <returns>LastWorkspaceDto</returns>
        Task<LastWorkspaceDto> GetLastAsync(GetLastWorkspaceQuery request, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request,cancellationToken">GetLastWorkspaceModifiedQuery,CancellationToken</param>
        /// <returns>LastWorkspaceDto</returns>
        Task<WorkspaceLastModifiedDto> GetLastModifiedAsync(GetLastModifiedWorkspaceQuery request, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workspace">UpdateWorkspaceCommand</param>
        /// <returns>Workspace</returns>
        Task<Core.Entities.Workspace> UpdataAsync(UpdateWorkspaceCommand workspace, ICurrentUserService currentUser, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workspaceId,currentUserService,cancellationToken">DeleteWorkspaceCommad</param>
        /// <returns>DeleteWorkspaceReturnDto</returns>
        Task<DeleteWorkspaceReturnDto> DeleteAsync(DeleteWorkspaceCommad workspaceId, ICurrentUserService currentUserService, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyWord">GetWorkspaceByKeyWord</param>
        /// <returns>WorkspacesDTOLists</returns>
        Task<WorkspaceDtoLists> GetByKeyWord(GetWorkspaceByKeyWord keyWord);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="">GetWorkspacesCount</param>
        /// <returns>int</returns>
        Task<int> GetCount(GetWorkspacesCountQuery request);
    }
}
