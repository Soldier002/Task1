using Domain.Persistence.TableStorage.Models.Entities;
using Domain.Persistence.TableStorage.Repositories;
using Moq;
using Services.Services;
using TestsCommon.Extensions;

namespace Services.Tests.Services
{
    public class GetLogsForPeriodServiceTests
    {
        private Mock<ITableStorageRepository> _tableStorageRepository;

        [OneTimeSetUp]
        public void Setup()
        {
            _tableStorageRepository = MockUtils.Create<ITableStorageRepository>();
        }

        [Test]
        public async Task GivenHappyPath_WhenExecute_ReturnsCorrectData()
        {
            // arrange
            var from = DateTime.Now;
            var to = DateTime.UtcNow;
            var ct = CancellationToken.None;
            IList<WeatherApiCallLog> weatherApiCallLogs = new List<WeatherApiCallLog>();

            _tableStorageRepository.Setup(x => x.GetAll(from, to, ct)).Returns(Task.FromResult(weatherApiCallLogs));

            var service = new GetLogsForPeriodService(_tableStorageRepository.Object);

            // act
            var result = await service.Execute(from, to, ct);

            // assert
            Assert.That(result == weatherApiCallLogs);
        }
    }
}