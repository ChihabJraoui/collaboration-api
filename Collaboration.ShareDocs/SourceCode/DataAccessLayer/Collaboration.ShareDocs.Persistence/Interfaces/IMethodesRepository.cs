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
        Task<bool> UniqueName<TEntity>(string name, CancellationToken cancellationToken) where TEntity : class;


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="name"></param>
        /// <param name="propertyName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns> 
        Task<bool> Unique<TEntity>( string name, string propertyName, CancellationToken cancellationToken )
            where TEntity : class;
    }
}
