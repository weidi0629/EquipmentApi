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

    IEnumerable<LogEntry> GetLogs(int equipmentId, int page, int pageSize);

    int GetErrorLogCount(int equipmentId, int hours);

    LogEntry AddLog(int equipmentId, CreateLogEntryDto dto);


}
