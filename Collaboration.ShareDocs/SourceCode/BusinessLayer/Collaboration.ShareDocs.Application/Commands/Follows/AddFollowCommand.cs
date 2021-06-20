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
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly INotificationRepository _notificationRepository;

            public Handler(IUnitOfWork unitOfWork,
                UserManager<ApplicationUser> userManager,
                ICurrentUserService currentUserService,
                INotificationRepository notificationRepository,
                IMapper mapper)
            {
                this._unitOfWork = unitOfWork;
                this._currentUserService = currentUserService;
                _mapper = mapper;
                this._userManager = userManager;
                this._notificationRepository = notificationRepository;
            }
            public async Task<ApiResponseDetails> Handle(AddFollowCommand request, CancellationToken cancellationToken)
            {
                var user = await this._userManager.Users.SingleOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

                if (user == null)
                {
                    var message = string.Format(Resource.Error_NotFound, request.UserId);
                    return ApiCustomResponse.NotFound(message);
                }
                if (await _unitOfWork.FollowRepository.IsFollowing(user.Id, _currentUserService.UserId) != null)
                {
                    var message = string.Format(Resource.Error_NotValid, request.UserId);
                    return ApiCustomResponse.NotFound(message);
                }

                var follow = new Follow()
                {
                    FollowerId = new Guid(_currentUserService.UserId),
                    FollowingId = user.Id
                };
                var me = await this._userManager.Users.SingleOrDefaultAsync(u => u.Id == new Guid(_currentUserService.UserId), cancellationToken);
                
                var follower = await _unitOfWork.FollowRepository.CreateAsync(follow,cancellationToken);
                me.Followers.Add(follower);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                var response = _mapper.Map<FollowDto>(follower);

                var notification = new Notification
                {
                    Text = $"you had followed by {_currentUserService.UserName}",
                    Category = Persistence.Enums.Category.FollowEvent
                };
                await _unitOfWork.NotificationRepository.Create(notification, new Guid(_currentUserService.UserId), cancellationToken);
                await _unitOfWork.UserNotificationRepository.AssignNotificationToTheUser(notification, user.Id.ToString(), cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return ApiCustomResponse.ReturnedObject(response);
            }
        }
    }
}
