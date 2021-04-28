using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Persistence.Interfaces
{
    public interface IMethodesRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name,cancellationToken">string,CancellationToken</param>
        /// <returns>Boolean</returns>
        Task<Boolean> UniqueName(string name, CancellationToken cancellationToken);
    }
}
