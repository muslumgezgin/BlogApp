using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Domain.Common;
using Blog.Domain.Entities;

namespace Blog.Application.Common.Dtos.AuthorDtos;

public class AuthorModelDto : BaseEntity
{

    public string Name { get; set; } = string.Empty;

    public string SurName { get; set; } = string.Empty;


    public static AuthorModelDto ToAuthorModelDto(Author author)
    {
        return new AuthorModelDto
        {
            Id = author.Id,
            Name = author.Name,
            SurName = author.SurName
        };
    }

}