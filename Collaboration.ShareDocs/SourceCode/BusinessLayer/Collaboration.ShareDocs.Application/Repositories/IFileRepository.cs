using Application.File;

using Application.File.Queries.GetFile;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IFileRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command,folderId">Component,Folder</param>
        /// <returns>Core.Entitiesfile</returns>
        Task<Core.Entities.File> AddAsync(AddFileCommand command,Core.Entities.Folder folder);

        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
       // Task<Unit> Post( PostFileCommand command);

        Task<Core.Entities.File> GetAsync(GetFileQuery query);


    }
}
