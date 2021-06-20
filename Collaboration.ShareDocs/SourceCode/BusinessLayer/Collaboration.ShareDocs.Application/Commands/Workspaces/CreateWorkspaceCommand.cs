﻿using AutoMapper;
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
            public async Task<ApiResponseDetails> Handle(CreateWorkspaceCommand request, CancellationToken cancellationToken)
            {

                var workspaceRepository = _unitOfWork.WorkspaceRepository;
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

                var workspace = await workspaceRepository.CreateAsync(newWorkspace, cancellationToken);
                var res = await _unitOfWork.SaveChangesAsync(cancellationToken);
                var notification = new Notification
                {
                    Text = $"{workspace.Name} has created by { _currentUserService.UserName} ",
                    Category = Persistence.Enums.Category.newFile
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
                var response = _mapper.Map<WorkspaceDto>(workspace);
                return ApiCustomResponse.ReturnedObject(response);
            }
        }
    }
}
