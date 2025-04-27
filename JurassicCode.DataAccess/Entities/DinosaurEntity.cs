using System;

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
}