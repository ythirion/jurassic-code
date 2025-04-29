namespace JurassicCode.Domain.Events;

/// <summary>
/// Base interface for all domain events
/// </summary>
public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}