using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.Projects.Dto;
using Collaboration.ShareDocs.Application.Common.Exceptions;
using Collaboration.ShareDocs.Application.Common.Response;
using Collaboration.ShareDocs.Persistence.Entities;
using Collaboration.ShareDocs.Persistence.Interfaces;
using Collaboration.ShareDocs.Resources;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Application.Commands.Projects
{
    public class CreateProjectCommand : IRequest<ApiResponseDetails>
    {
        public string Label { get; set; }

        public string Description { get; set; }
        
        public Guid WorkspaceId { get; set; }

        public class Handler : IRequestHandler<CreateProjectCommand, ApiResponseDetails>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ICurrentUserService _currentUserService;
            private readonly IMapper _mapper;
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(IUnitOfWork unitOfWork,ICurrentUserService currentUserService,
                                         IMapper mapper, UserManager<ApplicationUser> userManager)
            {
                this._unitOfWork = unitOfWork;
                this._currentUserService = currentUserService;
                _mapper = mapper;
                _userManager = userManager;
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
                var defaultUser = new List<ApplicationUser>();
                var owner = await this._userManager.FindByIdAsync(_currentUserService.UserId);
                var newProject = new Project()
                {
                    Label = request.Label,
                    Description = request.Description,
                    Workspace = workspace,
                    Icon = "https://ui-avatars.com/api/?background=random&name=" + request.Label,
                    
                };

                await _unitOfWork.ProjectRepository.CreateAsync(newProject, cancellationToken);
                
                await _unitOfWork.SaveChangesAsync(cancellationToken);
               
                var notification = new Notification
                {
                    Text = $"{owner.UserName} has shared {newProject.Label} in the {newProject.Workspace.Name}",
                    Category = Persistence.Enums.Category.project
                };
                // followingUsers
                var followingUsers = await _unitOfWork.FollowRepository.GetFollowings(new Guid(_currentUserService.UserId), cancellationToken);
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
