using Collaboration.ShareDocs.Persistence.Common;
using Collaboration.ShareDocs.Persistence.Entities;
using Collaboration.ShareDocs.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Collaboration.ShareDocs.Persistence.Repositories
{
    public class FolderRepository : GenericRepository<Folder>, IFolderRepository
    {
        private readonly AppDbContext _context;

        public FolderRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Folder> CreateFolderChild(Folder folder, Folder folderParent, Project project, Component component, CancellationToken cancellationToken)
        {
            var folderChild = new Folder(component.Name)
            {
                Parent = folderParent,
                Project = project,
            };
            if (folderParent.FolderId.Equals(Guid.Empty))
            {
                folderParent = folder;
            }
            //component.Parent = parentRoot;
            folderParent.Components.Add(folder);

            await base.InsertAsync(folder, cancellationToken);

            return folderChild;

        }
        public async Task<Folder> CreateAsync(Folder obj, CancellationToken cancellationToken)
        {
            await base.InsertAsync(obj, cancellationToken);

            return obj;
        }

        public bool Delete(Folder folder) => base.Delete(folder);

        public async Task<List<Folder>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await dbSet.OrderByDescending(n => n.Created).ToListAsync(cancellationToken);
        }

        public async Task<Folder> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            var Folder = await dbSet.Where(w => w.FolderId == id).Include(y => y.Parent).SingleOrDefaultAsync();

            return Folder;
        }
        public async Task<List<Folder>> GetByProjectIdAsync(Guid projectId, CancellationToken cancellationToken)
        {
            return await dbSet.Where(w => w.Project.Id == projectId).ToListAsync(cancellationToken);
        }
        public async Task<List<Folder>> GetByCreatedAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await dbSet.Where(w => new Guid(w.CreatedBy) == userId).ToListAsync(cancellationToken);
        }
        public async Task<Folder> GetQueryAsync(Guid FolderID)
        {
            var Folder = await dbSet.Where(w => w.FolderId == FolderID).Include(y => y.Components).Include(y => y.Files).SingleOrDefaultAsync();

            return Folder;
        }
        public async Task<string> RenameAsync(Folder folder, string name, CancellationToken cancellationToken)
        {
            folder.Name = name;
            await _context.SaveChangesAsync(cancellationToken);
            return folder.Name;
        }

        public async Task<bool> UniqueName(string name)
        {
            return await dbSet.AllAsync(n => n.Name != name);
        }

        public Task<Folder> UpdateAsync(Folder obj, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
