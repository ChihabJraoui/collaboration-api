using AutoMapper;
using Collaboration.ShareDocs.Application.Commands.Files.Dto;
using Collaboration.ShareDocs.Application.Common.Response;
using Collaboration.ShareDocs.Persistence.Interfaces;
using Collaboration.ShareDocs.Resources;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Application.Commands.Files
{
    public class GetFilesByFolderId:IRequest<ApiResponseDetails>
    {
        public Guid FolderParentId { get; set; }

        public class Handler : IRequestHandler<GetFilesByFolderId, ApiResponseDetails>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                this._unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<ApiResponseDetails> Handle(GetFilesByFolderId request, CancellationToken cancellationToken)
            {
                var folder = await _unitOfWork.FolderRepository.GetAsync(request.FolderParentId, cancellationToken);
                if(folder == null)
                {
                    var message = string.Format(Resource.Error_NotFound, request.FolderParentId);
                    return ApiCustomResponse.NotFound(message);
                }
                var files =await _unitOfWork.FileRepository.GetByFolderIdAsync(folder.FolderId, cancellationToken);
                var response = _mapper.Map<List<FileDto>>(files);
                return ApiCustomResponse.ReturnedObject(response);
            }
        }
    }
}
