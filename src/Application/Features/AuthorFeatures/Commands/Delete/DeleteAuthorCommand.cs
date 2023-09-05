using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Application.Common.Wrappers;
using MediatR;

namespace Blog.Application.Features.AuthorFeatures.Commands.Delete;

public class DeleteAuthorCommand : IRequest<Response>
{
    public Guid Id { get; set; }
}
