using Collaboration.ShareDocs.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Persistence.Interfaces
{
    public interface IFileRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="File"></param>
        /// <returns>File</returns>
        Task<File> AddAsync(File file, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
      

        Task<File> GetAsync(Guid fileId, CancellationToken cancellationToken);

        Task<List<File>> GetByFolderIdAsync(Guid folderId, CancellationToken cancellationToken);
    }
}
