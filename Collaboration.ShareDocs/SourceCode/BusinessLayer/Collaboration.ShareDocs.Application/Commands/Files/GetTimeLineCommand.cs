using Collaboration.ShareDocs.Application.Common.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Application.Commands.Files
{
    public class GetTimeLineCommand:IRequest<ApiResponseDetails>
    {
        public class Handler : IRequestHandler<GetTimeLineCommand, ApiResponseDetails>
        {
            public Task<ApiResponseDetails> Handle(GetTimeLineCommand request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
