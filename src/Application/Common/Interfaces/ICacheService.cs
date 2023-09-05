using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Application.Common.Interfaces;
public interface ICacheService
{
    Task<T?> GetAsync<T>(string key);
    Task CreateAsync<T>(string key, T value, TimeSpan? absoluteExpiration = null);

}
