using Domain.Integration.ApiClients;
using Domain.Persistence.BlobStorage.Builders;
using Domain.Persistence.BlobStorage.Repositories;
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
    public class GetLondonWeatherDataServiceTests
    {
        private Mock<IOpenWeatherMapApiClient> _openWeatherMapApiClient;
        private Mock<IBlobStorageRepository> _blobStorageRepository;
        private Mock<HttpMessageHandler> _httpMessageHandler;
        private Mock<IWeatherApiCallLogMapper> _weatherApiCallLogMapper;
        private Mock<IBlobNameBuilder> _blobNameBuilder;
        private Mock<ITableStorageRepository> _tableStorageRepository;

        [OneTimeSetUp]
        public void Setup()
        {
            _openWeatherMapApiClient = MockUtils.Create<IOpenWeatherMapApiClient>();
            _blobStorageRepository = MockUtils.Create<IBlobStorageRepository>();
            _httpMessageHandler = MockUtils.Create<HttpMessageHandler>();
            _weatherApiCallLogMapper = MockUtils.Create<IWeatherApiCallLogMapper>();
            _blobNameBuilder = MockUtils.Create<IBlobNameBuilder>();
            _tableStorageRepository = MockUtils.Create<ITableStorageRepository>();
        }

        [Test]
        public async Task GivenHappyPath_WhenExecute_ThenSavesData()
        {
            // arrange
            var now = DateTime.UtcNow;
            var blobName = "20201010_123456_blop";
            var ct = CancellationToken.None;
            var httpContent = new StringContent("weatherApiData");
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = httpContent,
            };
            var httpRequestMessage = new HttpRequestMessage();
            var weatherApiCallLog = new WeatherApiCallLog();

            _httpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", httpRequestMessage, ct)
                .Returns(Task.FromResult(httpResponseMessage));
            _blobNameBuilder.Setup(x => x.Build(now)).Returns(blobName);
            _weatherApiCallLogMapper.Setup(x => x.Map(httpResponseMessage, now, blobName)).Returns(weatherApiCallLog);
            _blobStorageRepository.Setup(x => x.SaveWeatherData(It.IsAny<Stream>(), now, ct)).Returns(Task.CompletedTask);
            _tableStorageRepository.Setup(x => x.Save(weatherApiCallLog, ct)).Returns(Task.CompletedTask);
            _openWeatherMapApiClient.Setup(x => x.GetWeatherInLondon(ct)).Returns(Task.FromResult(httpResponseMessage));

            var service = new GetLondonWeatherDataService(_openWeatherMapApiClient.Object, _blobStorageRepository.Object, _tableStorageRepository.Object, _weatherApiCallLogMapper.Object, _blobNameBuilder.Object);

            // act
            await service.Execute(now, ct);

            // assert
            _blobStorageRepository.Verify(x => x.SaveWeatherData(It.IsAny<Stream>(), now, ct), Times.Once);
            _tableStorageRepository.Verify(x => x.Save(weatherApiCallLog, ct), Times.Once);
        }

        [Test]
        public async Task GivenFailedRequest_WhenExecute_ThenBlobIsNotSaved()
        {
            // arrange
            var now = DateTime.UtcNow;
            var blobName = "20201010_123456_blop";
            var ct = CancellationToken.None;
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
            };
            var httpRequestMessage = new HttpRequestMessage();
            var weatherApiCallLog = new WeatherApiCallLog { PayloadBlobName = blobName };

            _httpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", httpRequestMessage, ct)
                .Returns(Task.FromResult(httpResponseMessage));
            _blobNameBuilder.Setup(x => x.Build(now)).Returns(blobName);
            _weatherApiCallLogMapper.Setup(x => x.Map(httpResponseMessage, now, blobName)).Returns(weatherApiCallLog);
            _tableStorageRepository.Setup(x => x.Save(weatherApiCallLog, ct)).Returns(Task.CompletedTask);
            _openWeatherMapApiClient.Setup(x => x.GetWeatherInLondon(ct)).Returns(Task.FromResult(httpResponseMessage));

            var service = new GetLondonWeatherDataService(_openWeatherMapApiClient.Object, _blobStorageRepository.Object, _tableStorageRepository.Object, _weatherApiCallLogMapper.Object, _blobNameBuilder.Object);

            // act
            await service.Execute(now, ct);

            // assert
            Assert.That(weatherApiCallLog.PayloadBlobName == null, Is.True);
            _tableStorageRepository.Verify(x => x.Save(weatherApiCallLog, ct), Times.Once);
        }
    }
}