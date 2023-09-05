using Microsoft.EntityFrameworkCore.Storage;

namespace Blog.Application.Common.Interfaces.Repository.Base;

public interface IUnitOfWork : IDisposable, IAsyncDisposable
{

    IPostRepository PostRepository { get; }

    IAuthorRepository AuthorRepository { get; }

    Task<IDbContextTransaction> BeginTransactionAsync();
    Task<int> CommitAsync();

}