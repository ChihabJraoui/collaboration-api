using Collaboration.ShareDocs.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Persistence.Repositories
{
    public class MethodesRepository : IMethodesRepository
    {
        private readonly AppDbContext _context;


        public MethodesRepository(AppDbContext context)
        {
            this._context = context;
        }

        
        /// <summary>
        /// Unique
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="name"></param>
        /// <param name="propertyName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> Unique<TEntity>(string name, string propertyName, CancellationToken cancellationToken) where TEntity : class
        {
            var dbSet = _context.Set<TEntity>();


            ParameterExpression argParam = Expression.Parameter(typeof(TEntity), "s");
            Expression nameProperty = Expression.Property(argParam, propertyName);

            var val1 = Expression.Constant(name);
            Expression e1 = Expression.NotEqual(nameProperty, val1);

            var lambda = Expression.Lambda<Func<TEntity, bool>>(e1, argParam);


            return await dbSet.AllAsync(lambda, cancellationToken);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="name"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> UniqueName<TEntity>(string name, CancellationToken cancellationToken) where TEntity : class
        { 
            return  await Unique<TEntity>(name, "Name", cancellationToken);
        }
    }
}
