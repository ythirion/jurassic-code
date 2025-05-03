using JurassicCode.Application.Interfaces;
using JurassicCode.Domain.Events;
using Microsoft.Extensions.Logging;

namespace JurassicCode.Infrastructure.Services;

/// <summary>
/// Implementation of domain event dispatcher
/// </summary>
public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly ILogger<DomainEventDispatcher> _logger;
    
    public DomainEventDispatcher(ILogger<DomainEventDispatcher> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    public Task DispatchEventsAsync(IEnumerable<IDomainEvent> events)
    {
        foreach (var @event in events)
        {
            // In a real implementation, use a proper event bus, message broker or mediator
            // to dispatch events to registered handlers
            _logger.LogInformation(
                "Domain event dispatched: {EventType} occurred at {OccurredOn}",
                @event.GetType().Name,
                @event.OccurredOn);
        }
        
        return Task.CompletedTask;
    }
}