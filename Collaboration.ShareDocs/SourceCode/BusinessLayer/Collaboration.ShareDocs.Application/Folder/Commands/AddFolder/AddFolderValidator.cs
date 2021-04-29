using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Folder.Commands
{
    public class AddFolderValidator : AbstractValidator<AddFolderCommand>
    {
        public AddFolderValidator()
        {
            this.RuleFor(p => p.ProjectId).NotEmpty().WithMessage("Id is required");
            this.RuleFor(p => p.Name).NotEmpty().MinimumLength(3).WithMessage("Name is required");

        }
    }
}
