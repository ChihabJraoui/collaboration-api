﻿using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.Files.Dto;
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

namespace Collaboration.ShareDocs.Application.Commands.Files
{
    public class CreateFileCommand:IRequest<ApiResponseDetails>
    {
        public string Name { get; set; }
        public Guid FolderParentId { get; set; }
        public string PathFile { get; set; }
        public string Extension { get; set; }

        public class Handler : IRequestHandler<CreateFileCommand, ApiResponseDetails>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly ICurrentUserService _currentUserService;
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService, UserManager<ApplicationUser> userManager)
            {
                this._unitOfWork = unitOfWork;
                _mapper = mapper;
                _currentUserService = currentUserService;
                _userManager = userManager;
            }
            public async Task<ApiResponseDetails> Handle(CreateFileCommand request, CancellationToken cancellationToken)
            {
                var folder = await _unitOfWork.FolderRepository.GetAsync(request.FolderParentId, cancellationToken);
                if(folder == null)
                {
                    var message = string.Format(Resource.Error_NotFound, request.FolderParentId);
                    return ApiCustomResponse.NotFound(message);
                }
                var newFile = new File(request.Name)
                {
                    Name = request.Name,
                    FilePath = request.PathFile,
                    Parent = folder,
                    Extension=request.Extension
                };
                var file = await _unitOfWork.FileRepository.AddAsync(newFile, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                //
                var username = await this._userManager.FindByIdAsync(_currentUserService.UserId);


                var notification = new Notification
                {
                    Text = $"{username.UserName} has shared {file.Name}",
                    Category = Persistence.Enums.Category.newFile
                };
                //followingUsers
                var followingUsers = await _unitOfWork.FollowRepository.GetFollowings(new Guid(_currentUserService.UserId), cancellationToken);

                if (followingUsers == null)
                {
                    var message = string.Format(Resource.Error_NotFound, _currentUserService.UserId);
                    return ApiCustomResponse.NotFound(message);
                }

                await _unitOfWork.NotificationRepository.Create(notification, new Guid(_currentUserService.UserId), cancellationToken);

                await _unitOfWork.UserNotificationRepository.AssignNotificationToTheUsers(notification, followingUsers, cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                
                var response = _mapper.Map<FileDto>(file);
                return ApiCustomResponse.ReturnedObject(response);
            }
        }
    }
}
