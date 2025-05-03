using JurassicCode.Domain.Entities;

namespace JurassicCode.Domain.Events;

public class DinosaurCreatedEvent : IDomainEvent
{
    public DinosaurCreatedEvent(Dinosaur dinosaur)
    {
        Dinosaur = dinosaur;
        OccurredOn = DateTime.Now;
    }
    
    public Dinosaur Dinosaur { get; }
    public DateTime OccurredOn { get; }
}

public class DinosaurHealthChangedEvent : IDomainEvent
{
    public DinosaurHealthChangedEvent(Dinosaur dinosaur)
    {
        Dinosaur = dinosaur;
        OccurredOn = DateTime.Now;
    }
    
    public Dinosaur Dinosaur { get; }
    public DateTime OccurredOn { get; }
}

public class DinosaurFedEvent : IDomainEvent
{
    public DinosaurFedEvent(Dinosaur dinosaur)
    {
        Dinosaur = dinosaur;
        OccurredOn = DateTime.Now;
    }
    
    public Dinosaur Dinosaur { get; }
    public DateTime OccurredOn { get; }
}

public class DinosaurAddedToZoneEvent : IDomainEvent
{
    public DinosaurAddedToZoneEvent(Zone zone, Dinosaur dinosaur)
    {
        Zone = zone;
        Dinosaur = dinosaur;
        OccurredOn = DateTime.Now;
    }
    
    public Zone Zone { get; }
    public Dinosaur Dinosaur { get; }
    public DateTime OccurredOn { get; }
}

public class DinosaurRemovedFromZoneEvent : IDomainEvent
{
    public DinosaurRemovedFromZoneEvent(Zone zone, Dinosaur dinosaur)
    {
        Zone = zone;
        Dinosaur = dinosaur;
        OccurredOn = DateTime.Now;
    }
    
    public Zone Zone { get; }
    public Dinosaur Dinosaur { get; }
    public DateTime OccurredOn { get; }
}