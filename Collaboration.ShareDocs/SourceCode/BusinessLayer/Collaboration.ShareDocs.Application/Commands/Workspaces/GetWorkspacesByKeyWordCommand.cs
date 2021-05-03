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
    public class GetWorkspacesByKeyWordCommand : IRequest<ApiResponseDetails>
    {
        public string Keyword { get; set; }

        public class Handler : IRequestHandler<GetWorkspacesByKeyWordCommand, ApiResponseDetails>
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

            public async Task<ApiResponseDetails> Handle(GetWorkspacesByKeyWordCommand request, CancellationToken cancellationToken)
            {
                if(request.Keyword == null)
                {
                    return ApiCustomResponse.NotValid(request.Keyword, "not empty", "");
                }
                var response = await _unitOfWork.WorkspaceRepository.GetByKeyWord(request.Keyword, cancellationToken);
                if (response.Count == 0)
                {
                    return ApiCustomResponse.NotFound("There is no Data!");
                }
                return ApiCustomResponse.ReturnedObject(response);

            }
        }
    }
}