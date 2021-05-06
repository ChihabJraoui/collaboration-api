using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.Folders.Dto;
using Collaboration.ShareDocs.Application.Common.Response;
using Collaboration.ShareDocs.Persistence.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Application.Commands.Folders
{
    public class GetFoldersByCreatedUserCommand :IRequest<ApiResponseDetails>
    {
        public Guid UserId { get; set; }

        public class Handler : IRequestHandler<GetFoldersByCreatedUserCommand, ApiResponseDetails>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ICurrentUserService _currentUserService;
            private readonly IMapper _mapper;
            public Handler(IUnitOfWork unitOfWork,
                                         ICurrentUserService currentUserService,
                                         IMapper mapper)
            {
                this._unitOfWork = unitOfWork;
                this._currentUserService = currentUserService;
                _mapper = mapper;
            }
            public async Task<ApiResponseDetails> Handle(GetFoldersByCreatedUserCommand request, CancellationToken cancellationToken)
            {
                var folders = await _unitOfWork.FolderRepository.GetByCreatedAsync(request.UserId, cancellationToken);
                var response = _mapper.Map<List<FolderDto>>(folders);
                return ApiCustomResponse.ReturnedObject(response);
            }
        }
    }
}
