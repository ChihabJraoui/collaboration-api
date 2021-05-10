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
    public class FileRepository : GenericRepository<File>, IFileRepository
    {
        private readonly AppDbContext _context;

        public FileRepository(AppDbContext context):base(context)
        {
            _context = context;
        }
        

        public async Task<File> AddAsync(File file, CancellationToken cancellationToken)
        {
            await base.InsertAsync(file, cancellationToken);

            return file;
        }

        public async Task<File> GetAsync(Guid fileId, CancellationToken cancellationToken)
        {
            return await _context.Files.Where(f => f.FileId == fileId).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<File>> GetByFolderIdAsync(Guid folderId,CancellationToken cancellationToken)
        {
            return await dbSet.Where(w => w.Parent.FolderId == folderId).ToListAsync(cancellationToken);
        }
    }
}
