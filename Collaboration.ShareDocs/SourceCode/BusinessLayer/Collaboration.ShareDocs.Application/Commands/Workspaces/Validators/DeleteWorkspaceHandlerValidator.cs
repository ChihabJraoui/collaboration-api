using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Collaboration.ShareDocs.Application.Common.Extensions;

namespace Collaboration.ShareDocs.Application.Commands.Workspaces.Validators
{
    public class DeleteWorkspaceHandlerValidator : AbstractValidator<DeleteWorkspaceCommand>
    {
        public DeleteWorkspaceHandlerValidator()
        {
            this.RuleFor(x => x.WorkspaceId).IsGuid(  )
                .WithMessage("Workspace should not be empty");

        }
    }
}
