using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Blog.Application.Features.AuthorFeatures.Events;

public class AuthorEventHandler : INotificationHandler<AuthorCreatedEvent>, INotificationHandler<AuthorDeletedEvent>
{
    private readonly ILogger _logger;

    public AuthorEventHandler(ILogger<AuthorEventHandler> logger)
    {
        _logger = logger;
    }
    public Task Handle(AuthorCreatedEvent notification, CancellationToken cancellationToken)
    {

        _logger.LogInformation($"Author created with id: {notification.Id}");
        return Task.CompletedTask;
    }

    public Task Handle(AuthorDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Author deleted with id: {notification.Id}");
        return Task.CompletedTask;
    }
}

