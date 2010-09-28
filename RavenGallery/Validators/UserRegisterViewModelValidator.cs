using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RavenGallery.ViewModels;
using FluentValidation;
using RavenGallery.Core.Services;

namespace RavenGallery.Validators
{
    public class UserRegisterViewModelValidator : AbstractValidator<UserRegisterViewModel>
    {
        public UserRegisterViewModelValidator(IUserService userService)
        {
            this.RuleFor(x => x.Username)
                .NotEmpty()
                .Must(x => !userService.DoesUserExistWithUsername(x)).WithMessage("This user already exists")
                .Length(6, 20).WithMessage("Username must be between 6 and 20 characters long")
                .Matches("^[A-z0-9]+$").WithMessage("Username can only contain alpha numeric characters");
            this.RuleFor(x => x.Password)
                .NotEmpty()
                .Length(6, 10000).WithMessage("Password must be at least 6 characters long");
        }
    }
}