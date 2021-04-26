using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Collaboration.ShareDocs.Application.Workspace.Commands.CreateWorkspace
{
    public class CreateWorkspaceCommand:IRequest<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
