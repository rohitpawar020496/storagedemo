using System.Collections.Generic;
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
        private Mock<IFileMetaDataRepository> _metaDataRepository;
        private Mock<IFileStorage> _fileStorage;
        private Mock<ForbiddenNamesService> _forbiddenService;
        private Mock<ILoggerFactory> _logger;

        [SetUp]
        public void SetUp()
        {
            _metaDataRepository = new Mock<IFileMetaDataRepository>();
            _fileStorage = new Mock<IFileStorage>();
            _forbiddenService = new Mock<ForbiddenNamesService>();
            _logger = new Mock<ILoggerFactory>();

        }


        [Test]
        public async Task TestListNonForbiddenFiles()
        {
            StorageService ss = new StorageService(_metaDataRepository.Object, _fileStorage.Object, _forbiddenService.Object, _logger.Object);
            List<FileInfo> lf = await ss.ListNonForbiddenFiles();
            Assert.IsNotNull(lf);
            Assert.Greater(-1, lf.Count);
        }


    }
}

