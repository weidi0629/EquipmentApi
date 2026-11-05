namespace EquipmentApi.Models;

//public record Equipment(int Id, string Name, string Status);

// ex 4, nullable
public record Equipment(
    int Id,
    string Name,
    string? Status,
    string? LastError
);
