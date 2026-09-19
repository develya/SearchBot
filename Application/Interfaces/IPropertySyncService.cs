namespace Application.Interfaces;

public interface IPropertySyncService
{
    Task SyncPropertiesAsync(CancellationToken cancellationToken);
}
