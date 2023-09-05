using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Application.Common.Interfaces.Repository.Base;
using Blog.Domain.Entities;

namespace Blog.Application.Common.Interfaces.Repository
{
    public interface IAuthorRepository : IRepository<Author>
    {
     
    }
}