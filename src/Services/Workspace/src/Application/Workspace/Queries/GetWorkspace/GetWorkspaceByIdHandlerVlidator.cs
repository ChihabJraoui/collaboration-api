using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Workspace.Queries.GetWorkspace
{
    public class GetWorkspaceByIdHandlerVlidator:AbstractValidator<GetWorkspaceByIdQuery>
    {
        public GetWorkspaceByIdHandlerVlidator()
        {
            this.RuleFor(x => x.WorkspaceRequestId).NotEmpty().WithMessage("Id is required");
        }
    }
}
