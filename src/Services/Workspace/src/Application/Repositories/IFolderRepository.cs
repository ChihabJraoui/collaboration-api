using Application.Common.Interfaces;
using Application.Folder.Commands;
using Application.Folder.Commands.DeleteFolder;
using Application.Folder.Commands.UpdateFolder;
using Application.Folder.Queries;
using Application.Folder.Queries.GetAllFolder;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IFolderRepository
    {
        /// <summary>
        /// TODO Add dto 
        /// </summary>
        /// <param name="command,component,currentUserService,project">Component,AddFolderCommand,ICurrentUserService,Project</param>
        /// <returns>Core.Entities.Folder</returns>
        Task<Core.Entities.Folder> AddAsync(Core.Commun.Component component,AddFolderCommand command,ICurrentUserService currentUserService,Core.Entities.Project project);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FolderID">Guid</param>
        /// <returns>Core.Entities.Folder</returns>
        Task<Core.Entities.Folder> GetAsync(Guid FolderID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query,currentUserService">DeleteFolderCommand, ICurrentUserService</param>
        /// <returns>DeleteFolderDto</returns>
        Task<DeleteFolderDto> DeleteAsync(DeleteFolderCommand query, ICurrentUserService currentUserService);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FolderID">GetFolderQuery</param>
        /// <returns>Core.Entities.Folder</returns>
        Task<Core.Entities.Folder> GetQueryAsync(GetFolderQuery FolderID);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request,cancellationToken">GetAllFoldersQuery,CancellationToken</param>
        /// <returns>GetFoldersDtoLists</returns>
        Task<GetFoldersDtoLists> GetAllAsync(GetAllFoldersQuery request, CancellationToken cancellationToken);

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
        Task<string> RenameAsync(RenameFolderCommand request, Core.Entities.Folder folder, ICurrentUserService currentUserService,CancellationToken cancellationToken);
    }
}
