using Blog.Application.Common.Exceptions;
using FluentAssertions;
using FluentValidation.Results;

namespace Application.UnitTests.Common.Exceptions;

public class ValidationExceptionTests
{
    [Test]
    public void DefaultConstructorCreatesAnEmptyErrorDictionary()
    {
        var actual = new ValidationException().Failures;

        actual.Keys.Should().BeEquivalentTo(Array.Empty<string>());
    }

    [Test]
    public void SingleValidationFailureCreatesASingleElementErrorDictionary()
    {
        var errKey = "Name";
        var errMessage = "must be at least 5 characters long";
        var failures = new List<ValidationFailure>
    {
        new ValidationFailure(errKey, errorMessage: errMessage),
    };

        var actual = new ValidationException(failures).Failures;

        actual.Keys.Should().BeEquivalentTo(new string[] { errKey });

        var errorCode = failures[0].ErrorCode;

        actual[errKey].Should().BeEquivalentTo(new string[] { $"{errorCode} : {errMessage}" });
    }

}