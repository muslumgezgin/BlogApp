using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Application.Common.Dtos.AuthorDtos;
using Blog.Application.Common.Interfaces.Repository;
using Blog.Application.Features.AuthorFeatures.Queries.GetAll;
using Blog.Domain.Entities;
using Blog.Infrastructure.Persistence;
using Blog.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repositories;

public class AuthorRepository : Repository<Author>, IAuthorRepository
{

    private readonly DbSet<Author> _dbSet;
    public AuthorRepository(ApplicationDbContext context) : base(context)
    {
        this._dbSet = context.Set<Author>();
    }

    public async Task<List<AuthorModelDto>> GetAllAuthorsAsync(GetAuthorsQuery request)

    {
        IQueryable<Author> query =this._dbSet;

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            query = query.Where(x => x.Name.Contains(request.SearchTerm) || x.SurName.Contains(request.SearchTerm));
        }

        if (request.SortOrder == "desc")
        {
            query = query.OrderByDescending(x => x.Id);
        }
        else
        {
            query = query.OrderBy(x => x.Id);
        }

        request.PageSize ??= 10;
        request.Page ??= 1;

        int skip = (request.Page.Value - 1) * request.PageSize.Value;

        var authors = query
                .Skip(skip).Take((int)request.PageSize)
                .Select(x => new AuthorModelDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    SurName = x.SurName
                })
                .AsNoTracking();

        return await authors.ToListAsync();
    }
}