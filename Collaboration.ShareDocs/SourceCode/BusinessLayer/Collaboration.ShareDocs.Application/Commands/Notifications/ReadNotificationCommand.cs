using AutoMapper;
using Collaboration.ShareDocs.Application.Common.Response;
using Collaboration.ShareDocs.Persistence.Interfaces;
using Collaboration.ShareDocs.Resources;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Application.Commands.Notifications
{
    public class ReadNotificationCommand:IRequest<ApiResponseDetails>
    {
        public Guid NotificationId { get; set; }
        public class Handler : IRequestHandler<ReadNotificationCommand, ApiResponseDetails>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly INotificationRepository _notificationRepository;
            private readonly ICurrentUserService _currentUserService;

            public Handler(IUnitOfWork unitOfWork,
                IMapper mapper,
                INotificationRepository notificationRepository,
                ICurrentUserService currentUserService)
            {
                this._unitOfWork = unitOfWork;
                _mapper = mapper;
                _notificationRepository = notificationRepository;
                _currentUserService = currentUserService;
            }
            public async Task<ApiResponseDetails> Handle(ReadNotificationCommand request, CancellationToken cancellationToken)
            {
               if(request == null)
                {
                    var message = string.Format(Resource.Error_NotValid, request.NotificationId);
                    return ApiCustomResponse.NotFound(message);
                }
                var notification = await _notificationRepository.GetNotification(request.NotificationId, _currentUserService.UserId);
                if (notification == null)
                {
                    var message = string.Format(Resource.Error_NotFound, request.NotificationId);
                    return ApiCustomResponse.NotFound(message);
                }
                var isRead =  _notificationRepository.ReadNotification(notification);

                return ApiCustomResponse.ReturnedObject(isRead);
            }
        }
    }
}
