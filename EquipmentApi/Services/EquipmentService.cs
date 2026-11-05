using EquipmentApi.Models;

namespace EquipmentApi.Services;

public class EquipmentService : IEquipmentService
{
    // ex 4
    private readonly IConfiguration _configuration;

    public EquipmentService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public IEnumerable<Equipment> GetAll()
    {
        // ex4
        var threshold = _configuration.GetValue<int>("AlertThreshold", 5);

        return new[]
        {
            new Equipment(1, "Line1-Press", "Normal", null),
            new Equipment(2, "Line2-Motor", "Fault", "Motor overheating"),      
        };
        
    }

    public Equipment? GetById(int id)
    {
        return GetAll().FirstOrDefault(e => e.Id == id);
    }
}
