using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Collaboration.ShareDocs.Application.Workspace.Queries.GetAllWorkspaces
{
    public class GetAllWorkspaceQuery: IRequest<WorkspacesDTOLists>
    {
    }
}
