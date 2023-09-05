using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Application.Common.Wrappers;
using MediatR;

namespace Blog.Application.Features.PostFeatures.Commands.Delete
{
    public class DeletePostCommand : IRequest<Response>
    {
        public Guid Id { get; set; }
        
    }
}