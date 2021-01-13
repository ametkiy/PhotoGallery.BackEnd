using FluentValidation;
using PhotoGallery.Features.Tags.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoGallery.Features.Tags.Validations
{
    public class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
    {
        public CreateTagCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        }
    }
}