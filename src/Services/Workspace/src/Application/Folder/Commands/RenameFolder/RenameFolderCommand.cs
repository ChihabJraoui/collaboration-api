using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Folder.Commands.UpdateFolder
{
    public class RenameFolderCommand:IRequest<string>
    {
        public Guid FolderId{ get; set; }
        public string Name { get; set; }
    }
}
