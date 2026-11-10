namespace EquipmentApi.Dtos;

public class LogEntryDto
{
    public int Id { get; set; }
    public string Message { get; set; } = string.Empty;
    public string Level { get; set; } = "Info";
    public DateTime CreatedAt { get; set; }
    public int EquipmentId { get; set; }
}
