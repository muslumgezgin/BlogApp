using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Application.Common.Interfaces;
using Blog.Application.Common.Interfaces.Repository;
using Blog.Application.Common.Interfaces.Repository.Base;
using Blog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Storage;

namespace Blog.Infrastructure.Repositories.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        public IPostRepository PostRepository { get; set; }

        public IAuthorRepository AuthorRepository { get; set; }

        private readonly IApplicationDbContext _context;

        public UnitOfWork(IApplicationDbContext context)
        {
            this._context = context;
            this.PostRepository = new PostRepository((ApplicationDbContext)_context);
            this.AuthorRepository = new AuthorRepository((ApplicationDbContext)_context);
        }


        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await((ApplicationDbContext)this._context).Database.BeginTransactionAsync();

            throw new NotImplementedException();
        }

        public async Task<int> CommitAsync()
        {
            return await this._context.SaveChangesAsync(CancellationToken.None);

        }

        public void Dispose()
        {

            ((ApplicationDbContext)this._context).Dispose();
        }

        public async ValueTask DisposeAsync()
        {

            await ((ApplicationDbContext)this._context).DisposeAsync();

        }
    }
}