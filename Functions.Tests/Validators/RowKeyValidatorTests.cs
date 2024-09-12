using Domain.Common.Strings;
using Functions.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functions.Tests.Validators
{
    public class RowKeyValidatorTests
    {
        [Test]
        public async Task GivenCorrectlyFormattedKey_WhenValidate_ReturnsSuccess()
        {
            // arrange
            var originalDateTime = new DateTime(2020, 9, 8, 10, 11, 12);
            var rowKey = originalDateTime.ToString(DateTimeFormats.RowKeyTimeFormat);
            var validator = new RowKeyValidator();

            // act
            var result = validator.Validate(rowKey, nameof(rowKey));

            // assert
            Assert.That(result.Success, Is.True);
            Assert.That(rowKey.CompareTo(result.Value) == 0, Is.True);
            Assert.That(string.IsNullOrWhiteSpace(result.ValidationMessages), Is.True);
        }

        [Test]
        public async Task GivenEmptyString_WhenValidate_ReturnsFailure()
        {
            // arrange
            var rowKey = string.Empty;
            var validator = new RowKeyValidator();

            // act
            var result = validator.Validate(rowKey, nameof(rowKey));

            // assert
            Assert.That(result.Success, Is.False);
            Assert.That(string.IsNullOrWhiteSpace(result.ValidationMessages), Is.False);
            Assert.That(result.Value, Is.EqualTo(default(string)));
        }

        [Test]
        public async Task GivenIncorrectlyFormattedString_WhenValidate_ReturnsFailure()
        {
            // arrange
            var originalDateTime = new DateTime(2020, 9, 8, 10, 11, 12);
            var rowKey = originalDateTime.ToString("HH:mm");
            var validator = new RowKeyValidator();

            // act
            var result = validator.Validate(rowKey, nameof(rowKey));

            // assert
            Assert.That(result.Success, Is.False);
            Assert.That(string.IsNullOrWhiteSpace(result.ValidationMessages), Is.False);
            Assert.That(result.Value, Is.EqualTo(default(string)));
        }
    }
}
