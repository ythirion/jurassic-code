using System.Runtime.CompilerServices;

namespace JurassicCode.Db2;

public static class DataAccessLayer
{
    public static Database _db;
    public static void Init(Database db) => _db = db;
    
    public static void Init() => _db = new Database();
    
    public static void SaveZone(Zone zone)
    {
        _db.Zones[zone.Name] = new Entities.ZoneEntity
        {
            ZoneCode = zone.Name, 
            AccessStatus = zone.IsOpen,
            DinosaurCodes = zone.Dinosaurs.Select(d => d.Name).ToList()
        };
    }

    public static Entities.ZoneEntity GetZone(string zoneCode)
    {
        _db.Zones.TryGetValue(zoneCode, out var zone);
        return zone;
    }

    public static void SaveDinosaur(string zoneCode, Entities.DinosaurEntity dinosaur)
    {
        if (_db.Zones.TryGetValue(zoneCode, out var zone))
        {
            _db.Dinosaurs.Add(dinosaur.CodeName, dinosaur);
            zone.DinosaurCodes.Add(dinosaur.CodeName);
        }
    }

    public static List<Entities.DinosaurEntity> GetDinosaurs(string zoneCode)
    {
        if (_db.Zones.TryGetValue(zoneCode, out var zone))
        {
            return zone.DinosaurCodes.Select(code => new Entities.DinosaurEntity { CodeName = code }).ToList();
        }
        return new List<Entities.DinosaurEntity>();
    }
    
    public static Entities.DinosaurEntity GetDinosaurByName(string dinoCode)
    {
        _db.Dinosaurs.TryGetValue(dinoCode, out var dino);
        return dino;
    }
}