using System;
using System.Collections.Generic;

namespace JurassicCode.DataAccess.Entities
{
    public class DinosaurEntity
    {
        public string CodeName { get; set; }
        public string Species { get; set; }
        public bool IsCarnivorous { get; set; }
        public bool IsSick { get; set; }
        public DateTime LastFed { get; set; }
    }

    public class ZoneEntity
    {
        public string ZoneCode { get; set; }
        public bool AccessStatus { get; set; }
        public List<string> DinosaurCodes { get; set; } = new List<string>();
    }
}