namespace TestPilot.DTOs;
public class ForwardRequestDto
{
    public string Url { get; set; } = string.Empty; // Initialize with an empty string
    public string Method { get; set; } = string.Empty; // Initialize with an empty string
    public string? Body { get; set; } // Make Body nullable as it's not always required
}
