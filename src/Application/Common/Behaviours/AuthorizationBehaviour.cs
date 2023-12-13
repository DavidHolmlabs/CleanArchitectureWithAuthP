using System.Reflection;
using AuthPermissions.AspNetCore;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Constants;

namespace CleanArchitecture.Application.Common.Behaviours;

public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly IUser _user;
    private readonly IIdentityService _identityService;

    public AuthorizationBehaviour(
        IUser user,
        IIdentityService identityService)
    {
        _user = user;
        _identityService = identityService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var authorizeAttributes = request.GetType().GetCustomAttributes<Microsoft.AspNetCore.Authorization.AuthorizeAttribute>();

        if (authorizeAttributes.Any())
        {
            // Must be authenticated user
            if (_user.Id == null)
            {
                throw new UnauthorizedAccessException();
            }

            // Role-based authorization
            var authorizeAttributesWithRoles = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Roles));

            if (authorizeAttributesWithRoles.Any())
            {
                var authorized = false;

                foreach (var roles in authorizeAttributesWithRoles.Select(a => a.Roles?.Split(',') ?? []))
                {
                    foreach (var role in roles)
                    {
                        var isInRole = await _identityService.IsInRoleAsync(_user.Id, role.Trim());
                        if (isInRole)
                        {
                            authorized = true;
                            break;
                        }
                    }
                }

                // Must be a member of at least one role in roles
                if (!authorized)
                {
                    throw new ForbiddenAccessException();
                }
            }

            // Policy-based authorization
            var authorizeAttributesWithPolicies = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Policy));

            if (authorizeAttributesWithPolicies.Any())
            {
                foreach (var policy in authorizeAttributesWithPolicies.Select(a => a.Policy))
                {
                    var authorized = await _identityService.AuthorizeAsync(_user.Id, policy ?? "");

                    if (!authorized)
                    {
                        throw new ForbiddenAccessException();
                    }
                }
            }

            var authorizeAttributesWithPermissions = authorizeAttributes.Where(a => a is HasPermissionAttribute);

            if (authorizeAttributesWithPermissions.Any())
            {
                foreach (var hasPermissionAttribute in authorizeAttributesWithPermissions)
                {
                    bool success = Enum.TryParse(hasPermissionAttribute.Policy ?? Permissions.NotSet.ToString(), out Permissions cleanPermissions);

                    if (!success)
                    {
                        throw new ArgumentException(hasPermissionAttribute.Policy);
                    }

                    bool hasPermission = await _identityService.HasPermissionAsync(_user.Id, cleanPermissions);

                    if (!hasPermission)
                    {
                        throw new ForbiddenAccessException();
                    }
                }
            }
        }

        // User is authorized / authorization not required
        return await next();
    }
}
