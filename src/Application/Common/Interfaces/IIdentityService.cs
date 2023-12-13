using CleanArchitecture.Application.Common.Models;

namespace CleanArchitecture.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<bool> HasPermissionAsync<TEnumPermissions>(string userId, TEnumPermissions permissionToCheck) where TEnumPermissions : Enum;

    Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

    Task<Result> DeleteUserAsync(string userId);
}
