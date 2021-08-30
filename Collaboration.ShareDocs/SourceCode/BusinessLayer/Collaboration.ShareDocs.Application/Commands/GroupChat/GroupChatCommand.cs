using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.IndividualChat.Dto;
using Collaboration.ShareDocs.Application.Common.Response;
using Collaboration.ShareDocs.Persistence.Hubs;
using Collaboration.ShareDocs.Persistence.Interfaces;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Application.Commands.GroupChat
{
    public class GroupChatCommand: IRequest<ApiResponseDetails>
    {
        public string Message { get; set; }
        public Guid GroupId { get; set; }
    }
    public class Handler : IRequestHandler<GroupChatCommand, ApiResponseDetails>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        private readonly INotificationRepository _notificationRepository;
        private readonly IHubContext<GroupHub> _hubContext;

        public Handler(IUnitOfWork unitOfWork,
            IUserRepository userRepository,
            IFollowRepository followRepository,
            ICurrentUserService currentUserService,
            INotificationRepository notificationRepository,
            IMapper mapper,
            IHubContext<GroupHub> hubContext
            )
        {
            this._unitOfWork = unitOfWork;
            this._currentUserService = currentUserService;
            _mapper = mapper;
            
            this._userRepository = userRepository;
            this._notificationRepository = notificationRepository;
            _hubContext = hubContext;
        }
        public async Task<ApiResponseDetails> Handle(GroupChatCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _userRepository.GetUser(new Guid(_currentUserService.UserId), cancellationToken);
            //handle error of id
            var message = new Persistence.Entities.IndividualChat
            {
                Text = request.Message,
                From = currentUser,
                To = null,
                SentAt = DateTime.Now
            };

            var group = await _unitOfWork.GroupRepository.GetAsync(request.GroupId, cancellationToken);
            group.Messages.Add(message);
            group.Members.Add(currentUser);
           // await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _hubContext.Clients.All.SendAsync("Recive Message", message.Text);

            var response = _mapper.Map<IndividualChatDto>(message);
            return ApiCustomResponse.ReturnedObject(response);


            // throw new NotImplementedException();
        }
    }
}
