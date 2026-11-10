using EquipmentApi.Models;
using EquipmentApi.Dtos;
using EquipmentApi.Data;
using Microsoft.EntityFrameworkCore;


namespace EquipmentApi.Services;

public class EquipmentService : IEquipmentService
{
    /*
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

        return new[]
        {
            new Equipment(1, "Line1-Press", "Normal", null),
            new Equipment(2, "Line2-Motor", "Fault", "Motor overheating"),      
        };

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
    }*/  
    // week 1 version 



    private readonly AppDbContext _context;
    private readonly ILogger<EquipmentService> _logger;


    public EquipmentService(AppDbContext context, ILogger<EquipmentService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public IEnumerable<Equipment> GetAll()
    {
        return _context.Equipment.ToList();
    }

    public Equipment? GetById(int id)
    {
        return _context.Equipment.FirstOrDefault(e => e.Id == id);
    }

    public Equipment Create(CreateEquipmentDto dto)
    {
        var equipment = new Equipment(0, dto.Name, dto.Status, null);
        _context.Equipment.Add(equipment);
        _context.SaveChanges();
        return equipment;
    }

    public Equipment? Update(int id, CreateEquipmentDto dto)
    {
        var existing = GetById(id);
        if (existing == null) return null;

        var updated = existing with { Name = dto.Name, Status = dto.Status };
        _context.Entry(existing).CurrentValues.SetValues(updated);
        _context.SaveChanges();
        return updated;
    }

    public bool Delete(int id)
    {
        var existing = GetById(id);
        if (existing == null) return false;

        _context.Equipment.Remove(existing);
        _context.SaveChanges();
        return true;
    }

    public IEnumerable<LogEntry> GetLogs(int equipmentId, int page, int pageSize)
    {
            var query = _context.LogEntries
            .Where(l => l.EquipmentId == equipmentId)
            .OrderByDescending(l => l.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

        _logger.LogInformation("SQL: {Sql}", query.ToQueryString());

        return query.ToList();
    }

    public int GetErrorLogCount(int equipmentId, int hours)
    {
        var since = DateTime.UtcNow.AddHours(-hours);
        return _context.LogEntries
            .Count(l => l.EquipmentId == equipmentId
                && l.Level == "Error"
                && l.CreatedAt >= since);
    }

    public LogEntry AddLog(int equipmentId, CreateLogEntryDto dto)
    {
        var log = new LogEntry
        {
            Message = dto.Message,
            Level = dto.Level,
            CreatedAt = DateTime.UtcNow,
            EquipmentId = equipmentId
        };
        _context.LogEntries.Add(log);
        _context.SaveChanges();
        return log;
    }

}
