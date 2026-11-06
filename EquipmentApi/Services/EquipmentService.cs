using EquipmentApi.Models;
using EquipmentApi.Dtos;

namespace EquipmentApi.Services;

public class EquipmentService : IEquipmentService
{
    // ex 4
    private readonly IConfiguration _configuration;

    private readonly List<Equipment> _equipments = new()
    {
        new Equipment(1, "Line1-Press", "Normal", null),
        new Equipment(2, "Line2-Motor", "Fault", "Motor overheating"),
    };

    private int _nextId = 3;

    public EquipmentService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public IEnumerable<Equipment> GetAll()
    {
        // ex4
        var threshold = _configuration.GetValue<int>("AlertThreshold", 5);

        /*return new[]
        {
            new Equipment(1, "Line1-Press", "Normal", null),
            new Equipment(2, "Line2-Motor", "Fault", "Motor overheating"),      
        };*/

        return _equipments;
        
    }

    public Equipment? GetById(int id)
    {
        //return GetAll().FirstOrDefault(e => e.Id == id);
        return _equipments.FirstOrDefault(e => e.Id == id);
    }

    public Equipment Create(CreateEquipmentDto dto)
    {
        var equipment = new Equipment(_nextId++, dto.Name, dto.Status, null);
        _equipments.Add(equipment);
        return equipment;
    }

    public Equipment? Update(int id, CreateEquipmentDto dto)
    {
        var existing = GetById(id);
        if (existing == null) return null;

        var updated = existing with { Name = dto.Name, Status = dto.Status };
        var index = _equipments.FindIndex(e => e.Id == id);
        _equipments[index] = updated;
        return updated;
    }

    public bool Delete(int id)
    {
        var existing = GetById(id);
        if (existing == null) return false;

        _equipments.Remove(existing);
        return true;
    }
}
