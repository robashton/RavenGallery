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
            this.RuleFor(x => x.Username).NotEmpty()
                .Must(x => !userService.DoesUserExistWithUsername(x)).WithMessage("This user already exists")
                .Matches("^[A-z0-9]+$").WithMessage("Username can only contain alpha numeric characters");
            this.RuleFor(x => x.Password).NotEmpty();
            this.RuleFor(x => x.StayLoggedIn).NotEmpty();
        }
    }
}