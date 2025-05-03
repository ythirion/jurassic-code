namespace JurassicCode.Db2;

public class Entities
{
    public class DinosaurEntity
    {
        public string CodeName { get; set; }
        public string Species { get; set; }
        public bool IsVegan { get; set; }
        public string? HealthStatus { get; set; }
        public DateTime FeedingTime { get; set; }
    }

    public class ZoneEntity
    {
        public string ZoneCode { get; set; }
        public bool AccessStatus { get; set; }
        public List<string> DinosaurCodes { get; set; } = new List<string>();
    }
}