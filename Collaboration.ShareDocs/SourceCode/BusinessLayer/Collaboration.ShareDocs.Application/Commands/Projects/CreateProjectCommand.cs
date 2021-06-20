using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.Projects.Dto;
using Collaboration.ShareDocs.Application.Common.Exceptions;
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

namespace Collaboration.ShareDocs.Application.Commands.Projects
{
    public class CreateProjectCommand:IRequest<ApiResponseDetails>
    {
        public string Label { get; set; }
        public string Description { get; set; }
        public Guid WorkspaceId { get; set; }

        public class Handler : IRequestHandler<CreateProjectCommand, ApiResponseDetails>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ICurrentUserService _currentUserService;
            private readonly IMapper _mapper;
            public Handler(IUnitOfWork unitOfWork,ICurrentUserService currentUserService,
                                         IMapper mapper)
            {
                this._unitOfWork = unitOfWork;
                this._currentUserService = currentUserService;
                _mapper = mapper;
            }
            public async Task<ApiResponseDetails> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
            {
                var workspace = await _unitOfWork.WorkspaceRepository.GetAsync(request.WorkspaceId, cancellationToken);
                
                if(workspace == null)
                {
                    var message = string.Format(Resource.Error_NotFound, request.Label);
                    return ApiCustomResponse.NotFound(message);
                }
                
                if (!await _unitOfWork.MethodRepository.Unique<Project>(request.Label,"Label", cancellationToken))
                {
                    var message = string.Format(Resource.Error_NameExist, request.Label);
                    return ApiCustomResponse.ValidationError(new Error("Label", message));
                }

                var newProject = new Project()
                {
                    Label = request.Label,
                    Description = request.Description,
                    Workspace = workspace
                };

                await _unitOfWork.ProjectRepository.CreateAsync(newProject, cancellationToken);
                
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                var notification = new Notification
                {
                    Text = $"{ _currentUserService.UserId} has shared {newProject.Label} in the {newProject.Workspace.Name}",
                    Category = Persistence.Enums.Category.project
                };
                //followingUsers
                var followingUsers = await _unitOfWork.FollowRepository.GetFollowing(new Guid(_currentUserService.UserId), cancellationToken);
                if (followingUsers == null)
                {
                    var message = string.Format(Resource.Error_NotFound, _currentUserService.UserId);
                    return ApiCustomResponse.NotFound(message);
                }
                await _unitOfWork.NotificationRepository.Create(notification, new Guid(_currentUserService.UserId), cancellationToken);
                await _unitOfWork.UserNotificationRepository.AssignNotificationToTheUsers(notification, followingUsers, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                var response = _mapper.Map<ProjectDto>(newProject);
                return ApiCustomResponse.ReturnedObject(response);


            }
        }
    }
}
