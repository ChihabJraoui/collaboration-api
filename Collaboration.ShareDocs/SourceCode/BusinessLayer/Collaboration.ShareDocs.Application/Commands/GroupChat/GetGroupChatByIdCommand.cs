using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.GroupChat.Dto;
using Collaboration.ShareDocs.Application.Commands.IndividualChat.Dto;
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
    public class GetGroupChatByIdCommand:IRequest<ApiResponseDetails>
    {
        public Guid GroupId { get; set; }
        public class Handler : IRequestHandler<GetGroupChatByIdCommand, ApiResponseDetails>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ICurrentUserService _currentUserService;

            private readonly IMapper _mapper;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IUserProjectRepository _userProjectRepository;

            public Handler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, IUserProjectRepository userProjectRepository,
                                         IMapper mapper, UserManager<ApplicationUser> userManager)
            {
                this._unitOfWork = unitOfWork;
                this._currentUserService = currentUserService;
                _mapper = mapper;
                _userManager = userManager;
                _userProjectRepository = userProjectRepository;
            }
            public async Task<ApiResponseDetails> Handle(GetGroupChatByIdCommand request, CancellationToken cancellationToken)
            {
                var group = await _unitOfWork.GroupRepository.GetAsync(request.GroupId, cancellationToken);
                if (group == null)
                {
                    var message = string.Format(Resource.Error_NotFound, group, request.GroupId);
                    return ApiCustomResponse.NotFound(message);
                }
                var response = _mapper.Map<CreateGroupDto>(group);
                return ApiCustomResponse.ReturnedObject(response);

            }
        }
    }
}
