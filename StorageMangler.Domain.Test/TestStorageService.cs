﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using StorageMangler.Domain.Infrastructure;
using StorageMangler.Domain.Model;
using StorageMangler.Domain.Service;
using Moq;


namespace StorageMangler.Domain.Test
{

    [TestFixture]
    public class TestStorageService
    {
        private readonly IFileMetaDataRepository _metaDataRepository;
        private readonly IFileStorage _fileStorage;
        private readonly ForbiddenNamesService _forbiddenService;
        private readonly ILoggerFactory _logger;

        [SetUp]
        public void SetUp()
        {
            _var services = new ServiceCollection();
            var serviceProvider = services.BuildServiceProvider();


            _metaDataRepository = serviceProvider.GetService<IFileMetaDataRepository>();
            _fileStorage = serviceProvider.GetService<IFileStorage>();
            _forbiddenService = serviceProvider.GetService<ForbiddenNamesService>();
            _logger = serviceProvider.GetService<ILoggerFactory>();

        }


       [Test]
        public async Task TestListNonForbiddenFiles()
        {
            StorageService ss = new StorageService(_metaDataRepository, _fileStorage, _forbiddenService, _logger);
            List<FileInfo> lf = await ss.ListNonForbiddenFiles();
            Assert.IsNotNull(lf);
            Assert.Greater(0, lf.Count);
        }


    }

    
   
}

