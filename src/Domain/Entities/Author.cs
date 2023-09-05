using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Domain.Entities;

public class Author : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public string SurName { get; set; } = string.Empty;

}