using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.GroupChat.Dto;
using Collaboration.ShareDocs.Application.Commands.IndividualChat.Dto;
using Collaboration.ShareDocs.Application.Common.Response;
using Collaboration.ShareDocs.Persistence.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Application.Commands.GroupChat
{
    public class GroupChatHistoryCommand : IRequest<ApiResponseDetails>
    {
        public Guid groupId { get; set; }

        public class Handler : IRequestHandler<GroupChatHistoryCommand, ApiResponseDetails>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ICurrentUserService _currentUserService;
            private readonly IMapper _mapper;
            private readonly IFollowRepository _followRepository;
            private readonly IUserRepository _userRepository;
            private readonly INotificationRepository _notificationRepository;
            private bool reply = false;

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
            public async Task<ApiResponseDetails> Handle(GroupChatHistoryCommand request, CancellationToken cancellationToken)
            {
                var group = await _unitOfWork.GroupRepository.GetAsync(request.groupId, cancellationToken);
                var currentUser = await _userRepository.GetUser(new Guid(_currentUserService.UserId), cancellationToken);
                var history = await _unitOfWork.GroupRepository.GetHistory(group.GroupID, cancellationToken);
                foreach (var chat in history)
                {
                    foreach (var ch in chat)
                    {
                        if (ch.From.Id == currentUser.Id)
                        {
                            reply = true;
                        }
                        var entity = _mapper.Map<IndividualChatDto>(ch);
                        var model = new GroupChatHistoryDto() { Messages = entity, replay = reply };
                       
                    }
                    
                }
                var response = _mapper.Map<List<List<IndividualChatDto>>>(history);
                return ApiCustomResponse.ReturnedObject(response);


            }
        }
    }
}

