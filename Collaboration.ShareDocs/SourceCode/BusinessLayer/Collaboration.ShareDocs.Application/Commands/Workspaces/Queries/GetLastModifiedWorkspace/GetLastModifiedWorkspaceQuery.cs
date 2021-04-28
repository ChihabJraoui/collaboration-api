using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Collaboration.ShareDocs.Application.Workspace.Queries.GetLastModifiedWorkspace
{
    public class GetLastModifiedWorkspaceQuery:IRequest<WorkspaceLastModifiedDto>
    {
    }
}
