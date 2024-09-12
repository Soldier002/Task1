using Domain.Common.Strings;
using Functions.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functions.Tests.Validators
{
    public class DateTimeValidatorTests
    {
        [Test]
        public async Task GivenValidIso8601Date_WhenValidate_ReturnsSuccess()
        {
            // arrange
            var originalDateTime = new DateTime(2020, 9, 8, 10, 11, 12);
            var dateString = originalDateTime.ToString(DateTimeFormats.Iso8601Format);
            var validator = new DateTimeValidator();

            // act
            var result = validator.Validate(dateString, nameof(dateString));

            // assert
            Assert.That(result.Success, Is.True);
            Assert.That(DateTime.Compare(originalDateTime, result.Value) == 0, Is.True);
            Assert.That(string.IsNullOrWhiteSpace(result.ValidationMessages), Is.True);
        }

        [Test]
        public async Task GivenEmptyString_WhenValidate_ReturnsFailure()
        {
            // arrange
            var dateString = string.Empty;
            var validator = new DateTimeValidator();

            // act
            var result = validator.Validate(dateString, nameof(dateString));

            // assert
            Assert.That(result.Success, Is.False);
            Assert.That(string.IsNullOrWhiteSpace(result.ValidationMessages), Is.False);
            Assert.That(result.Value, Is.EqualTo(default(DateTime)));
        }

        [Test]
        public async Task GivenIncorrectlyFormattedString_WhenValidate_ReturnsFailure()
        {
            // arrange
            var originalDateTime = new DateTime(2020, 9, 8, 10, 11, 12);
            var dateString = originalDateTime.ToString("yyyy/MM/dd HH:mm:ss");
            var validator = new DateTimeValidator();

            // act
            var result = validator.Validate(dateString, nameof(dateString));

            // assert
            Assert.That(result.Success, Is.False);
            Assert.That(string.IsNullOrWhiteSpace(result.ValidationMessages), Is.False);
            Assert.That(result.Value, Is.EqualTo(default(DateTime)));
        }
    }
}
