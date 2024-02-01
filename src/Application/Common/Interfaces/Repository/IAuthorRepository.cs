using Blog.Application.Common.Dtos.AuthorDtos;
using Blog.Application.Common.Interfaces.Repository.Base;
using Blog.Application.Features.AuthorFeatures.Queries.GetAll;
using Blog.Domain.Entities;

namespace Blog.Application.Common.Interfaces.Repository;

public interface IAuthorRepository : IRepository<Author>
{
    Task<List<AuthorModelDto>> GetAllAuthorsAsync(GetAuthorsQuery request);
}