using JurassicCode.DataAccess.Repositories;
using JurassicCode.DataAccess.Entities;

namespace JurassicCode;

using System;
using System.Collections.Generic;

public partial class ParkService : IParkService
{
    public ParkService()
    {
        DataAccessLayer.Init();
        Init();
    }

    public void AddZone(string name, bool isOpen)
    {
        foreach (var zone in DataAccessLayer.GetAllZones())
        { if (name != null)
            {
                if (zone.Value.ZoneCode != null)
                {
                    if (zone.Value.ZoneCode == name) { throw new Exception("Zone already exists."); }
                }
            }
        } 
        DataAccessLayer.SaveZone(name, isOpen, new List<string>());
    }
    
    public void AddDinosaurToZone(string zoneName, Dinosaur dinosaur)
    {
        bool zoneFound = false;
        for (int i = 0; i < DataAccessLayer.GetZoneCount(); i++)
        {
            var zoneEntry = DataAccessLayer.GetZoneAtIndex(i);
            if (zoneEntry.Value.ZoneCode == zoneName && zoneEntry.Value.AccessStatus == true)
            {
                var dinosaurEntity = new DinosaurEntity
                {
                    CodeName = dinosaur.Name,
                    Species = dinosaur.Species,
                    IsCarnivorous = dinosaur.IsCarnivorous,
                    IsSick = dinosaur.IsSick,
                    LastFed = dinosaur.LastFed
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
        ZoneEntity fromZone = null;
        ZoneEntity toZone = null;

        for (int i = 0; i < DataAccessLayer.GetZoneCount(); i++)
        {
            var zoneEntry = DataAccessLayer.GetZoneAtIndex(i);
            if (zoneEntry.Key == fromZoneName)
            {
                fromZone = zoneEntry.Value;
            }
            if (zoneEntry.Key == toZoneName && zoneEntry.Value.AccessStatus == true)
            {
                toZone = zoneEntry.Value;
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
        DataAccessLayer.ToggleZoneStatus(zoneName);
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
        foreach (var zone in DataAccessLayer.GetAllZones())
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
                        IsCarnivorous = dino.IsCarnivorous,
                        IsSick = dino.IsSick,
                        LastFed = dino.LastFed
                    };
                }
                yield break;
            }
        }
        throw new Exception("Zone does not exist.");
    }
    
    public IEnumerable<Zone> GetAllZones()
    {
        foreach (var zoneEntry in DataAccessLayer.GetAllZones())
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
                        IsCarnivorous = dinoEntity.IsCarnivorous,
                        IsSick = dinoEntity.IsSick,
                        LastFed = dinoEntity.LastFed
                    });
                }
            }
            
            zone.Dinosaurs = dinosaurs;
            yield return zone;
        }
    }
}