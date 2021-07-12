using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.Workspaces.Dto;
using Collaboration.ShareDocs.Application.Common.Exceptions;
using Collaboration.ShareDocs.Application.Common.Response;
using Collaboration.ShareDocs.Persistence.Entities;
using Collaboration.ShareDocs.Persistence.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Collaboration.ShareDocs.Resources;
using System;
using Microsoft.AspNetCore.Identity;

namespace Collaboration.ShareDocs.Application.Commands.Workspaces
{
    public class CreateWorkspaceCommand : IRequest<ApiResponseDetails>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }


        public class Handler : IRequestHandler<CreateWorkspaceCommand, ApiResponseDetails>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ICurrentUserService _currentUserService;
            private readonly IMethodesRepository _methodesRepository;
            private readonly IMapper _mapper;
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(IUnitOfWork unitOfWork,
                UserManager<ApplicationUser> userManager,
                ICurrentUserService currentUserService,
                IMethodesRepository methodesRepository,
                IMapper mapper)
            {
                this._unitOfWork = unitOfWork;
                this._currentUserService = currentUserService;
                this._methodesRepository = methodesRepository;
                _mapper = mapper;
                _userManager = userManager;
            }

            public async Task<ApiResponseDetails> Handle(CreateWorkspaceCommand request, CancellationToken cancellationToken)
            {
                // R01 Workspace label is unique
                if (!await _methodesRepository.UniqueName<Workspace>(request.Name, cancellationToken))
                {
                    var message = string.Format(Resource.Error_NameExist,request.Name);
                    return ApiCustomResponse.ValidationError(new Error("Name", message));
                }

                var newWorkspace = new Workspace
                {
                    Name = request.Name,
                    Description = request.Description,
                    Image = request.Image
                };

                var workspace = await _unitOfWork.WorkspaceRepository.CreateAsync(newWorkspace, cancellationToken);

                var currentUser = await this._userManager.FindByIdAsync(_currentUserService.UserId);

                var notification = new Notification
                {
                    Text = $"{workspace.Name} has created by { currentUser.UserName}",
                    Category = Persistence.Enums.Category.newFile
                };

                // Create notification
                await _unitOfWork.NotificationRepository.Create(notification, new Guid(_currentUserService.UserId), cancellationToken);

                // Get followers
                //var followingUsers = await _unitOfWork.FollowRepository.GetFollowings(new Guid(_currentUserService.UserId), cancellationToken);

                //if (followingUsers.Count > 0)
                //{
                //    // Send notifications to followers
                //    //await _unitOfWork.UserNotificationRepository.AssignNotificationToTheUsers(notification, followingUsers, cancellationToken);
                //}

                // Save changes
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                var response = _mapper.Map<WorkspaceDto>(workspace);

                return ApiCustomResponse.ReturnedObject(response);
            }
        }
    }
}
