using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Collaboration.ShareDocs.Application.Workspace.Queries.GetLastWorkspace
{
    public class GetLastWorkspaceQuery:IRequest<LastWorkspaceDto>
    {
    }
}
