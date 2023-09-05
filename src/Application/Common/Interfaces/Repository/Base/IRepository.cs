using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Domain.Common;

namespace Blog.Application.Common.Interfaces.Repository.Base;

public interface IRepository<T> where T : BaseEntity
{

    Task<T?> GetByIdAsync(Guid id);

    Task<T> AddAsync(T entity);

    Task DeleteAsync(T entity);
    
}