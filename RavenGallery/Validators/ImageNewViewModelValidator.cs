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

        }
    }
}