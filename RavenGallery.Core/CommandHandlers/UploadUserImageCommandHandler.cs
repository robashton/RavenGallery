using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RavenGallery.Core.Commands;
using RavenGallery.Core.Services;
using RavenGallery.Core.Repositories;

namespace RavenGallery.Core.CommandHandlers
{
    public class UploadUserImageCommandHandler : ICommandHandler<UploadUserImageCommand>
    {
        private IUserRepository userRepository;
        private IImageUploaderService imageUploader;

        public UploadUserImageCommandHandler(IUserRepository userRepository, IImageUploaderService imageUploader)
        {
            this.userRepository = userRepository;
            this.imageUploader = imageUploader;
        }

        public void Handle(UploadUserImageCommand command)
        {
            var user = this.userRepository.Load(command.OwnerUserId);
            this.imageUploader.UploadUserImage(user, command.Title, command.Tags, command.Data);
        }
    }
}
