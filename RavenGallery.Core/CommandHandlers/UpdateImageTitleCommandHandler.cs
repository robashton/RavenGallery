using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RavenGallery.Core.Commands;
using RavenGallery.Core.Repositories;

namespace RavenGallery.Core.CommandHandlers
{
    public class UpdateImageTitleCommandHandler : ICommandHandler<UpdateImageTitleCommand>
    {
        private IImageRepository imageRepository;

        public UpdateImageTitleCommandHandler(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        public void Handle(UpdateImageTitleCommand command)
        {
            var entity = this.imageRepository.Load(command.ImageId);
            entity.UpdateTitle(command.Title);
        }
    }
}
