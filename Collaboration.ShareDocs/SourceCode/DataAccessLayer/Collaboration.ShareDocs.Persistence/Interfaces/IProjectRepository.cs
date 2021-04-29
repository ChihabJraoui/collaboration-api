using Collaboration.ShareDocs.Persistence.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
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
        Task<List<Project>> GetByKeyWordAsync(string keyWord);
    }
}
