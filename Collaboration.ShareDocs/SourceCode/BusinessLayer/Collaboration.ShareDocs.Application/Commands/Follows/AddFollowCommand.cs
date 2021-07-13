using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.Follows.Dto;
using Collaboration.ShareDocs.Application.Common.Response;
using Collaboration.ShareDocs.Persistence.Entities;
using Collaboration.ShareDocs.Persistence.Interfaces;
using Collaboration.ShareDocs.Resources;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Application.Commands.Follows
{
    public class AddFollowCommand : IRequest<ApiResponseDetails>
    {
        public Guid UserId { get; set; }

        public class Handler : IRequestHandler<AddFollowCommand, ApiResponseDetails>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ICurrentUserService _currentUserService;
            private readonly IMapper _mapper;
            private readonly IFollowRepository _followRepository;
            private readonly IUserRepository _userRepository;
            
            private readonly INotificationRepository _notificationRepository;

            public Handler(IUnitOfWork unitOfWork,
                IUserRepository userRepository,
                IFollowRepository followRepository,
                ICurrentUserService currentUserService,
                INotificationRepository notificationRepository,
                IMapper mapper)
            {
                this._unitOfWork = unitOfWork;
                this._currentUserService = currentUserService;
                _mapper = mapper;
                _followRepository = followRepository;
                this._userRepository = userRepository;
                this._notificationRepository = notificationRepository;
            }
            public async Task<ApiResponseDetails> Handle(AddFollowCommand request, CancellationToken cancellationToken)
            {
                //BR1 
                var user = await _userRepository.GetUser(request.UserId, cancellationToken);

                if (user == null)
                {
                    var message = string.Format(Resource.Error_NotFound, request.UserId);
                    return ApiCustomResponse.NotFound(message);
                }
                
                //BR2
                if (user.Id.ToString() == _currentUserService.UserId)
                {
                    var message = string.Format(Resource.Error_NotValid, request.UserId);
                    return ApiCustomResponse.NotFound(message);
                }

                //BR3 
                var isFollowing = await _followRepository.GetFollowings(new Guid(_currentUserService.UserId), cancellationToken);
                if ( isFollowing.Contains(user) == true)
                {
                    var message = string.Format(Resource.Error_IsAleradyExist, request.UserId);
                    return ApiCustomResponse.NotFound(message);
                }
               

                var me = await _userRepository.GetUser(new Guid(_currentUserService.UserId), cancellationToken);

                me.Followings.Add(user);
                user.Followers.Add(me);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                //NOTIFICATION 
                var notification = new Notification
                {
                    Text = $"you had followed by {me.UserName}",
                    Category = Persistence.Enums.Category.FollowEvent
                };
                //
                await _unitOfWork.NotificationRepository.Create(notification, new Guid(_currentUserService.UserId), cancellationToken);
                await _unitOfWork.UserNotificationRepository.AssignNotificationToTheUser(notification, user.Id.ToString(), cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return ApiCustomResponse.ReturnedObject();
            }
        }
    }
}
