namespace JurassicCode.Domain.Exceptions;

public abstract class DomainException : Exception
{
    protected DomainException(string message) : base(message)
    {
    }
}

public class DinosaurDomainException : DomainException
{
    public DinosaurDomainException(string message) : base(message)
    {
    }
}

public class ZoneDomainException : DomainException
{
    public ZoneDomainException(string message) : base(message)
    {
    }
}

public class ParkManagementException : DomainException
{
    public ParkManagementException(string message) : base(message)
    {
    }
}