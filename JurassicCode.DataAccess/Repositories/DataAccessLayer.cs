using System;
using System.Collections.Generic;
using System.Linq;
using JurassicCode.DataAccess.Entities;

namespace JurassicCode.DataAccess.Repositories
{
    public static class DataAccessLayer
    {
        private static Database _db;
        
        public static void Init(Database db) => _db = db;
        
        public static void Init() => _db = new Database();
        
        public static void SaveZone(string zoneName, bool isOpen, IEnumerable<string> dinosaurNames)
        {
            _db.Zones[zoneName] = new ZoneEntity
            {
                ZoneCode = zoneName, 
                AccessStatus = isOpen,
                DinosaurCodes = dinosaurNames.ToList()
            };
        }

        public static ZoneEntity GetZone(string zoneCode)
        {
            _db.Zones.TryGetValue(zoneCode, out var zone);
            return zone;
        }

        public static void SaveDinosaur(string zoneCode, DinosaurEntity dinosaur)
        {
            if (_db.Zones.TryGetValue(zoneCode, out var zone))
            {
                _db.GetDinosaurs().Add(dinosaur.CodeName, dinosaur);
                zone.DinosaurCodes.Add(dinosaur.CodeName);
            }
        }

        public static List<DinosaurEntity> GetDinosaurs(string zoneCode)
        {
            if (_db.Zones.TryGetValue(zoneCode, out var zone))
            {
                return zone.DinosaurCodes.Select(code => {
                    _db.GetDinosaurs().TryGetValue(code, out var dino);
                    return dino ?? new DinosaurEntity { CodeName = code };
                }).ToList();
            }
            return new List<DinosaurEntity>();
        }
        
        public static DinosaurEntity GetDinosaurByName(string dinoCode)
        {
            _db.GetDinosaurs().TryGetValue(dinoCode, out var dino);
            return dino;
        }
        
        // Methods to support existing ParkService implementation
        public static IEnumerable<KeyValuePair<string, ZoneEntity>> GetAllZones()
        {
            return _db.Zones;
        }
        
        public static int GetZoneCount()
        {
            return _db.Zones.Count;
        }
        
        public static KeyValuePair<string, ZoneEntity> GetZoneAtIndex(int index)
        {
            return _db.Zones.ElementAt(index);
        }
        
        public static void ToggleZoneStatus(string zoneName)
        {
            if (_db.Zones.TryGetValue(zoneName, out var zone))
            {
                zone.AccessStatus = !zone.AccessStatus;
            }
        }
    }
}