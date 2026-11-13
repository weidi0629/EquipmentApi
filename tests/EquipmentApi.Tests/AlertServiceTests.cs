using EquipmentApi.Data;
using EquipmentApi.Models;
using EquipmentApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EquipmentApi.Tests;

public class AlertServiceTests
{
    private static AppDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    private static AlertService CreateService(AppDbContext context)
    {
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["AlertThreshold"] = "5"
            })
            .Build();
        return new AlertService(context, config);
    }

    private static async Task SeedErrorsAsync(AppDbContext context, int count, int hoursAgo = 0)
    {
        for (var i = 0; i < count; i++)
        {
            context.LogEntries.Add(new LogEntry
            {
                Message = $"Error {i}",
                Level = "Error",
                CreatedAt = DateTime.UtcNow.AddHours(-hoursAgo),
                EquipmentId = 1
            });
        }
        await context.SaveChangesAsync();
    }

    [Fact]
    public async Task SixErrorsIn24h_ShouldAlert()
    {
        // Arrange
        using var context = CreateContext();
        await SeedErrorsAsync(context, 6);
        var service = CreateService(context);

        // Act
        var result = await service.ShouldAlertAsync(1);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task FourErrorsIn24h_ShouldNotAlert()
    {
        // Arrange
        using var context = CreateContext();
        await SeedErrorsAsync(context, 4);
        var service = CreateService(context);

        // Act
        var result = await service.ShouldAlertAsync(1);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task ZeroErrors_ShouldNotAlert()
    {
        // Arrange
        using var context = CreateContext();
        var service = CreateService(context);

        // Act
        var result = await service.ShouldAlertAsync(1);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task OldError_ShouldNotAlert()
    {
        // Arrange
        using var context = CreateContext();
        await SeedErrorsAsync(context, 1, hoursAgo: 25);
        var service = CreateService(context);

        // Act
        var count = await service.CountErrorsLast24hAsync(1);
        var result = await service.ShouldAlertAsync(1);

        // Assert
        Assert.Equal(0, count);
        Assert.False(result);
    }
}
