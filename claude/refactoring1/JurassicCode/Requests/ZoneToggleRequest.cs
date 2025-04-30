using System.Collections.Generic;

namespace JurassicCode.Requests;

public class ZoneToggleRequest
{
    public List<string> ZoneNames { get; set; } = new List<string>();
}