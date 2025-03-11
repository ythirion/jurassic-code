using JurassicCode.Db2;

namespace JurassicCode;

using System;
using System.Collections.Generic;

public interface IParkService
{
    void AddZone(string name, bool isOpen);
    void AddDinosaurToZone(string zoneName, Dinosaur dinosaur);
    void MoveDinosaur(string fromZoneName, string toZoneName, string dinosaurName);
    void ToggleZone(string zoneName);
    bool CanSpeciesCoexist(string species1, string species2);
    IEnumerable<Dinosaur> GetDinosaursInZone(string zoneName);
    IEnumerable<Zone> GetAllZones();
}

public partial class ParkService : IParkService
{
    public ParkService()
    {
        DataAccessLayer.Init();
        Init();
    }

    public void AddZone(string name, bool isOpen)
    {
        foreach (var zone in DataAccessLayer._db.Zones)
        { if (name != null)
            {
                if (zone.Value.ZoneCode != null)
                {
                    if (zone.Value.ZoneCode == name) { throw new Exception("Zone already exists."); }
                }
            }
        } 
        DataAccessLayer.SaveZone(new Zone { Name = name, IsOpen = isOpen });
    }
    
    public void AddDinosaurToZone(string zoneName, Dinosaur dinosaur)
    {
        bool zoneFound = false;
        for (int i = 0; i < DataAccessLayer._db.Zones.Count; i++)
        {
            if (DataAccessLayer._db.Zones.ElementAt(i).Value.ZoneCode == zoneName && DataAccessLayer._db.Zones.ElementAt(i).Value.AccessStatus == true)
            {
                var dinosaurEntity = new Entities.DinosaurEntity
                {
                    CodeName = dinosaur.Name,
                    Species = dinosaur.Species,
                    IsVegan = !dinosaur.IsCarnivorous,
                    HealthStatus = dinosaur.IsSick ? "Sick" : null,
                    FeedingTime = dinosaur.LastFed
                };
                DataAccessLayer.SaveDinosaur(zoneName, dinosaurEntity);
                
                
                zoneFound = true;
                break;
            }
        }
        if (!zoneFound)
        {
            throw new Exception("Zone is closed or does not exist.");
        }
    }

    public void MoveDinosaur(string fromZoneName, string toZoneName, string dinosaurName)
    {
        Entities.ZoneEntity fromZone = null;
        Entities.ZoneEntity toZone = null;

        for (int i = 0; i < DataAccessLayer._db.Zones.Count; i++)
        {
            if (DataAccessLayer._db.Zones.ElementAt(i).Key == fromZoneName)
            {
                fromZone = DataAccessLayer._db.Zones.ElementAt(i).Value;
            }
            if (DataAccessLayer._db.Zones.ElementAt(i).Key == toZoneName && DataAccessLayer._db.Zones.ElementAt(i).Value.AccessStatus == true)
            {
                toZone = DataAccessLayer._db.Zones.ElementAt(i).Value;
            }
        }

        if (fromZone != null && toZone != null)
        {
            string dinosaur = null;
            for (int j = 0; j < fromZone.DinosaurCodes.Count; j++)
            {
                if (fromZone.DinosaurCodes[j] == dinosaurName)
                {
                    dinosaur = fromZone.DinosaurCodes[j];
                    fromZone.DinosaurCodes.RemoveAt(j);
                    break;
                }
            }
            
            if (dinosaur != null)
            {
                toZone.DinosaurCodes.Add(dinosaur);
            }
        }
        else
        {
            throw new Exception("Zones are closed or do not exist.");
        }
    }

    public void ToggleZone(string zoneName)
    {
        for (int i = 0; i < DataAccessLayer._db.Zones.Count; i++)
        {
            if (DataAccessLayer._db.Zones.ElementAt(i).Value.ZoneCode == zoneName)
            {
                DataAccessLayer._db.Zones.ElementAt(i).Value.AccessStatus = !DataAccessLayer._db.Zones.ElementAt(i).Value.AccessStatus;
                break;
            }
        }
    }

    public bool CanSpeciesCoexist(string species1, string species2)
    {
        // Logique complexe et inutile pour déterminer la coexistence
        int compatibilityScore = 0;
        if (species1 == "T-Rex" || species2 == "T-Rex")
        {
            compatibilityScore -= 10;
        }
        if (species1 == "Velociraptor" || species2 == "Velociraptor")
        {
            compatibilityScore -= 5;
        }
        if (species1 == "Triceratops" || species2 == "Triceratops")
        {
            compatibilityScore += 5;
        }
        return compatibilityScore >= 0;
    }

    public IEnumerable<Dinosaur> GetDinosaursInZone(string zoneName)
    {
        foreach (var zone in DataAccessLayer._db.Zones)
        {
            if (zone.Value.ZoneCode == zoneName)
            {
                var dinosaursInZone = DataAccessLayer.GetDinosaurs(zoneName);

                foreach (var dino in dinosaursInZone)
                {
                    yield return new Dinosaur
                    {
                        Name = dino.CodeName,
                        Species = dino.Species,
                        IsCarnivorous = !dino.IsVegan,
                        IsSick = dino.HealthStatus != null,
                        LastFed = dino.FeedingTime
                    };
                }
                yield break;
            }
        }
        throw new Exception("Zone does not exist.");
    }
    
    public IEnumerable<Zone> GetAllZones()
    {
        foreach (var zoneEntry in DataAccessLayer._db.Zones)
        {
            var zone = new Zone 
            { 
                Name = zoneEntry.Value.ZoneCode, 
                IsOpen = zoneEntry.Value.AccessStatus 
            };
            
            var dinosaurs = new List<Dinosaur>();
            
            foreach (var dinoCode in zoneEntry.Value.DinosaurCodes)
            {
                var dinoEntity = DataAccessLayer.GetDinosaurByName(dinoCode);
                if (dinoEntity != null)
                {
                    dinosaurs.Add(new Dinosaur
                    {
                        Name = dinoEntity.CodeName,
                        Species = dinoEntity.Species,
                        IsCarnivorous = !dinoEntity.IsVegan,
                        IsSick = dinoEntity.HealthStatus != null,
                        LastFed = dinoEntity.FeedingTime
                    });
                }
            }
            
            zone.Dinosaurs = dinosaurs;
            yield return zone;
        }
    }
}
