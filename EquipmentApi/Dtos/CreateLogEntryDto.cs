namespace EquipmentApi.Dtos;

public class CreateLogEntryDto
{
    public string Message { get; set; } = string.Empty;
    public string Level { get; set; } = "Info";
}


