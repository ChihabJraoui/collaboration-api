using Collaboration.ShareDocs.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Persistence.Interfaces
{
    public interface IFileRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Filename,FolderPath"></param>
        /// <returns>File</returns>
        Task<File> AddAsync(string FileName,string pathFile,Folder folder);

        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
      

        Task<File> GetAsync(Guid fileId);
    }
}
