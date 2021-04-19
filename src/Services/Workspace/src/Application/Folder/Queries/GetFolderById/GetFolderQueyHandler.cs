using Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Folder.Queries
{
    public class GetFolderQueyHandler : IRequestHandler<GetFolderQuery, Core.Entities.Folder>
    {
        private readonly IFolderRepository _folderRepository;

        public GetFolderQueyHandler(IFolderRepository folderRepository)
        {
            _folderRepository = folderRepository;
        }
        public async Task<Core.Entities.Folder> Handle(GetFolderQuery request, CancellationToken cancellationToken)
        {
            return await _folderRepository.GetQueryAsync(request);
        }
    }
}
