using Collaboration.ShareDocs.Persistence.Entities;
using Collaboration.ShareDocs.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Persistence.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly AppDbContext _context;

        public FileRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<File> AddAsync(string FileName, string pathFile, Folder folder)
        {
            var file = new File(FileName)
            {
                Parent = folder,
                FilePath = pathFile
            };

            //folder.FolderId
            await _context.Files.AddAsync(file);
            await _context.SaveChangesAsync();

            return file;
        }

        public async Task<File> GetAsync(Guid fileId)
        {
            return await _context.Files.Where(f => f.FileId == fileId).FirstOrDefaultAsync();
        }
    }
}
