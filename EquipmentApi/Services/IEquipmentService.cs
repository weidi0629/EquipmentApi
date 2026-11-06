using EquipmentApi.Models;
using EquipmentApi.Dtos;


namespace EquipmentApi.Services;

public interface IEquipmentService
{
    IEnumerable<Equipment> GetAll();
    
    // ex4, add one fucntion list
    Equipment? GetById(int id);

    Equipment Create(CreateEquipmentDto dto);

    Equipment? Update(int id, CreateEquipmentDto dto);

    bool Delete(int id);
}
