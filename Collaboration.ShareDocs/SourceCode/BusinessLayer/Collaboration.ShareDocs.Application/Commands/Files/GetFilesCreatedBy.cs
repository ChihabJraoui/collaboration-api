using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.Files.Dto;
using Collaboration.ShareDocs.Application.Common.Response;
using Collaboration.ShareDocs.Persistence.Entities;
using Collaboration.ShareDocs.Persistence.Interfaces;
using Collaboration.ShareDocs.Resources;
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
    public class GetFilesCreatedBy:IRequest<ApiResponseDetails>
    {
        public Guid UserId { get; set; }
        public class Handler : IRequestHandler<GetFilesCreatedBy, ApiResponseDetails>
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
            public async Task<ApiResponseDetails> Handle(GetFilesCreatedBy request, CancellationToken cancellationToken)
            {
                var user = await this._userManager.Users.SingleOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

                if (user == null)
                {
                    var message = string.Format(Resource.Error_NotFound, request.UserId);
                    return ApiCustomResponse.NotFound(message);
                }
                var files = await _unitOfWork.FileRepository.GetByCreatedByAsync(request.UserId, cancellationToken);
                var response = _mapper.Map<List<FileDto>>(files);
                return ApiCustomResponse.ReturnedObject(response);
            }
        }
    }
}
