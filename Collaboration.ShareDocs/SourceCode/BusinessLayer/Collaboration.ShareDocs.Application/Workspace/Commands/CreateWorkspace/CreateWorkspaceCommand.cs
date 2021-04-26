using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Workspace.Commands
{
    public class CreateWorkspaceCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        
    }
}
