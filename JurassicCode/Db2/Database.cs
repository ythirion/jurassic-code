namespace JurassicCode.Db2;

public class Database
{
    public readonly Dictionary<string, Entities.ZoneEntity> Zones = new Dictionary<string, Entities.ZoneEntity>();
    public readonly Dictionary<string, Entities.DinosaurEntity> Dinosaurs = new Dictionary<string, Entities.DinosaurEntity>();
}