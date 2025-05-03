using JurassicCode.Domain.Events;

namespace JurassicCode.Application.Interfaces;

/// <summary>
/// Domain event dispatcher interface
/// </summary>
public interface IDomainEventDispatcher
{
    Task DispatchEventsAsync(IEnumerable<IDomainEvent> events);
}