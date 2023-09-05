using AutoMapper;
using Blog.Application.Common.Dtos.AuthorDtos;
using Blog.Application.Common.Dtos.PostDtos;
using Blog.Application.Common.Exceptions;
using Blog.Application.Common.Interfaces.Repository.Base;
using Blog.Application.Common.Wrappers;
using Blog.Domain.Entities;
using MediatR;

namespace Blog.Application.Features.PostFeatures.Queries.Get;

public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, Response<PostModelWithAuthorDto>>
{

    private readonly IUnitOfWork _unitOfWork;
    public GetPostByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        this._unitOfWork = unitOfWork;
    }
    public async Task<Response<PostModelWithAuthorDto>> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
    {


        Post? entity = await this._unitOfWork.PostRepository.GetByIdAsync(request.Id);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Post), key: request.Id);
        }


        AuthorModelDto? authorModelDto = null;

        if (request.IncludeAuthor)
        {
            var authorEntity = await this._unitOfWork.AuthorRepository.GetByIdAsync(entity.AuthorId);

            if (authorEntity is not null)
            {
                authorModelDto = AuthorModelDto.ToAuthorModelDto(authorEntity);
            }
        }

        var postModelWithAuthorDto = new PostModelWithAuthorDto
        {

            Title = entity.Title,
            Content = entity.Content,
            Description = entity.Description,
            AuthorId = entity.AuthorId,
            Author = authorModelDto
        };
        return new Response<PostModelWithAuthorDto>(postModelWithAuthorDto);
    }
}