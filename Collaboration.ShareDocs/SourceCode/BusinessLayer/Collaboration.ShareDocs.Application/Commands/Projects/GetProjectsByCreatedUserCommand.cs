using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.Projects.Dto;
using Collaboration.ShareDocs.Application.Common.Response;
using Collaboration.ShareDocs.Persistence.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Application.Commands.Projects
{
    public class GetProjectsByCreatedUserCommand:IRequest<ApiResponseDetails>
    {
        public Guid UserId { get; set; }

        public class Handler : IRequestHandler<GetProjectsByCreatedUserCommand, ApiResponseDetails>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ICurrentUserService _currentUserService;
            private readonly IMapper _mapper;
            public Handler(IUnitOfWork unitOfWork,
                                         ICurrentUserService currentUserService,
                                         IMapper mapper)
            {
                this._unitOfWork = unitOfWork;
                this._currentUserService = currentUserService;
                _mapper = mapper;
            }
            public async Task<ApiResponseDetails> Handle(GetProjectsByCreatedUserCommand request, CancellationToken cancellationToken)
            {
                
                var projects = await _unitOfWork.ProjectRepository.GetByCreatedAsync(request.UserId, cancellationToken);
                var response = _mapper.Map<List<ProjectDto>>(projects);
                return ApiCustomResponse.ReturnedObject(response);
            }
        }
    }
}
