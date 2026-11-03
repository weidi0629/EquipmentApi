using EquipmentApi.Models;

namespace EquipmentApi.Services;

public class EquipmentService : IEquipmentService
{
    public IEnumerable<Equipment> GetAll() => new[]
    {
        new Equipment(1, "Line1-Press", "Normal-Grace"),
        new Equipment(2, "Line2-Motor", "Fault-Weidi"),
    };
}
