using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Blog.Application.Features.PostFeatures.Events;

public class PostEventHandler : INotificationHandler<PostCreatedEvent> , INotificationHandler<PostDeletedEvent>
{
    private readonly ILogger _logger;
    public PostEventHandler(ILogger<PostEventHandler> logger)
    {
        _logger = logger;
        
    }
    public Task Handle(PostCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Post created with id: {notification.Id}");
        return Task.CompletedTask;
    }

    public Task Handle(PostDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Post deleted with id: {notification.Id}");
        return Task.CompletedTask;
    }
}