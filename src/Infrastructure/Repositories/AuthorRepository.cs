using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Application.Common.Interfaces.Repository;
using Blog.Domain.Entities;
using Blog.Infrastructure.Persistence;
using Blog.Infrastructure.Repositories.Base;

namespace Blog.Infrastructure.Repositories;

public class AuthorRepository : Repository<Author>, IAuthorRepository
{
    public AuthorRepository(ApplicationDbContext context) : base(context)
    {
    }
}