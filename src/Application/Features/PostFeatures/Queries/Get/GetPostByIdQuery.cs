using Blog.Application.Common.Dtos.PostDtos;
using Blog.Application.Common.Wrappers;
using MediatR;

namespace Blog.Application.Features.PostFeatures.Queries.Get;

public class GetPostByIdQuery : IRequest<Response<PostModelWithAuthorDto>>
{
    public Guid Id { get; set; }

    public bool IncludeAuthor { get; set; } = false;
    
}