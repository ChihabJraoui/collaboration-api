using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Folder.Commands.DeleteFolder
{
    public class DeleteFolderCommand : IRequest<DeleteFolderDto>
    {
        public Guid FolderId { get; set; }
    }
}
