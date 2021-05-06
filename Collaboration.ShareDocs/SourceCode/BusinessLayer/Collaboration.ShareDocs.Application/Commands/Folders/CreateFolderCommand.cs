using Collaboration.ShareDocs.Application.Common.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Application.Commands.Folders
{
    public class CreateFolderCommand:IRequest<ApiResponseDetails>
    {
        public string Name { get; set; }
        public Guid FolderParentId { get; set; }
        public Guid ProjectId { get; set; }

        public class Handler : IRequestHandler<CreateFolderCommand, ApiResponseDetails>
        {
            public async Task<ApiResponseDetails> Handle(CreateFolderCommand request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
