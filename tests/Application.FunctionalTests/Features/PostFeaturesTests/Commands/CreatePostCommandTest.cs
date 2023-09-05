using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Blog.Application.Common.Exceptions;
using Blog.Application.Features.AuthorFeatures.Commands.Create;
using Blog.Application.Features.AuthorFeatures.Commands.Delete;
using Blog.Application.Features.PostFeatures.Commands.Create;
using Blog.Application.Features.PostFeatures.Commands.Delete;

namespace Blog.Application.FunctionalTests.Features.PostFeaturesTests.Commands;

using static Testing;
public class CreatePostCommandTest : BaseTestFixture
{


    [Test]
    public async Task ShoulThrowExceptionForAnUnvalidAuthorID()
    {

        var command = new CreatePostCommand
        {
            Title = "Test Title",
            Content = "Test Content",
            AuthorId = Guid.NewGuid()
        };

        var res = FluentActions.Invoking(() => SendAsync(command));

        await res.Should().ThrowAsync<NotFoundException>();
    }



    [Test]
    public async Task ShouldSuccessFlow()
    {

        var authorCommand = new CreateAuthorCommand
        {
            Name = "Test Name",
            SurName = "Test SurName"
        };

        var responseAuthor = await SendAsync(authorCommand);

        var command = new CreatePostCommand
        {
            Title = "Test Title",
            Content = "Test Content",
            AuthorId = responseAuthor.Data.Id
        };


        var response = await SendAsync(command);

        response.Should().NotBeNull();
        response.Data.Should().NotBeNull();
        response.Data.Title.Should().Be(command.Title);
        response.Data.AuthorId.Should().Be(command.AuthorId);


        // should delete created post

        var deletePostCommand = new DeletePostCommand
        {
            Id = response.Data.Id
        };

        var deletePostResponse = await SendAsync(deletePostCommand);

        deletePostResponse.Succeeded.Should().BeTrue();


        // should delete created author

        var deleteAuthorCommand = new DeleteAuthorCommand
        {
            Id = responseAuthor.Data.Id
        };


        var deleteAuthorResponse = await SendAsync(deleteAuthorCommand);

        deleteAuthorResponse.Succeeded.Should().BeTrue();

    }


}