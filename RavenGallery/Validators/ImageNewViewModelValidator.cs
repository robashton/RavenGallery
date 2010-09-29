using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RavenGallery.ViewModels;
using FluentValidation;

namespace RavenGallery.Validators
{
    public class ImageNewViewModelValidator : AbstractValidator<ImageNewViewModel>
    {
        public ImageNewViewModelValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .Length(6, 30).WithMessage("Title length must be between 6 and 30 characters long");
            RuleFor(x => x.Tags)
                .Must((x, y) => y.Length > 0).WithMessage("Image must have at least one tag");
        }
    }
}