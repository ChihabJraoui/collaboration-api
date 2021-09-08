using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.Users.Dto;
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
    public class GetGroupMemberCommand:IRequest<ApiResponseDetails>
    {
        public Guid GroupId { get; set; }

        public class Handler : IRequestHandler<GetGroupMemberCommand, ApiResponseDetails>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ICurrentUserService _currentUserService;
            private readonly IMapper _mapper;
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService,
                                         IMapper mapper, UserManager<ApplicationUser> userManager)
            {
                this._unitOfWork = unitOfWork;
                this._currentUserService = currentUserService;
                _mapper = mapper;
                _userManager = userManager;
            }
            public async Task<ApiResponseDetails> Handle(GetGroupMemberCommand request, CancellationToken cancellationToken)
            {
                var group = await _unitOfWork.GroupRepository.GetAsync(request.GroupId, cancellationToken);
                if (group == null)
                {
                    var message = string.Format(Resource.Error_NotFound, group, request.GroupId);
                    return ApiCustomResponse.NotFound(message);
                }
                var members =  await _unitOfWork.GroupRepository.GetMemberAsync(group.GroupID,cancellationToken);
                var response = _mapper.Map<List<List<UserProfileDto>>>(members);
                return ApiCustomResponse.ReturnedObject(response);
            }
        }
                
    }
}
