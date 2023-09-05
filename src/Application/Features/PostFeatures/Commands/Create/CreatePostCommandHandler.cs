using AutoMapper;
using Blog.Application.Common.Dtos.AuthorDtos;
using Blog.Application.Common.Dtos.PostDtos;
using Blog.Application.Common.Exceptions;
using Blog.Application.Common.Interfaces.Repository.Base;
using Blog.Application.Common.Wrappers;
using Blog.Application.Features.PostFeatures.Events;
using Blog.Domain.Entities;
using MediatR;

namespace Blog.Application.Features.PostFeatures.Commands.Create;

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Response<PostModelWithAuthorDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublisher _publisher;
    public CreatePostCommandHandler(IUnitOfWork unitOfWork, IPublisher publisher)
    {
        this._unitOfWork = unitOfWork;
        this._publisher = publisher;
    }
    public async Task<Response<PostModelWithAuthorDto>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            throw new BadRequestException($"{nameof(CreatePostCommand)} request is null");
        }

        using var transaction = await _unitOfWork.BeginTransactionAsync();


        var authorEntity = await _unitOfWork.AuthorRepository.GetByIdAsync(request.AuthorId) ?? throw new NotFoundException(nameof(Author), request.AuthorId);
        Post? postEntity = null;
        try
        {

            if (authorEntity == null)
            {
                throw new NotFoundException(nameof(Author), request.AuthorId);
            }


            postEntity = new Post
            {
                Title = request.Title,
                Content = request.Content,
                Description = request.Description,
                AuthorId = request.AuthorId
            };


            postEntity = await _unitOfWork.PostRepository.AddAsync(postEntity);

            await this._unitOfWork.CommitAsync();

            await transaction.CommitAsync(cancellationToken);

            await this._publisher.Publish(new PostCreatedEvent() { Id = postEntity.Id, Title = postEntity.Title }, cancellationToken);


        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }

        AuthorModelDto author = AuthorModelDto.ToAuthorModelDto(authorEntity);
        var postModelWithAuthorDto = new PostModelWithAuthorDto
        {
            Id = postEntity.Id,
            Title = postEntity.Title,
            Content = postEntity.Content,
            AuthorId = postEntity.AuthorId,
            Description = postEntity.Description,
            Author = author
        };


        return new Response<PostModelWithAuthorDto>(postModelWithAuthorDto);

    }
}