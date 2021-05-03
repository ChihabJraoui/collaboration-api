using AutoMapper;
using Collaboration.ShareDocs.Application.Common.Response;
using Collaboration.ShareDocs.Persistence.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Application.Commands.Workspaces
{
    public class GetWorkspacesCount:IRequest<ApiResponseDetails>
    {
        public class Handler : IRequestHandler<GetWorkspacesCount, ApiResponseDetails>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ICurrentUserService _currentUserService;
            private readonly IMapper _mapper;
            public Handler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _currentUserService = currentUserService;
                _mapper = mapper;
            }

            public async Task<ApiResponseDetails> Handle(GetWorkspacesCount request, CancellationToken cancellationToken)
            {
               var response = await _unitOfWork.WorkspaceRepository.GetCount(cancellationToken);
                return ApiCustomResponse.ReturnedObject(response);
            }
        }
        }
}
