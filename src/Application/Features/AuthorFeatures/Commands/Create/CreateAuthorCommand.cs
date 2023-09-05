using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Application.Common.Dtos.AuthorDtos;
using Blog.Application.Common.Wrappers;
using MediatR;

namespace Blog.Application.Features.AuthorFeatures.Commands.Create;

public class CreateAuthorCommand : IRequest<Response<AuthorModelDto>>
{

    public string Name { get; set; } = string.Empty;

    public string SurName { get; set; } = string.Empty;

}