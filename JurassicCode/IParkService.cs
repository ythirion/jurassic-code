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