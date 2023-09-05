using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Blog.Application.Features.AuthorFeatures.Events;

public class AuthorCreatedEvent : INotification
{
    public Guid Id { get; set; }

    public string Name { get; set; } = String.Empty;

}
