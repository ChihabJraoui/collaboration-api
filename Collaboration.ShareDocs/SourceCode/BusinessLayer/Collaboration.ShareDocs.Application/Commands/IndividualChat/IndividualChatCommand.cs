﻿using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.IndividualChat.Dto;
using Collaboration.ShareDocs.Application.Common.Response;
using Collaboration.ShareDocs.Persistence.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Collaboration.ShareDocs.Application.Commands.IndividualChat
{
    public class IndividualChatCommand:IRequest<ApiResponseDetails>
    {
        public string Message { get; set; }
        public Guid To { get; set; }
        public class Handler : IRequestHandler<IndividualChatCommand, ApiResponseDetails>
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

            public async Task<ApiResponseDetails> Handle(IndividualChatCommand request, CancellationToken cancellationToken)
            {
                var currentUser = await _userRepository.GetUser(new Guid(_currentUserService.UserId), cancellationToken);
                var user = await _userRepository.GetUser(request.To, cancellationToken);
                var message = new Persistence.Entities.IndividualChat {
                    Text = request.Message,
                    From = currentUser,
                    To = user,
                    SentAt = DateTime.Now
                };
                await _unitOfWork.IndividualChatRepository.Create(message,cancellationToken);
                await _unitOfWork.SaveChangesAsync();
                var response = _mapper.Map<IndividualChatDto>(message);
                return ApiCustomResponse.ReturnedObject(response);

            }
        }
}
}