namespace JurassicCode.Requests;

public class MoveDinosaurRequest
{
    public string FromZoneName { get; set; }
    public string ToZoneName { get; set; }
    public string DinosaurName { get; set; }
}