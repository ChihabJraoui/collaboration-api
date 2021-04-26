using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Project.Commands.AddMemberToProject
{
    public class AddMemberToProject:IRequest<Unit>
    {
        public Guid MemberID { get; set; }
        public Guid ProjectID { get; set; }
    }
}
