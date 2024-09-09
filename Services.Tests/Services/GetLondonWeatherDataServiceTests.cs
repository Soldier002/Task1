//using Moq;

//namespace Services.Tests.Services
//{
//    public class GetLondonWeatherDataServiceTests
//    {
//        private Mock<IOpenWeatherMapApiClient> _openWeatherMapApiClient;
//        private Mock<IBlobStorageRepository> _blobStorageRepository;

//        [OneTimeSetUp]
//        public void Setup()
//        {
//            _openWeatherMapApiClient = new Mock<IOpenWeatherMapApiClient>(MockBehavior.Strict);
//            _blobStorageRepository = new Mock<IBlobStorageRepository>(MockBehavior.Strict);
//        }

//        [Test]
//        public async Task GivenHappyPath_WhenExecute_ThenSavesWeatherDataToBlobStorage()
//        {
//            // arrange
//            var weatherData = "weatherData";
//            _openWeatherMapApiClient.Setup(x => x.GetWeatherInLondon()).Returns(Task.FromResult(weatherData));
//            _blobStorageRepository.Setup(x => x.SaveWeatherData(weatherData)).Returns(Task.CompletedTask);
//            var service = new GetLondonWeatherDataService(_openWeatherMapApiClient.Object, _blobStorageRepository.Object);

//            // act
//            await service.Execute();

//            // assert
//            _blobStorageRepository.Verify(x => x.SaveWeatherData(weatherData), Times.Once);
//        }
//    }
//}