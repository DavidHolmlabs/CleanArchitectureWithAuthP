using AuthPermissions.AdminCode;
using AuthPermissions.BaseCode.DataLayer.Classes;

namespace Microsoft.Extensions.DependencyInjection;

public class SiTenantChangeService : ITenantChangeService
{
    //Returns null if all OK
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task<string> CreateNewTenantAsync(Tenant tenant)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
#nullable disable
        return null;
#nullable restore
    }

    public Task<string> HierarchicalTenantDeleteAsync(List<Tenant> tenantsInOrder)
    {
        throw new NotImplementedException();
    }

    public Task<string> HierarchicalTenantUpdateNameAsync(List<Tenant> tenantsToUpdate)
    {
        throw new NotImplementedException();
    }

    public Task<string> MoveHierarchicalTenantDataAsync(List<(string oldDataKey, Tenant tenantToMove)> tenantToUpdate)
    {
        throw new NotImplementedException();
    }

    public Task<string> MoveToDifferentDatabaseAsync(string oldDatabaseInfoName, string oldDataKey, Tenant updatedTenant)
    {
        throw new NotImplementedException();
    }

    public Task<string> SingleTenantDeleteAsync(Tenant tenant)
    {
        throw new NotImplementedException();
    }

    public Task<string> SingleTenantUpdateNameAsync(Tenant tenant)
    {
        throw new NotImplementedException();
    }
}
