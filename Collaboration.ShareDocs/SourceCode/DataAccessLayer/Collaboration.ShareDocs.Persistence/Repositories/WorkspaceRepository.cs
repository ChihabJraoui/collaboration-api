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
    public class WorkspaceRepository : IWorkspaceRepository
    {
        private readonly AppDbContext _context;

        public WorkspaceRepository(AppDbContext appDbcontext)
        {
            _context = appDbcontext;
        }
        public async Task<Guid> CreateAsync(Workspace workspace)
        {
            var newworkspace=await _context.Workspaces.AddAsync(workspace);
            await _context.SaveChangesAsync();
            return newworkspace.Entity.Id;
        }

        public async Task<string> DeleteAsync(Guid workspaceId, CancellationToken cancellationToken)
        {
            var entity = await _context.Workspaces
                .Where(e => e.Id == workspaceId)
                .SingleOrDefaultAsync(cancellationToken)
                ;
            _context.Workspaces.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

           
            return entity.DeletedBy;
        }
    
        public async Task <List<Workspace>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Workspaces.OrderByDescending(n => n.Created).ToListAsync(cancellationToken);
        }

        public async Task<Workspace> GetAsync(Guid workspaceId)
        {
            var query =  _context.Workspaces.Where(w => w.Id ==workspaceId)
               .Include(w => w.Projects).OrderBy(n => n.Created);
            var Workspace = await query.FirstOrDefaultAsync();
            return Workspace;
        }

        public async Task<List<Workspace>> GetByKeyWord(string keyWord)
        {
            return  await _context.Workspaces.Where(w => w.Name.Contains(keyWord)).ToListAsync();
        }

        public async Task<int> GetCount()
        {
            return await _context.Workspaces.CountAsync();
        }

        public async Task<Workspace> GetLastAsync(CancellationToken cancellationToken)
        {
            var lastWorkspace = await _context.Workspaces
               .OrderByDescending(w => w.Created).ToListAsync(cancellationToken); //this conce
            return lastWorkspace.FirstOrDefault();
        }

        public async Task<Workspace> GetLastModifiedAsync(CancellationToken cancellationToken)
        {
            var lastModifiedWorkspace = await _context.Workspaces
                .OrderByDescending(w => w.LastModified).ToListAsync(cancellationToken);
            return lastModifiedWorkspace.FirstOrDefault();
        }

        public async Task<Workspace> UpdataAsync(Workspace workspace, CancellationToken cancellationToken)
        {
           
            var workspaceData = await GetAsync(workspace.Id);
            
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
