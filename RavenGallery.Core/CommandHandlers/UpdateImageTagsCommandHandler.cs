using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RavenGallery.Core.Commands;
using RavenGallery.Core.Repositories;

namespace RavenGallery.Core.CommandHandlers
{
    public class UpdateImageTagsCommandHandler : ICommandHandler<UpdateImageTagsCommand>
    {
        private IImageRepository imageRepository;

        public UpdateImageTagsCommandHandler(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        public void Handle(UpdateImageTagsCommand command)
        {
            var entity = imageRepository.Load(command.ImageId);
            entity.ClearTags();
            foreach (var tag in command.Tags)
            {
                entity.AddTag(tag);
            }
        }
    }
}
