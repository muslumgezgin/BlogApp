using System.Text.Json.Serialization;
using Blog.Application.Common.Dtos.PostDtos;
using Blog.Application.Common.Wrappers;
using MediatR;

namespace Blog.Application.Features.PostFeatures.Commands.Create;

public class CreatePostCommand : IRequest<Response<PostModelWithAuthorDto>>
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    public  Guid AuthorId { get; set; }

}
