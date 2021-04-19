using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Workspace.Commands.UpdateWorkspace
{
    public class UpdateWorkspaceCommand:IRequest<Core.Entities.Workspace>
    {
        public Guid WorkspaceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool BookMark { get; set; }
        public bool IsPrivate { get; set; }
        
    }
}
