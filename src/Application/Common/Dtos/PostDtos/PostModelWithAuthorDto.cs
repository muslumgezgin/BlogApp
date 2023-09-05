using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Application.Common.Dtos.AuthorDtos;
using Blog.Domain.Common;
using Blog.Domain.Entities;

namespace Blog.Application.Common.Dtos.PostDtos;

public class PostModelWithAuthorDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    public Guid AuthorId { get; set; }

    public AuthorModelDto? Author { get; set; }

}
