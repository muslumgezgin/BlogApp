using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Application.Common.Exceptions;
using Blog.Application.Common.Interfaces.Repository.Base;
using Blog.Application.Common.Wrappers;
using Blog.Application.Features.PostFeatures.Events;
using Blog.Domain.Entities;
using MediatR;

namespace Blog.Application.Features.PostFeatures.Commands.Delete;
public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, Response>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublisher _publisher;

    public DeletePostCommandHandler(IUnitOfWork unitOfWork, IPublisher publisher)
    {
        this._unitOfWork = unitOfWork;
        this._publisher = publisher;
    }

    public async Task<Response> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        if (request == null || request.Id == Guid.Empty)
        {
            throw new BadRequestException($"{nameof(DeletePostCommand)} request is null");
        }

        using var transaction = await _unitOfWork.BeginTransactionAsync();

        var postEntity = await this._unitOfWork.PostRepository.GetByIdAsync(request.Id);

        if (postEntity == null)
        {
            throw new NotFoundException(nameof(Post), request.Id);
        }

        await this._unitOfWork.PostRepository.DeleteAsync(postEntity);

        await this._unitOfWork.CommitAsync();

        await transaction.CommitAsync(cancellationToken);

        await this._publisher.Publish(new PostDeletedEvent() { Id = postEntity.Id, Title = postEntity.Title }, cancellationToken);

        return new Response(true);

    }
}
