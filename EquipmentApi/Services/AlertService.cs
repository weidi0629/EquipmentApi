using EquipmentApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EquipmentApi.Services;

public class AlertService : IAlertService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;

    public AlertService(AppDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    public async Task<bool> ShouldAlertAsync(int equipmentId)
    {
        var threshold = _config.GetValue<int>("AlertThreshold");
        var count = await CountErrorsLast24hAsync(equipmentId);
        return count > threshold;
    }

    public async Task<int> CountErrorsLast24hAsync(int equipmentId)
    {
        var since = DateTime.UtcNow.AddHours(-24);
        return await _context.LogEntries
            .CountAsync(l => l.EquipmentId == equipmentId
                && l.Level == "Error"
                && l.CreatedAt >= since);
    }
}
