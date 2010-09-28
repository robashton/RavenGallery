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
            this.RuleFor(x => x.Username).NotEmpty().Must(x => !userService.DoesUserExistWithUsername(x));
            this.RuleFor(x => x.Password).NotEmpty();
            this.RuleFor(x => x.StayLoggedIn).NotEmpty();
        }
    }
}