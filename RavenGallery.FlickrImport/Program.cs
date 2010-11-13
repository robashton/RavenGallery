using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RavenGallery.Core;
using StructureMap;
using FlickrNet;
using RavenGallery.Core.Services;
using RavenGallery.Core.Commands;
using RavenGallery.Core.Utility;
using Raven.Client;
using System.IO;
using RavenGallery.Core.Documents;
using Raven.Client.Document;
using Raven.Database;
using RavenGallery.Core.Entities;
using RavenGallery.Core.Repositories;
using RavenGallery.Core.Infrastructure;
using System.Threading;
using Raven.Client.Client;

namespace RavenGallery.FlickrImport
{
    public static class Program
    {
        private const string RAVENPATH = @"D:\Programming\RavenGallery\RavenGallery\App_Data\RavenDB";
       
        public static void Main(string[] args)
        {          
            Flickr flickr = new Flickr();

            var documentStore = new EmbeddableDocumentStore
            {
                Configuration = new RavenConfiguration
                {
                    DataDirectory = RAVENPATH,
                }
            };
            documentStore.Initialize();

            using (var session = documentStore.OpenSession())
            {
                PerformInitialSetup(session);

                session.Advanced.DatabaseCommands.DeleteByIndex("Raven/DocumentsByEntityName",
                    new Raven.Database.Data.IndexQuery() { Query = "Tag:Images" }, true);

                FlickrImporter importer = new FlickrImporter(session,
                    new ImageUploaderService(
                        new RavenFileStorageService(documentStore),
                        new ImageRepository(session)),
                        new UserRepository(session));

                importer.ImportSearchResults(flickr, IdUtil.CreateUserId("robashton"), "dog");
                importer.ImportSearchResults(flickr, IdUtil.CreateUserId("robashton"), "swan");
                importer.ImportSearchResults(flickr, IdUtil.CreateUserId("robashton"), "computer");
                importer.ImportSearchResults(flickr, IdUtil.CreateUserId("robashton"), "megaman");
                importer.ImportSearchResults(flickr, IdUtil.CreateUserId("robashton"), "rainbow");
                importer.ImportSearchResults(flickr, IdUtil.CreateUserId("robashton"), "sunset");
                importer.ImportSearchResults(flickr, IdUtil.CreateUserId("robashton"), "raven");
                importer.ImportSearchResults(flickr, IdUtil.CreateUserId("robashton"), "coffee");
                importer.ImportSearchResults(flickr, IdUtil.CreateUserId("robashton"), "jumper");               

            }
                       
            while (documentStore.DocumentDatabase.Statistics.StaleIndexes.Length > 0)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Waiting for indexing to complete");
            }

         
        }

        private static void PerformInitialSetup(IDocumentSession session)
        {
            IUserService userService = new UserService(session);
            if (!userService.DoesUserExistWithUsername("robashton"))
            {
                User newUser = new User("robashton", "password");
                IUserRepository userRepository = new UserRepository(session);
                userRepository.Add(newUser);
            }
            session.SaveChanges();
        }     
    }
}