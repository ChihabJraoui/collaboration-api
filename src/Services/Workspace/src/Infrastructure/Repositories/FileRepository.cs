using Application.File;

using Application.File.Queries.GetFile;
using Application.Repositories;
using AutoMapper.Configuration;
using Core.Commun;
using Core.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = Core.Entities.File;

namespace Infrastructure.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IFolderRepository _folderRepository;
        private string[] permittedExtensions = { ".txt", ".pdf" };

        public FileRepository(ApplicationDbContext applicationDbContext,IFolderRepository folderRepository)
        {
            _context = applicationDbContext;
            _folderRepository = folderRepository;


        }
        public async Task<File> AddAsync(AddFileCommand command, Folder folder)
        {
            var file = new File(command.Name)
            {
                Parent = folder,
                FilePath = command.PathFile
            };

            //folder.FolderId
            await _context.Files.AddAsync(file);
            await _context.SaveChangesAsync();

            return file;
        }

        public async Task<File> GetAsync(GetFileQuery query)
        {
           
           return await _context.Files.Where(f => f.FileId == query.FileId).FirstOrDefaultAsync();
     
        }

        //public async Task<Unit> Post( PostFileCommand command)
        //{

        //    var size = command.Files.Sum(f => f.Length);
        //    foreach(var formFile in command.Files)
        //    {
        //        var filePath = Path.GetTempFileName();
        //        var stream = System.IO.File.Create(filePath);

        //        await formFile.CopyToAsync(stream);




        //         var ext = Path.GetExtension(filePath).ToLowerInvariant();

        //        if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
        //        {
        //            // The extension is invalid ... discontinue processing the file
        //        }

        //        var file = new File(filePath)
        //        {
        //            Parent = await _folderRepository.GetAsync(command.FolderParentId)
        //        };

        //        await _context.Files.AddAsync(file);
        //        await _context.SaveChangesAsync();
        //    }


        //    return Unit.Value;


        //}

    }
}