using Application.Folder.Commands.UpdateFolder;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Folder.Commands.RenameFolder
{
    public class RenameFolderValidator : AbstractValidator<RenameFolderCommand>
    {
        public RenameFolderValidator()
        {
            this.RuleFor(p => p.FolderId).NotEmpty().WithMessage("Is required.");
            this.RuleFor(p => p.Name).NotEmpty().MinimumLength(3).WithMessage("Is required.");

        }

    }
}
