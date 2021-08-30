using AutoMapper;
using Collaboration.ShareDocs.Application.Common.Response;
using Collaboration.ShareDocs.Persistence.Entities;
using Collaboration.ShareDocs.Persistence.Interfaces;
using Collaboration.ShareDocs.Resources;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Application.Commands.GroupChat
{
    public class JoinGroupCommand : IRequest<ApiResponseDetails>
    {
        public Guid GroupId { get; set; }
        public Guid[] UsersId { get; set; }

        public class Handler : IRequestHandler<JoinGroupCommand, ApiResponseDetails>
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
            public async Task<ApiResponseDetails> Handle(JoinGroupCommand request, CancellationToken cancellationToken)
            {
                var group = await _unitOfWork.GroupRepository.GetAsync(request.GroupId, cancellationToken);
                if (group == null)
                {
                    var message = string.Format(Resource.Error_NotFound, group, request.GroupId);
                    return ApiCustomResponse.NotFound(message);
                }
                foreach (var userId in request.UsersId)
                {
                    var userDb = await this._userManager.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();

                    if (userDb == null)
                    {
                        var message = string.Format(Resource.Error_NotFound, userDb, userId);
                        return ApiCustomResponse.NotFound(message);
                    }

                    group.Members.Add(userDb);

                     await _unitOfWork.SaveChangesAsync(cancellationToken);
                    
                }
                return ApiCustomResponse.ReturnedObject();
            }
        }
    }
}


