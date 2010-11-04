using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RavenGallery.Core;
using FlickrNet;
using RavenGallery.Core.Commands;
using RavenGallery.Core.Services;
using RavenGallery.Core.Repositories;
using Raven.Client;
using System.Net;
using System.IO;

namespace RavenGallery.FlickrImport
{
    public class FlickrImporter
    {
        private IImageUploaderService imageUploadService;
        private IUserRepository userRepository;
        private IDocumentSession documentSession;

        public FlickrImporter(IDocumentSession documentSession, IImageUploaderService imageUploadService, IUserRepository userRepository)
        {
            this.imageUploadService = imageUploadService;
            this.userRepository = userRepository;
            this.documentSession = documentSession;
        }

        public void ImportSearchResults(Flickr flickr, string userId, string searchTerm)
        {
            var results = flickr.PhotosSearch(new PhotoSearchOptions()
            {
                 SafeSearch = SafetyLevel.Safe,
                 IsCommons = true,                   
                 Tags = searchTerm,
                 Extras = PhotoSearchExtras.Tags,
                 Page = 0,
                 PerPage = 50
            });
            var user = userRepository.Load(userId);
            foreach (var image in results)
            {                
                Console.WriteLine("Found {0} with ({1})", image.Title, string.Join(",", image.Tags.ToArray()));

                Console.WriteLine("Downloading image data");

                WebRequest request = WebRequest.Create(image.MediumUrl);
                Byte[] content = null;
                using (var response = request.GetResponse())
                {
                    content = new Byte[response.ContentLength];
                    var stream = response.GetResponseStream();
                    BinaryReader reader = new BinaryReader(stream);
                    content = reader.ReadBytes((int)response.ContentLength);
                }

                imageUploadService.UploadUserImage(user, 
                        image.Title,
                        image.Tags.ToArray(),
                        content);

                Console.WriteLine("Downloaded image data");  
            }
            documentSession.SaveChanges();
        }
    }
}
