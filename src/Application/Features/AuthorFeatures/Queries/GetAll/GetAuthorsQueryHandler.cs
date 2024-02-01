using Blog.Application.Common.Dtos.AuthorDtos;
using Blog.Application.Common.Interfaces.Repository.Base;
using Blog.Application.Common.Wrappers;
using MediatR;

namespace Blog.Application.Features.AuthorFeatures.Queries.GetAll;

public class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, Response<List<AuthorModelDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAuthorsQueryHandler(IUnitOfWork unitOfWork)
    {
        this._unitOfWork = unitOfWork;
    }

    public async Task<Response<List<AuthorModelDto>>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
    {

        List<AuthorModelDto>? authors = await this._unitOfWork.AuthorRepository.GetAllAuthorsAsync(request);

        return new Response<List<AuthorModelDto>>(authors);

    }
}