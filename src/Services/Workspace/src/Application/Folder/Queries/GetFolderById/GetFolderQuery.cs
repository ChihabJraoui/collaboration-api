using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Folder.Queries
{
    public class GetFolderQuery:IRequest<Core.Entities.Folder>
    {
        public Guid FolderId { get; set; }
    }
}
