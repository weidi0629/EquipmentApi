namespace EquipmentApi.Models;

public class LogEntry
{
    public int Id { get; set; }
    public string Message { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    // week2 -2 logging
    public int EquipmentId { get; set; }
    public Equipment? Equipment { get; set; }

    public string Level { get; set; } = "Info";

}

