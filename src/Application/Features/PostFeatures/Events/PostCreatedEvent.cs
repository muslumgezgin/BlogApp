using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Blog.Application.Features.PostFeatures.Events;

public class PostCreatedEvent : INotification
{
    public Guid Id { get; set; }

    public string Title { get; set; } = String.Empty;

}