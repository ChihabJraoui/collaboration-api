using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Folder.Commands
{
    public class AddFolderCommand:IRequest<Core.Entities.Folder>
    {
        public string Name { get; set; }
        public Guid FolderParentId { get; set; }
        public Guid ProjectId { get; set; }

    }
}
