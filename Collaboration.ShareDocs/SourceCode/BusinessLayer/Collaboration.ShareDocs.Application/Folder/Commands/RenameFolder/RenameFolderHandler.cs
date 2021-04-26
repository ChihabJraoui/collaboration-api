using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Folder.Commands.UpdateFolder;
using Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Folder.Commands.RenameFolder
{
    public class RenameFolderHandler : IRequestHandler<RenameFolderCommand, string>
    {
        private readonly IFolderRepository _folderRepository;
        private readonly ICurrentUserService _currentUserService;
        

        public RenameFolderHandler(IFolderRepository folderRepository,ICurrentUserService currentUserService)
        {
            _folderRepository = folderRepository;
            _currentUserService = currentUserService;
           
        }
        public async Task<string> Handle(RenameFolderCommand request, CancellationToken cancellationToken)
        {
            var existe = await _folderRepository.UniqueName(request.Name);
            if (!existe)
            {
                throw new BusinessRuleException("Is already existe");
            };
            var folder = await _folderRepository.GetAsync(request.FolderId);
            if(folder == null)
            {
                throw new BusinessRuleException("Workspace doesn't exist");
            }

            var folderUpdated = await _folderRepository.RenameAsync(request, folder, _currentUserService, cancellationToken);
            return folderUpdated;
        }
    }
}
