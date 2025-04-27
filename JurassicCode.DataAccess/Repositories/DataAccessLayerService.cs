using System;
using System.Collections.Generic;
using JurassicCode.DataAccess.Entities;
using JurassicCode.DataAccess.Interfaces;

namespace JurassicCode.DataAccess.Repositories
{
    /// <summary>
    /// Implementation of IDataAccessLayer that wraps the static DataAccessLayer
    /// </summary>
    public class DataAccessLayerService : IDataAccessLayer
    {
        /// <summary>
        /// Initialize the database with a specific instance
        /// </summary>
        /// <param name="db">The database instance</param>
        public void Init(Database db)
        {
            DataAccessLayer.Init(db);
        }

        /// <summary>
        /// Initialize the database with a new instance
        /// </summary>
        public void Init()
        {
            DataAccessLayer.Init();
        }

        /// <summary>
        /// Save a zone to the database
        /// </summary>
        /// <param name="zoneName">Name of the zone</param>
        /// <param name="isOpen">Whether the zone is open</param>
        /// <param name="dinosaurNames">Names of dinosaurs in the zone</param>
        public void SaveZone(string zoneName, bool isOpen, IEnumerable<string> dinosaurNames)
        {
            DataAccessLayer.SaveZone(zoneName, isOpen, dinosaurNames);
        }

        /// <summary>
        /// Get a zone by its code
        /// </summary>
        /// <param name="zoneCode">The zone code</param>
        /// <returns>The zone entity</returns>
        public ZoneEntity GetZone(string zoneCode)
        {
            return DataAccessLayer.GetZone(zoneCode);
        }

        /// <summary>
        /// Save a dinosaur to a specific zone
        /// </summary>
        /// <param name="zoneCode">The zone code</param>
        /// <param name="dinosaur">The dinosaur entity</param>
        public void SaveDinosaur(string zoneCode, DinosaurEntity dinosaur)
        {
            DataAccessLayer.SaveDinosaur(zoneCode, dinosaur);
        }

        /// <summary>
        /// Get all dinosaurs in a specific zone
        /// </summary>
        /// <param name="zoneCode">The zone code</param>
        /// <returns>List of dinosaur entities</returns>
        public List<DinosaurEntity> GetDinosaurs(string zoneCode)
        {
            return DataAccessLayer.GetDinosaurs(zoneCode);
        }

        /// <summary>
        /// Get a dinosaur by its name
        /// </summary>
        /// <param name="dinoCode">The dinosaur code name</param>
        /// <returns>The dinosaur entity</returns>
        public DinosaurEntity GetDinosaurByName(string dinoCode)
        {
            return DataAccessLayer.GetDinosaurByName(dinoCode);
        }

        /// <summary>
        /// Get all zones in the database
        /// </summary>
        /// <returns>Collection of key-value pairs of zone name and zone entity</returns>
        public IEnumerable<KeyValuePair<string, ZoneEntity>> GetAllZones()
        {
            return DataAccessLayer.GetAllZones();
        }

        /// <summary>
        /// Get the count of zones in the database
        /// </summary>
        /// <returns>The number of zones</returns>
        public int GetZoneCount()
        {
            return DataAccessLayer.GetZoneCount();
        }

        /// <summary>
        /// Get a zone at a specific index
        /// </summary>
        /// <param name="index">The index of the zone</param>
        /// <returns>Key-value pair of zone name and zone entity</returns>
        public KeyValuePair<string, ZoneEntity> GetZoneAtIndex(int index)
        {
            return DataAccessLayer.GetZoneAtIndex(index);
        }

        /// <summary>
        /// Toggle the open/closed status of a zone
        /// </summary>
        /// <param name="zoneName">The name of the zone</param>
        public void ToggleZoneStatus(string zoneName)
        {
            DataAccessLayer.ToggleZoneStatus(zoneName);
        }
    }
}