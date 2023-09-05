using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Blog.Application.Common.Exceptions;
using Blog.Application.Features.AuthorFeatures.Commands.Create;
using Blog.Application.Features.AuthorFeatures.Commands.Delete;

namespace Blog.Application.FunctionalTests.Features.PostFeaturesTests.Commands;
using static Testing;

public class CreateAuthorTest : BaseTestFixture
{

    [Test]
    public async Task ShouldCreateAuthor()
    {
        // Arrange
        var command = new CreateAuthorCommand
        {
            Name = "Test Author",
            SurName = "Test SurName"
        };

        // Act
        var response = await SendAsync(command);

        // Assert
        response.Should().NotBeNull();
        response.Data.Should().NotBeNull();
        response.Data.Id.Should().NotBeEmpty();
        response.Data.Name.Should().Be(command.Name);
        response.Data.SurName.Should().Be(command.SurName);

        // clean what we created

        var deleteCommand = new DeleteAuthorCommand
        {
            Id = response.Data.Id
        };

        var deletedResponse = await SendAsync(deleteCommand);

        deletedResponse.Should().NotBeNull();
        deletedResponse.Succeeded.Should().BeTrue();
    }

    [Test]
    public async Task ShouldNotCreateAuthorWithNullData()
    {
        // Arrange
        var command = new CreateAuthorCommand();

        // Act
        var response =  FluentActions.Invoking(() => SendAsync(command));

        // Assert
        await response.Should().ThrowAsync<BadRequestException>();
    }


}
