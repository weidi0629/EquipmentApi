namespace EquipmentApi.Services;

public interface IAlertService
{
    Task<bool> ShouldAlertAsync(int equipmentId);
    Task<int> CountErrorsLast24hAsync(int equipmentId);
}
