using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.Follows.Dto;
using Collaboration.ShareDocs.Application.Commands.Users.Dto;
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

namespace Collaboration.ShareDocs.Application.Commands.Follows
{
    public class GetFollowingCommand:IRequest<ApiResponseDetails>
    {
        public Guid UserId { get; set; }
    }
    public class Handler : IRequestHandler<GetFollowingCommand, ApiResponseDetails>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        public Handler(IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            ICurrentUserService currentUserService,
            IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._currentUserService = currentUserService;
            _mapper = mapper;
            this._userManager = userManager;
        }
        public async Task<ApiResponseDetails> Handle(GetFollowingCommand request, CancellationToken cancellationToken)
        {
            var isFollowing = false;
            var user = await this._userManager.Users.SingleOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

            if (user == null)
            {
                var message = string.Format(Resource.Error_NotFound, request.UserId);
                return ApiCustomResponse.NotFound(message);
            }
            if (user.Id != new Guid(_currentUserService.UserId)) 
            {
                 var followEntity = await _unitOfWork.FollowRepository.IsFollowing(user.Id, _currentUserService.UserId);
                if (followEntity != null)
                {
                    isFollowing = true;
                }
                
                   
            }
            if (isFollowing != true && user.Id != new Guid(_currentUserService.UserId))
            {
                var message = string.Format(Resource.Error_NotFound, request.UserId);
                return ApiCustomResponse.NotFound(message);

            }
            var follows = await _userManager.Users.Where(w => w.Id == user.Id).Select(x => x.Followers).ToListAsync(cancellationToken);
            var count = follows.Count()+1;
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var response = _mapper.Map<List<ICollection<FollowDto>>>(follows);
            var result= new FollowingDto
            {
                Following = response,
                FollowingCount = count
            };
            return ApiCustomResponse.ReturnedObject(result);
        }
    }
}
