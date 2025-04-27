using System;
using System.Collections.Generic;
using JurassicCode.DataAccess.Entities;
using JurassicCode.DataAccess.Repositories;

namespace JurassicCode.DataAccess.Interfaces
{
    /// <summary>
    /// Interface for data access operations
    /// </summary>
    public interface IDataAccessLayer
    {
        /// <summary>
        /// Initialize the database with a specific instance
        /// </summary>
        /// <param name="db">The database instance</param>
        void Init(Database db);

        /// <summary>
        /// Initialize the database with a new instance
        /// </summary>
        void Init();

        /// <summary>
        /// Save a zone to the database
        /// </summary>
        /// <param name="zoneName">Name of the zone</param>
        /// <param name="isOpen">Whether the zone is open</param>
        /// <param name="dinosaurNames">Names of dinosaurs in the zone</param>
        void SaveZone(string zoneName, bool isOpen, IEnumerable<string> dinosaurNames);

        /// <summary>
        /// Get a zone by its code
        /// </summary>
        /// <param name="zoneCode">The zone code</param>
        /// <returns>The zone entity</returns>
        ZoneEntity GetZone(string zoneCode);

        /// <summary>
        /// Save a dinosaur to a specific zone
        /// </summary>
        /// <param name="zoneCode">The zone code</param>
        /// <param name="dinosaur">The dinosaur entity</param>
        void SaveDinosaur(string zoneCode, DinosaurEntity dinosaur);

        /// <summary>
        /// Get all dinosaurs in a specific zone
        /// </summary>
        /// <param name="zoneCode">The zone code</param>
        /// <returns>List of dinosaur entities</returns>
        List<DinosaurEntity> GetDinosaurs(string zoneCode);

        /// <summary>
        /// Get a dinosaur by its name
        /// </summary>
        /// <param name="dinoCode">The dinosaur code name</param>
        /// <returns>The dinosaur entity</returns>
        DinosaurEntity GetDinosaurByName(string dinoCode);

        /// <summary>
        /// Get all zones in the database
        /// </summary>
        /// <returns>Collection of key-value pairs of zone name and zone entity</returns>
        IEnumerable<KeyValuePair<string, ZoneEntity>> GetAllZones();

        /// <summary>
        /// Get the count of zones in the database
        /// </summary>
        /// <returns>The number of zones</returns>
        int GetZoneCount();

        /// <summary>
        /// Get a zone at a specific index
        /// </summary>
        /// <param name="index">The index of the zone</param>
        /// <returns>Key-value pair of zone name and zone entity</returns>
        KeyValuePair<string, ZoneEntity> GetZoneAtIndex(int index);

        /// <summary>
        /// Toggle the open/closed status of a zone
        /// </summary>
        /// <param name="zoneName">The name of the zone</param>
        void ToggleZoneStatus(string zoneName);
    }
}