using FluentValidation;
using PlayBox.Application.DTOs.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayBox.Application.Validators.Content
{
    public class CreateContentDtoValidator : AbstractValidator<CreateContentDto>
    {
        public CreateContentDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(2000);

            RuleFor(x => x.ImageUrl)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(x => x.Links)
                .NotNull()
                .Must(x => x.Count <= 4)
                .WithMessage("Maximum 4 links are allowed");
        }
    }
}
