﻿using AuthPermissions;
using AuthPermissions.AspNetCore;
using AuthPermissions.AspNetCore.Services;
using AuthPermissions.AspNetCore.StartupServices;
using AuthPermissions.BaseCode.SetupCode;
using AuthPermissions.BaseCode;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Infrastructure;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Infrastructure.Data.Interceptors;
using CleanArchitecture.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using RunMethodsSequentially;
using CleanArchitecture.Domain.Constants;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration, string webRootPath)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

#if (UseSQLite)
            options.UseSqlite(connectionString);
#else
            options.UseSqlServer(connectionString);
#endif
        });

        services.AddDbContext<TenantDbContext>((sp, options) =>
        {

#if (UseSQLite)
            options.UseSqlite(connectionString);
#else
            options.UseSqlServer(connectionString);
#endif
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<ITenantDbContext>(provider => provider.GetRequiredService<TenantDbContext>());

#if (UseApiOnly)
        services.AddAuthentication()
            .AddBearerToken(IdentityConstants.BearerScheme);

        services.AddAuthorizationBuilder();

        services
            .AddIdentityCore<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddApiEndpoints();
#else
        services
            .AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.RegisterAuthPermissions<Permissions>(options =>
        {
            options.TenantType = TenantTypes.SingleLevel;
            options.LinkToTenantType = LinkToTenantTypes.OnlyAppUsers;
            options.EncryptionKey = configuration[nameof(AuthPermissionsOptions.EncryptionKey)];
            options.PathToFolderToLock = webRootPath;
        })
            .UsingEfCoreSqlServer(connectionString)
            .IndividualAccountsAuthentication<ApplicationUser>()
            .AddRolesPermissionsIfEmpty(AppAuthSetupData.RolesDefinition)
            .AddTenantsIfEmpty(AppAuthSetupData.TenantDefinition)
            .AddAuthUsersIfEmpty(AppAuthSetupData.UsersRolesDefinition)
            .RegisterAuthenticationProviderReader<SyncIndividualAccountApplicationUsers>()
            .RegisterFindUserInfoService<IndividualAccountApplicationUserLookup>()
            .AddSuperUserToIndividualAccounts<ApplicationUser>()
            .SetupAspNetCoreAndDatabase(options =>
            {
                //Migrate individual account database
                options.RegisterServiceToRunInJob<StartupServiceMigrateAnyDbContext<ApplicationDbContext>>();
                //Add demo users to the database
                options.RegisterServiceToRunInJob<StartupServicesIndividualAccountsAddDemoApplicationUsers>();

                options.RegisterServiceToRunInJob<StartupServiceMigrateAnyDbContext<TenantDbContext>>();
            });
#endif

        services.AddSingleton(TimeProvider.System);
        services.AddTransient<IIdentityService, IdentityService>();

        services.AddAuthorization();// TODO: Check: to be able to use [Authorize(Roles = "Administrator")]

        return services;
    }
}
