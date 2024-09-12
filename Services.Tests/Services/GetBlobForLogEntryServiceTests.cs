using Domain.Integration.ApiClients;
using Domain.Persistence.BlobStorage.Builders;
using Domain.Persistence.BlobStorage.Repositories;
using Domain.Persistence.TableStorage.Models.Dtos;
using Domain.Persistence.TableStorage.Models.Entities;
using Domain.Persistence.TableStorage.Repositories;
using Domain.Services.Services.Mappers;
using Moq;
using Moq.Protected;
using Services.Services;
using System.Net;
using System.Net.Http.Headers;
using TestsCommon.Extensions;

namespace Services.Tests.Services
{
    public class GetBlobForLogEntryServiceTests
    {
        private Mock<IBlobStorageRepository> _blobStorageRepository;
        private Mock<IBlobNameBuilder> _blobNameBuilder;

        [OneTimeSetUp]
        public void Setup()
        {
            _blobStorageRepository = MockUtils.Create<IBlobStorageRepository>();
            _blobNameBuilder = MockUtils.Create<IBlobNameBuilder>();
        }

        [Test]
        public async Task GivenHappyPath_WhenExecute_ThenReturnsCorrectData()
        {
            // arrange
            var partitionKey = "20201010";
            var rowKey = "151515";
            var ct = CancellationToken.None;
            var blobName = "20201010_151515_blop";
            var stream = MockUtils.Create<Stream>().Object;

            _blobNameBuilder.Setup(x => x.Build(It.Is<Keys>(k => k.PartitionKey == partitionKey && k.RowKey == rowKey))).Returns(blobName);
            _blobStorageRepository.Setup(x => x.GetWeatherData(blobName, ct)).Returns(Task.FromResult(stream));

            var service = new GetBlobForLogEntryService(_blobNameBuilder.Object, _blobStorageRepository.Object);

            // act
            var result = await service.Execute(partitionKey, rowKey, ct);

            // assert
            Assert.That(result == stream);
        }
    }
}