﻿using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.IndividualChat.Dto;
using Collaboration.ShareDocs.Application.Common.Response;
using Collaboration.ShareDocs.Persistence.Interfaces;
using Collaboration.ShareDocs.Resources;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Application.Commands.IndividualChat
{
    public class ChatHistoryCommand:IRequest<ApiResponseDetails>
    {
        public Guid UserId { get; set; }
        public class Handler : IRequestHandler<ChatHistoryCommand, ApiResponseDetails>
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
            public async  Task<ApiResponseDetails> Handle(ChatHistoryCommand request, CancellationToken cancellationToken)
            {
                var currentUser = await _userRepository.GetUser(new Guid(_currentUserService.UserId), cancellationToken);

                var user = await _userRepository.GetUser(request.UserId, cancellationToken);
                if (user == null)
                {
                    var message = string.Format(Resource.Error_NotFound, request.UserId);
                    return ApiCustomResponse.NotFound(message);
                }

                var chatMessaging = await _unitOfWork.IndividualChatRepository.GetChatAsync(currentUser.Id, user.Id);
               
                var response = _mapper.Map<List<IndividualChatDto>>(chatMessaging);
                var model = new IndividulChatModelDto()
                {
                    History = response,
                    UserName = user.FirstName + ' ' + user.LastName

                };
                return ApiCustomResponse.ReturnedObject(model);
                
            }
        }
    }
}
