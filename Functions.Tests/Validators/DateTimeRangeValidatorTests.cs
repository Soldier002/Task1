using Domain.Functions.Validators;
using Domain.Functions.Validators.Models;
using Functions.Validators;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsCommon.Extensions;

namespace Functions.Tests.Validators
{
    public class DateTimeRangeValidatorTests
    {
        private Mock<IDateTimeValidator> _dateTimeValidator;

        [OneTimeSetUp]
        public void SetUp()
        {
            _dateTimeValidator = MockUtils.Create<IDateTimeValidator>();
        }

        [Test]
        public async Task GivenCorrectTimeRange_WhenValidate_ReturnsSuccess()
        {
            // arrange
            var from = "2020-10-10T10:12:14";
            var fromName = nameof(from);
            var to = "2025-10-10T11:13:15";
            var toName = nameof(to);
            var validationResultFrom = new ValidationResult<DateTime>
            {
                Success = true,
                Value = new DateTime(2000, 01, 01, 10, 10, 10),
            };

            var validationResultTo = new ValidationResult<DateTime>
            {
                Success = true,
                Value = DateTime.UtcNow,
            };

            _dateTimeValidator.Setup(x => x.Validate(from, fromName)).Returns(validationResultFrom);
            _dateTimeValidator.Setup(x => x.Validate(to, toName)).Returns(validationResultTo);

            var validator = new DateTimeRangeValidator(_dateTimeValidator.Object);

            // act
            var result = validator.Validate(from, to, fromName, toName);

            // assert
            Assert.That(result.Success, Is.True);
            Assert.That(result.Value.From, Is.EqualTo(validationResultFrom.Value));
            Assert.That(result.Value.To, Is.EqualTo(validationResultTo.Value));
        }

        [Test]
        public async Task GivenTimeRangeWhenFromIsLaterThanTo_WhenValidate_ReturnsFailure()
        {
            // arrange
            var from = "2020-10-10T10:12:14";
            var fromName = nameof(from);
            var to = "2025-10-10T11:13:15";
            var toName = nameof(to);
            var validationResultFrom = new ValidationResult<DateTime>
            {
                Success = true,
                Value = DateTime.UtcNow
            };

            var validationResultTo = new ValidationResult<DateTime>
            {
                Success = true,
                Value = new DateTime(2000, 01, 01, 10, 10, 10),
            };

            _dateTimeValidator.Setup(x => x.Validate(from, fromName)).Returns(validationResultFrom);
            _dateTimeValidator.Setup(x => x.Validate(to, toName)).Returns(validationResultTo);

            var validator = new DateTimeRangeValidator(_dateTimeValidator.Object);

            // act
            var result = validator.Validate(from, to, fromName, toName);

            // assert
            Assert.That(result.Success, Is.False);
            Assert.That(result.Value, Is.Null);
            Assert.That(string.IsNullOrWhiteSpace(result.ValidationMessages), Is.False);
        }

        private static IEnumerable<TestCaseData> GetTestCases()
        {
            yield return new TestCaseData(false, false);
            yield return new TestCaseData(false, true);
            yield return new TestCaseData(true, false);
        }

        [Test, TestCaseSource(nameof(GetTestCases))]
        public async Task GivenEitherDateTimeFailsValidation_WhenValidate_ReturnsFailure(bool successFrom, bool successTo)
        {
            // arrange
            var from = "2020-10-10T10:12:14";
            var fromName = nameof(from);
            var to = "2025-10-10T11:13:15";
            var toName = nameof(to);
            var validationResultTo = new ValidationResult<DateTime>
            {
                Success = successFrom,
                Value = new DateTime(2000, 01, 01, 10, 10, 10),
                ValidationMessages = "not valid date"
            };
            var validationResultFrom = new ValidationResult<DateTime>
            {
                Success = successTo,
                Value = DateTime.UtcNow,
                ValidationMessages = "not valid date"
            };

            _dateTimeValidator.Setup(x => x.Validate(from, fromName)).Returns(validationResultFrom);
            _dateTimeValidator.Setup(x => x.Validate(to, toName)).Returns(validationResultTo);

            var validator = new DateTimeRangeValidator(_dateTimeValidator.Object);

            // act
            var result = validator.Validate(from, to, fromName, toName);

            // assert
            Assert.That(result.Success, Is.False);
            Assert.That(result.Value, Is.Null);
            Assert.That(string.IsNullOrWhiteSpace(result.ValidationMessages), Is.False);
        }
    }
}
