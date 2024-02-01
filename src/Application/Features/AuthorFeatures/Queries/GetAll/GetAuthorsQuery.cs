using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Application.Common.Dtos.AuthorDtos;
using Blog.Application.Common.Wrappers;
using MediatR;

namespace Blog.Application.Features.AuthorFeatures.Queries.GetAll;

public class GetAuthorsQuery : IRequest<Response<List<AuthorModelDto>>>
{
    public string? SearchTerm { get; set; }
    public int? Page { get; set; }
    public int? PageSize { get; set; }

    public string SortOrder { get; set; } = "desc";

}
