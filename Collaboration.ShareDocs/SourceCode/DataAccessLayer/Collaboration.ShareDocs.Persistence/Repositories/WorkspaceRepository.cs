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

        public async Task<List<Workspace>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await dbSet.OrderByDescending(n => n.Created).ToListAsync(cancellationToken);
        }

        public async Task<List<Workspace>> GetByKeyWord(string keyWord, CancellationToken cancellationToken)
        {
            return await dbSet.Where(w => w.Name.Contains(keyWord)).ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Get workspace details
        /// </summary>
        /// <param name="workspaceId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Workspace> GetAsync(Guid workspaceId, CancellationToken cancellationToken)
        {
            var query = dbSet.Where(w => w.Id == workspaceId)
                .Include(w => w.Projects)
                .OrderBy(n => n.Created);

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Workspace> CreateAsync(Workspace workspace, CancellationToken cancellationToken)
        {
            var newWorkspace = await InsertAsync(workspace, cancellationToken);

            return newWorkspace.Entity;
        }


        public bool Delete(Workspace workspace)
        {
            return base.Delete(workspace);
        }

        public async Task<int> GetCount(CancellationToken cancellationToken)
        {
            return await dbSet.CountAsync(cancellationToken);
        }

        public async Task<Workspace> GetLastAsync(CancellationToken cancellationToken)
        {
            var lastWorkspace = await dbSet.Where(x => x.Created != null).OrderByDescending(w => w.Created).ToListAsync(cancellationToken);
            return lastWorkspace.FirstOrDefault();
        }

        public async Task<Workspace> GetLastModifiedAsync(CancellationToken cancellationToken)
        {
            var lastModifiedWorkspace = await dbSet.Where(x=>x.LastModified != null).OrderByDescending(w => w.LastModified).ToListAsync(cancellationToken);
            return lastModifiedWorkspace.FirstOrDefault();
        }

        /// <summary>
        /// Update one workspace
        /// </summary>
        /// <param name="workspace"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
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
