using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.Projects.Dto;
using Collaboration.ShareDocs.Application.Commands.Users.Dto;
using Collaboration.ShareDocs.Application.Commands.Folders.Dto;
using Collaboration.ShareDocs.Application.Common.Response;
using Collaboration.ShareDocs.Persistence.Interfaces;
using Collaboration.ShareDocs.Resources;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Application.Commands.Projects
{
    public class GetProjectCommand: IRequest<ApiResponseDetails>
    {
        public Guid ProjectId { get; set; }

        public class Handler : IRequestHandler<GetProjectCommand, ApiResponseDetails>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ICurrentUserService _currentUserService;
            private readonly IMethodesRepository _methodesRepository;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork,
                                         ICurrentUserService currentUserService,
                                         IMethodesRepository methodesRepository,
                                         IMapper mapper)
            {
                this._unitOfWork = unitOfWork;
                this._currentUserService = currentUserService;
                this._methodesRepository = methodesRepository;
                _mapper = mapper;
            }

            public async Task<ApiResponseDetails> Handle(GetProjectCommand request, CancellationToken cancellationToken)
            {
                var project = await _unitOfWork.ProjectRepository.GetAsync(request.ProjectId, cancellationToken);

                if (project == null)
                {
                    var message = string.Format(Resource.Error_NotFound,project, request.ProjectId);
                    return ApiCustomResponse.NotFound(message);
                }

                var users = await _unitOfWork.UserProjectRepository.GetUsers(request.ProjectId, cancellationToken);
                var folders = await _unitOfWork.FolderRepository.GetByProjectIdAsync(request.ProjectId, cancellationToken);

                var response = _mapper.Map<ProjectDetailsDto>(project); 
                response.Users = _mapper.Map<ICollection<UserProfileDto>>(users);
                response.Folders = _mapper.Map<ICollection<FolderDto>>(folders);

                return ApiCustomResponse.ReturnedObject(response);
            }
        }
    }
}