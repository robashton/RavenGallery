using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RavenGallery.ViewModels;
using FluentValidation;
using RavenGallery.Core.Services;

namespace RavenGallery.Validators
{
    public class UserSignInViewModelValidator : AbstractValidator<UserSignInViewModel>
    {
        public UserSignInViewModelValidator(IUserService userService)
        {
            this.RuleFor(x => x.Username)
                .NotEmpty()
                .Must((model, property) => userService.DoesUserExistWithUsernameAndPassword(model.Username, model.Password))
                    .WithMessage("User/password combination does not exist in our system");

            this.RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}