namespace JurassicCode;

public class Zone
{
    public string Name { get; set; }
    public bool IsOpen { get; set; }
    public List<Dinosaur> Dinosaurs { get; set; } = new List<Dinosaur>();
    public string Status { get { return IsOpen ? "Open" : "Closed"; } }
}