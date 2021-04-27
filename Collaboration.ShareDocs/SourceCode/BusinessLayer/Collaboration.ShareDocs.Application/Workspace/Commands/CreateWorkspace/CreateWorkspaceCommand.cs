using System;
using MediatR;

namespace Collaboration.ShareDocs.Application.Workspace.Commands.CreateWorkspace
{
    public class CreateWorkspaceCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        
    }
}
