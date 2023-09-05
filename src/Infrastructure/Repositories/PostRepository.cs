using Blog.Application.Common.Interfaces.Repository;
using Blog.Domain.Entities;
using Blog.Infrastructure.Persistence;
using Blog.Infrastructure.Repositories.Base;

namespace Blog.Infrastructure.Repositories;

public class PostRepository : Repository<Post>, IPostRepository
{
    public PostRepository(ApplicationDbContext context) : base(context: context)
    {
    }
}