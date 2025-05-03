namespace JurassicCode.Db2;

public class Database
{
    public readonly Dictionary<string, Entities.ZoneEntity> Zones = new Dictionary<string, Entities.ZoneEntity>();
    private readonly Dictionary<string, Entities.DinosaurEntity> _dinosaurs = new Dictionary<string, Entities.DinosaurEntity>();

    public static Dictionary<string, Entities.DinosaurEntity> Dinosaurs(Database db)
    {
        return (Dictionary<string, Entities.DinosaurEntity>)ReflectionHelper.GetPrivateField(db, "_dinosaurs");
    }
}