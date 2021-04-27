using System;
using MediatR;

namespace Collaboration.ShareDocs.Application.Project.Commands.AddMemberToProject
{
    public class AddMemberToProject:IRequest<Unit>
    {
        public Guid MemberID { get; set; }
        public Guid ProjectID { get; set; }
    }
}
