using EquipmentApi.Models;

namespace EquipmentApi.Services;

public interface IEquipmentService
{
    IEnumerable<Equipment> GetAll();
    
    // ex4, add one fucntion list
    Equipment? GetById(int id);
}
