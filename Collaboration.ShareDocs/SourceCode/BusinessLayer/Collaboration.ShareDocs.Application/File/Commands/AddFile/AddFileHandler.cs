using Application.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.File
{
    public class AddFileHandler : IRequestHandler<AddFileCommand, Core.Entities.File>
    {
        private readonly IFileRepository _fileRepository;
        private readonly IFolderRepository _folderRepository;

        public AddFileHandler(IFileRepository fileRepository , IFolderRepository folderRepository)
        {
            _fileRepository = fileRepository;
            _folderRepository = folderRepository;

        }
        public async Task<Core.Entities.File> Handle(AddFileCommand request, CancellationToken cancellationToken)
        {
            
            //var file = new Core.Entities.File(request.Name) { 
            //    FilePath=request.PathFile,
            //};
            var folderParent =await _folderRepository.GetAsync(request.FolderParentId);
            return await _fileRepository.AddAsync(request,folderParent);

        }
    }
}
