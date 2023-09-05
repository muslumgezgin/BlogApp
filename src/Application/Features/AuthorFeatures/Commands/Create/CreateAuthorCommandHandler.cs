using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Application.Common.Dtos.AuthorDtos;
using Blog.Application.Common.Exceptions;
using Blog.Application.Common.Interfaces.Repository.Base;
using Blog.Application.Common.Wrappers;
using Blog.Application.Features.AuthorFeatures.Events;
using Blog.Domain.Entities;
using MediatR;
using MediatR.Pipeline;

namespace Blog.Application.Features.AuthorFeatures.Commands.Create;

public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Response<AuthorModelDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublisher _publisher;

    public CreateAuthorCommandHandler(IUnitOfWork unitOfWork, IPublisher publisher)
    {
        this._unitOfWork = unitOfWork;
        this._publisher = publisher;
    }

    public async Task<Response<AuthorModelDto>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        if (request == null || string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.SurName))
        {
            throw new BadRequestException($"{nameof(CreateAuthorCommand)} request is null");
        }



        using var transaction = await _unitOfWork.BeginTransactionAsync();

        Author? authorEntity = null;

        try
        {

            authorEntity = await _unitOfWork.AuthorRepository.AddAsync(entity: new Author
            {
                Name = request.Name,
                SurName = request.SurName
            });

            await this._unitOfWork.CommitAsync();

            await transaction.CommitAsync(cancellationToken);

            await this._publisher.Publish(new AuthorCreatedEvent() { Id = authorEntity.Id, Name = authorEntity.Name }, cancellationToken);


        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }

        var authorModel = new AuthorModelDto
        {
            Id = authorEntity.Id,
            Name = authorEntity.Name,
            SurName = authorEntity.SurName
        };

        return new Response<AuthorModelDto>(authorModel);
    }
}