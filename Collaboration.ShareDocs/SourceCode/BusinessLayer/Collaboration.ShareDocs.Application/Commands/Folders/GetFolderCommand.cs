using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.Folders.Dto;
using Collaboration.ShareDocs.Application.Common.Response;
using Collaboration.ShareDocs.Persistence.Interfaces;
using Collaboration.ShareDocs.Resources;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Application.Commands.Folders
{
    public class GetFolderCommand:IRequest<ApiResponseDetails>
    {
        public Guid FolderId { get; set; }

        public class Handler : IRequestHandler<GetFolderCommand, ApiResponseDetails>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                this._unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<ApiResponseDetails> Handle(GetFolderCommand request, CancellationToken cancellationToken)
            {
                if (request.FolderId == null)
                {
                    return ApiCustomResponse.IncompleteRequest();
                }
                var folder = await _unitOfWork.FolderRepository.GetAsync(request.FolderId, cancellationToken);
                if (folder == null)
                {
                    var message = string.Format(Resource.Error_NotFound, request.FolderId);
                    return ApiCustomResponse.NotFound(message);
                }
                var response = _mapper.Map<FolderDto>(folder);
                return ApiCustomResponse.ReturnedObject(response);

            }
        }
    }
}
