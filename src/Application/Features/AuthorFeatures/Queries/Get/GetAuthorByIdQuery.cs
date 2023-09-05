using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Application.Common.Dtos.AuthorDtos;
using Blog.Application.Common.Wrappers;
using MediatR;

namespace Blog.Application.Features.AuthorFeatures.Queries.Get
{
    public class GetAuthorByIdQuery : IRequest<Response<AuthorModelDto>>
    {

        public Guid Id { get; set; }
        
    }
}