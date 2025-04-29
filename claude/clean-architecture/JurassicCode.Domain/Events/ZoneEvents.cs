using JurassicCode.Domain.Entities;

namespace JurassicCode.Domain.Events;

public class ZoneCreatedEvent : IDomainEvent
{
    public ZoneCreatedEvent(Zone zone)
    {
        Zone = zone;
        OccurredOn = DateTime.Now;
    }
    
    public Zone Zone { get; }
    public DateTime OccurredOn { get; }
}

public class ZoneStatusChangedEvent : IDomainEvent
{
    public ZoneStatusChangedEvent(Zone zone)
    {
        Zone = zone;
        OccurredOn = DateTime.Now;
    }
    
    public Zone Zone { get; }
    public DateTime OccurredOn { get; }
}