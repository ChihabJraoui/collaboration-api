using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Folder.Queries
{
    public class GetFolderQuery:IRequest<Collaboration.ShareDocs.Persistence.Entities.Folder>
    {
        public Guid FolderId { get; set; }
    }
}
