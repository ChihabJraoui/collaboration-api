using Application.Common.Exceptions;
using Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.File.Queries.GetFile
{
    public class GetFileQueryHandler : IRequestHandler<GetFileQuery, Core.Entities.File>
    {
        private readonly IFileRepository _fileRepository;

        public GetFileQueryHandler(IFileRepository fileRepository)
        {
            this._fileRepository = fileRepository;
        }
        public async Task<Core.Entities.File> Handle(GetFileQuery request, CancellationToken cancellationToken)
        {
            if (request.FileId.Equals(Guid.Empty)){

                throw new BusinessRuleException("File id is requered");
            }

            return await this._fileRepository.GetAsync(request);

        }
    }
}
