using EquipmentApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EquipmentApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<LogEntry> LogEntries { get; set; }
}
