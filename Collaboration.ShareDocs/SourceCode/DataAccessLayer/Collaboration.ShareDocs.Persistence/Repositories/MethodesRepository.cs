using Collaboration.ShareDocs.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task<bool> UniqueName(string name, CancellationToken cancellationToken)
        {
            return await _context.Workspaces
                .AllAsync(n => n.Name != name,cancellationToken);
        }
    }
}
