using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.GroupChat.Dto;
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

namespace Collaboration.ShareDocs.Application.Commands.GroupChat
{
    public class GetGroupsByMemberId : IRequest<ApiResponseDetails>
    {
        public Guid MemberId { get; set; }

        public class Handler : IRequestHandler<GetGroupsByMemberId, ApiResponseDetails>
        {
            private readonly IUserRepository _userRepository;
            private readonly IUnitOfWork _unitOfWork;
            private readonly ICurrentUserService _currentUserService;

            private readonly IMapper _mapper;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IUserProjectRepository _userProjectRepository;


            public Handler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService,
                                         IMapper mapper, UserManager<ApplicationUser> userManager, IUserRepository userRepository)
            {
                this._userRepository = userRepository;
                this._unitOfWork = unitOfWork;
                this._currentUserService = currentUserService;
                _mapper = mapper;
                _userManager = userManager;
      
            }

            public async Task<ApiResponseDetails> Handle(GetGroupsByMemberId request, CancellationToken cancellationToken)
            {
                //BR01
                var member = await _userRepository.GetUser(request.MemberId, cancellationToken);
                if(member == null)
                {
                    var message = string.Format(Resource.Error_NotFound, request.MemberId);
                    return ApiCustomResponse.NotFound(message);
                }
                var groups = await _unitOfWork.GroupRepository.GetGroupsAsync(member, cancellationToken);
                var response = _mapper.Map<List<CreateGroupDto>>(groups);
                return ApiCustomResponse.ReturnedObject(response);
                
            }
        }
    }
}
