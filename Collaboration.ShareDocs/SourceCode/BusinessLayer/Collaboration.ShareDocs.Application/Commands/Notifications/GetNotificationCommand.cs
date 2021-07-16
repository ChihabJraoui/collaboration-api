using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.Notifications.Dto;
using Collaboration.ShareDocs.Application.Common.Response;
using Collaboration.ShareDocs.Persistence.Entities;
using Collaboration.ShareDocs.Persistence.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Application.Commands.Notifications
{
    public class GetNotificationCommand:IRequest<ApiResponseDetails>
    {
        public class Handler : IRequestHandler<GetNotificationCommand, ApiResponseDetails>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly INotificationRepository _notificationRepository;
            private readonly ICurrentUserService _currentUserService;

            public Handler(IUnitOfWork unitOfWork,
                IMapper mapper,
                INotificationRepository notificationRepository,
                UserManager<ApplicationUser> userManager,
                ICurrentUserService currentUserService)
            {
                this._unitOfWork = unitOfWork;
                _mapper = mapper;
                _notificationRepository = notificationRepository;
                _currentUserService = currentUserService;
            }
            public async Task<ApiResponseDetails> Handle(GetNotificationCommand request, CancellationToken cancellationToken)
            {
                var notifications = await _unitOfWork.UserNotificationRepository.GetUserNotifications(_currentUserService.UserId, cancellationToken);
                
                var countNoti = notifications.Count;
                var notificationDto = _mapper.Map<List<NotificationDto>>(notifications);
                var response = new NotifDto
                {
                    Notification = notificationDto,
                    NotificationCount = countNoti
                };
                return ApiCustomResponse.ReturnedObject(response);
            }
        }
    }
}
