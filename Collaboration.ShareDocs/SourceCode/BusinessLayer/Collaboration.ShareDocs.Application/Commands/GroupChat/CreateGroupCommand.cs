using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.GroupChat.Dto;
using Collaboration.ShareDocs.Application.Common.Response;
using Collaboration.ShareDocs.Persistence.Entities;
using Collaboration.ShareDocs.Persistence.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Application.Commands.GroupChat
{
    public class CreateGroupCommand:IRequest<ApiResponseDetails>
    {
        public string Name { get; set; }


        public class Handler : IRequestHandler<CreateGroupCommand, ApiResponseDetails>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ICurrentUserService _currentUserService;
            private readonly IMapper _mapper;
            private readonly IUserRepository _userRepository;

            public Handler(IUnitOfWork unitOfWork,
                           IUserRepository userRepository,
                           IFollowRepository followRepository,
                           ICurrentUserService currentUserService,
                           IMapper mapper
           //,
           //IHubContext<IndividualChatHub> hubContext
                           )
            {
                this._unitOfWork = unitOfWork;
                this._currentUserService = currentUserService;
                _mapper = mapper;

                this._userRepository = userRepository;
               
                // _hubContext = hubContext;
            }
            public async Task<ApiResponseDetails> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
            {
                var owner = await _userRepository.GetUser(new Guid(_currentUserService.UserId), cancellationToken);
                var group = new Group() { Name = request.Name, Owner = owner.Id };
                var newGroup= await _unitOfWork.GroupRepository.CreateGroup(group, cancellationToken);
                
                await _unitOfWork.SaveChangesAsync();
                newGroup.Members.Add(owner);
                await _unitOfWork.SaveChangesAsync();
                var response = _mapper.Map<CreateGroupDto>(newGroup);
                return ApiCustomResponse.ReturnedObject(response);




            }
        }

    }
    
}
