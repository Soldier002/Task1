using Domain.Persistence.TableStorage.Models.Dtos;
using Persistence.BlobStorage.Builders;

namespace Persistence.Tests.BlobStorage.Builders
{
    public class BlobNameBuilderTests
    {
        [Test]
        public async Task GivenParams_WhenBuild_CreatesIdenticalAndCorrectBlobNameFromAllMethods()
        {
            // arrange
            var dateTime = new DateTime(2020, 10, 11, 15, 16, 17);
            var keys = new Keys
            {
                PartitionKey = "20201011",
                RowKey = "151617"
            };
            var blobNameBuilder = new BlobNameBuilder();

            // act
            var dateTimeResult = blobNameBuilder.Build(dateTime);
            var keysResult = blobNameBuilder.Build(keys);

            // assert
            Assert.That(dateTimeResult, Is.EqualTo("20201011_151617_weather"));
            Assert.That(dateTimeResult.CompareTo(keysResult), Is.EqualTo(0));
        } 
    }
}
