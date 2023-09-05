using Blog.Application.Common.Dtos.AuthorDtos;
using Blog.Application.Common.Exceptions;
using Blog.Application.Common.Interfaces.Repository.Base;
using Blog.Application.Common.Wrappers;
using Blog.Domain.Entities;
using MediatR;

namespace Blog.Application.Features.AuthorFeatures.Queries.Get;

public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, Response<AuthorModelDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAuthorByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        this._unitOfWork = unitOfWork;
    }

    public async Task<Response<AuthorModelDto>> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {

        Author? entity = await this._unitOfWork.AuthorRepository.GetByIdAsync(request.Id);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Author), key: request.Id);
        }

        var authorModelDto = AuthorModelDto.ToAuthorModelDto(entity);


        return new Response<AuthorModelDto>(authorModelDto);

    }
}

