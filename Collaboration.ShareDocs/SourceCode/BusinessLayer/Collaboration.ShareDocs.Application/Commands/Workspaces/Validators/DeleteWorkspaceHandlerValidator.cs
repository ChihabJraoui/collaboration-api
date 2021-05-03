using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Collaboration.ShareDocs.Application.Commands.Workspaces.Validators
{
    public class DeleteWorkspaceHandlerValidator : AbstractValidator<DeleteWorkspaceCommand>
    {
        public DeleteWorkspaceHandlerValidator()
        {
            this.RuleFor(x => x.WorkspaceId).NotEmpty()
                .WithMessage("Workspace should not be empty");

        }
    }
}
