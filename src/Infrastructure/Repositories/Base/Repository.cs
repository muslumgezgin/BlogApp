using Blog.Application.Common.Interfaces.Repository.Base;
using Blog.Domain.Common;
using Blog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repositories.Base;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    DbSet<T> _dbSet;

    private readonly ApplicationDbContext _context;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = this._context.Set<T>();
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
        }

        await this._dbSet.AddAsync(entity);

        return entity;
    }

    public async Task DeleteAsync(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException($"{nameof(DeleteAsync)} entity must not be null");
        }

        T? existingEntity = await this._dbSet.FindAsync(entity.Id);

        if (existingEntity == null)
        {
            throw new ArgumentNullException($"{nameof(DeleteAsync)} entity not found in the db!");
        }

        this._dbSet.Remove(existingEntity);

    }

    public async Task<T?> GetByIdAsync(Guid id) => await this._dbSet.FindAsync(id);
}