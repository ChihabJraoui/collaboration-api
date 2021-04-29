using Collaboration.ShareDocs.Persistence.Entities;
using Collaboration.ShareDocs.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Persistence.Repositories
{
    public class WorkspaceRepository : GenericRepository<Workspace>, IWorkspaceRepository
    {
        private readonly AppDbContext _context;

        public WorkspaceRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Workspace> CreateAsync(Workspace workspace, CancellationToken cancellationToken)
        { 
            var newWorkspace = await dbSet.AddAsync(workspace, cancellationToken); 
            return newWorkspace.Entity;
        }



        public async Task<bool> DeleteAsync(Workspace workspace, CancellationToken cancellationToken)
        { 
            this.dbSet.Remove(workspace);
            return true;
        }



        public async Task<List<Workspace>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Workspaces.OrderByDescending(n => n.Created).ToListAsync(cancellationToken);
        }

        public async Task<Workspace> GetAsync(Guid workspaceId, CancellationToken cancellationToken)
        {
            var query = dbSet.Where(w => w.Id == workspaceId)
               .Include(w => w.Projects).OrderBy(n => n.Created);
            var Workspace = await query.FirstOrDefaultAsync(cancellationToken);
            return Workspace;
        }

        public async Task<List<Workspace>> GetByKeyWord(string keyWord)
        {
            return await dbSet.Where(w => w.Name.Contains(keyWord)).ToListAsync();
        }

        public async Task<int> GetCount()
        {
            return await dbSet.CountAsync();
        }

        public async Task<Workspace> GetLastAsync(CancellationToken cancellationToken)
        {
            var lastWorkspace = await dbSet.OrderByDescending(w => w.Created).ToListAsync(cancellationToken);  
            return lastWorkspace.FirstOrDefault();
        }

        public async Task<Workspace> GetLastModifiedAsync(CancellationToken cancellationToken)
        {
            var lastModifiedWorkspace = await dbSet.OrderByDescending(w => w.LastModified).ToListAsync(cancellationToken);
            return lastModifiedWorkspace.FirstOrDefault();
        }

        public async Task<Workspace> UpdateAsync(Workspace workspace, CancellationToken cancellationToken)
        {

            var workspaceData = await GetAsync(workspace.Id, cancellationToken);

            workspaceData.Name = workspace.Name;
            workspaceData.Description = workspace.Description;
            workspaceData.Image = workspace.Image;
            workspaceData.BookMark = workspace.BookMark;
            workspaceData.IsPrivate = workspace.IsPrivate;
            //LastModifiedBy TO DO ?!
            await this._context.SaveChangesAsync(cancellationToken);

            return workspaceData;
        }

    }
}
