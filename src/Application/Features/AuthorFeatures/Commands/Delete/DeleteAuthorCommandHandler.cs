using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Application.Common.Exceptions;
using Blog.Application.Common.Interfaces.Repository.Base;
using Blog.Application.Common.Wrappers;
using Blog.Application.Features.AuthorFeatures.Events;
using Blog.Domain.Entities;
using MediatR;

namespace Blog.Application.Features.AuthorFeatures.Commands.Delete;

public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, Response>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublisher _publisher;

    public DeleteAuthorCommandHandler(IUnitOfWork unitOfWork, IPublisher publisher)
    {
        this._unitOfWork = unitOfWork;
        this._publisher = publisher;
    }

    public async Task<Response> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        if (request == null || request.Id == Guid.Empty)
        {
            throw new BadRequestException($"{nameof(DeleteAuthorCommand)} request is null");
        }

        using var transaction = await _unitOfWork.BeginTransactionAsync();


        var authorEntity = await this._unitOfWork.AuthorRepository.GetByIdAsync(request.Id);


        if (authorEntity == null)
        {
            throw new NotFoundException(nameof(Author), request.Id);
        }

        await this._unitOfWork.AuthorRepository.DeleteAsync(authorEntity);

        await this._unitOfWork.CommitAsync();
        await transaction.CommitAsync(cancellationToken);

        await this._publisher.Publish(new AuthorDeletedEvent() { Id = authorEntity.Id, Name = authorEntity.Name }, cancellationToken);

        return new Response(true);

    }
}
