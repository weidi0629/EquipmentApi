using EquipmentApi.Models;

namespace EquipmentApi.Services;

public interface IEquipmentService
{
    IEnumerable<Equipment> GetAll();
}
