using Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Folder.Queries.GetAllFolder
{
    public class GetAllFolderHandler : IRequestHandler<GetAllFoldersQuery, GetFoldersDtoLists>
    {
        private readonly IFolderRepository _folderRepository;

        public GetAllFolderHandler(IFolderRepository folderRepository)
        {
            _folderRepository = folderRepository;
        }
        public async Task<GetFoldersDtoLists> Handle(GetAllFoldersQuery request, CancellationToken cancellationToken)
        {
            return await _folderRepository.GetAllAsync(request, cancellationToken);
        }
    }
}
