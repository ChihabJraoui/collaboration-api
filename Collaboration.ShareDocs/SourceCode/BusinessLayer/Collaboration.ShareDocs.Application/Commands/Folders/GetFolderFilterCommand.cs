using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.Folders.Dto;
using Collaboration.ShareDocs.Application.Common.Response;
using Collaboration.ShareDocs.Persistence.Entities;
using Collaboration.ShareDocs.Persistence.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Application.Commands.Folders
{
    public class GetFolderFilterCommand : IRequest<ApiResponseDetails>
    {
        public Guid Proprety { get; set; }

        public class Handler : IRequestHandler<GetFolderFilterCommand, ApiResponseDetails>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
            {
                this._unitOfWork = unitOfWork;
                _mapper = mapper;
                _userManager = userManager;
            }

            public async Task<ApiResponseDetails> Handle(GetFolderFilterCommand request, CancellationToken cancellationToken)
            {
                var folders = await _unitOfWork.FolderRepository.GetManyAsync(request.Proprety, cancellationToken);  
                var response = _mapper.Map<List<FolderDto>>(folders);
                return ApiCustomResponse.ReturnedObject(response);
            }
        }
    }
}
