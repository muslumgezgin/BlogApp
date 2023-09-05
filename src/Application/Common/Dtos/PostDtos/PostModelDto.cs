using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Domain.Common;

namespace Blog.Application.Common.Dtos.PostDtos;

public class PostModelDto : BaseEntity
{

    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    public Guid AuthorId { get; set; }

}