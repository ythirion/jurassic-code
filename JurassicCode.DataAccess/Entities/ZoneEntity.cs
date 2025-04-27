using System.Collections.Generic;

namespace JurassicCode.DataAccess.Entities
{
    public class ZoneEntity
    {
        public string ZoneCode { get; set; }
        public bool AccessStatus { get; set; }
        public List<string> DinosaurCodes { get; set; } = new List<string>();
    }
}