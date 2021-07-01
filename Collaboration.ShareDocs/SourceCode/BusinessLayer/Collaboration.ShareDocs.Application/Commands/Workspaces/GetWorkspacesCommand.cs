using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.Workspaces.Dto;
using Collaboration.ShareDocs.Application.Common.Response;
using Collaboration.ShareDocs.Persistence.Entities;
using Collaboration.ShareDocs.Persistence.Interfaces;
using Collaboration.ShareDocs.Resources;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Application.Commands.Workspaces
{
    public class GetWorkspacesCommand:IRequest<ApiResponseDetails>
    {
        public string Keyword { get; set; } 
        public class Handler : IRequestHandler<GetWorkspacesCommand, ApiResponseDetails>
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

            public async Task<ApiResponseDetails> Handle(GetWorkspacesCommand request, CancellationToken cancellationToken)
            {
                var workspaces =new List<Workspace>();
                if(request.Keyword==null)
                {
                    workspaces = await _unitOfWork.WorkspaceRepository.GetAllAsync(cancellationToken);
                }
                else
                {
                    workspaces = await _unitOfWork.WorkspaceRepository.GetByKeyWord(request.Keyword,cancellationToken);
                }
                 

                if(workspaces.Count == 0)
                {
                    var responseEmpty = new List<Workspace>();
                    return ApiCustomResponse.ReturnedObject(responseEmpty);
                }

                var response = _mapper.Map<List<WorkspaceDto>>(workspaces);
                return ApiCustomResponse.ReturnedObject(response);


            }
        }
    }
}
