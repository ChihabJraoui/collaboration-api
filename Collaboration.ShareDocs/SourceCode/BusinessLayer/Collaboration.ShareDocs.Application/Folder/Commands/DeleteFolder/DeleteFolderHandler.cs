using Application.Common.Interfaces;
using Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Folder.Commands.DeleteFolder
{
    public class DeleteFolderHandler : IRequestHandler<DeleteFolderCommand, DeleteFolderDto>
    {
        private readonly IFolderRepository _folderRepository;
        private readonly ICurrentUserService _currentUserService;

        public DeleteFolderHandler(IFolderRepository folderRepository,ICurrentUserService currentUserService)
        {
            _folderRepository = folderRepository;
            _currentUserService = currentUserService;

        }
        public async Task<DeleteFolderDto> Handle(DeleteFolderCommand request, CancellationToken cancellationToken)
        {
            return await _folderRepository.DeleteAsync(request, _currentUserService);
        }
    }
}
