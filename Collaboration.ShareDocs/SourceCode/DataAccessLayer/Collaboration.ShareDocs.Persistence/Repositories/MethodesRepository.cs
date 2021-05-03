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

        //public async Task<bool> UniqueName(string name, CancellationToken cancellationToken)
        //{
        //    return await _context.Workspaces
        //        .AllAsync(n => n.Name != name,cancellationToken);
        //}


        public async Task<bool> UniqueName<TEntity>(string name, CancellationToken cancellationToken) where TEntity : class
        {
            var dbSet = _context.Set<TEntity>();


            ParameterExpression argParam     = Expression.Parameter(typeof(TEntity), "s");
            Expression          nameProperty = Expression.Property(argParam, "Name");

            var        val1 = Expression.Constant(name);
            Expression e1   = Expression.NotEqual(nameProperty, val1);

            var lambda = Expression.Lambda<Func<TEntity, bool>>(e1, argParam); 


            return await dbSet.AllAsync(lambda, cancellationToken);
        }
    }
}
