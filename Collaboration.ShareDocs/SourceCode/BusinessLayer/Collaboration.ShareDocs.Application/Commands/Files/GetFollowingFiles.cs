using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.Files.Dto;
using Collaboration.ShareDocs.Application.Commands.Users.Dto;
using Collaboration.ShareDocs.Application.Common.Response;
using Collaboration.ShareDocs.Persistence.Entities;
using Collaboration.ShareDocs.Persistence.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Application.Commands.Files
{
    public class GetFollowingFiles : IRequest<ApiResponseDetails>
    {
        public class Handler : IRequestHandler<GetFollowingFiles, ApiResponseDetails>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ICurrentUserService _currentUserService;
            private readonly IMapper _mapper;
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(
                IUnitOfWork unitOfWork,
                UserManager<ApplicationUser> userManager,
                ICurrentUserService currentUserService,
                IMapper mapper
                )
            {
                this._unitOfWork = unitOfWork;
                this._currentUserService = currentUserService;
                _mapper = mapper;
                this._userManager = userManager;
            }
            
            public async Task<ApiResponseDetails> Handle(GetFollowingFiles request, CancellationToken cancellationToken)
            {
                var timeLines = new List<TimeLineDto>();
                var filesList = new List<File>();
                var profiles = new List<ApplicationUser>();
                //var followings = await _unitOfWork.FollowRepository.GetFollowings(new Guid(_currentUserService.UserId), cancellationToken);

                //foreach (var following in followings)
                //{
                //    var profile = await _userManager.FindByIdAsync(following.Id.ToString());
                //    var userProfileDto = _mapper.Map<UserProfileDto>(profile);

                //    var files = await _unitOfWork.FileRepository.GetByCreatedByAsync(following.Id, cancellationToken);

                //    var filesDto = _mapper.Map<List<FileDto>>(files); 

                //    foreach (var item in filesDto)
                //    {
                //        timeLines.Add(new TimeLineDto
                //        {
                //            Author = userProfileDto,
                //            File = item
                //        });
                //    }
                //}

                return ApiCustomResponse.ReturnedObject(timeLines);
            }
        }
    }
}