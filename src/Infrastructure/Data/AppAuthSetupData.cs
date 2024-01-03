// Copyright (c) 2021 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using AuthPermissions.BaseCode.SetupCode;

namespace CleanArchitecture.Infrastructure.Data
{
    public static class AppAuthSetupData
    {
        public static readonly List<BulkLoadRolesDto> RolesDefinition = new List<BulkLoadRolesDto>()
        {
            new("SuperAdmin", "Super admin - only use for setup", "AccessAll"),
            new("App Admin", "Overall app Admin",
                "UserRead, UserSync, UserChange, UserRolesChange, UserChangeTenant, UserRemove, " +
                "RoleRead, RoleChange, PermissionRead, IncludeFilteredPermissions, " +
                "TenantList, TenantCreate, TenantUpdate, TenantMove, TenantDelete, " +
                "AppStatusList, AppStatusAllDown, AppStatusTenantDown, AppStatusRemove"),
            new("Tenant Admin", "Tenant-level admin", "EmployeeRead, UserRead, UserRolesChange, RoleRead"),
            new("Tenant Director", "Company CEO, can see stock/sales and employees", "EmployeeRead, StockRead, SalesRead"),
            new("Area Manager", "Area manager - check stock and sales", "StockRead, SalesRead"),
            new("Store Manager", "Shop sales manager - full access", "StockRead, StockAddNew, StockRemove, SalesRead, SalesSell, SalesReturn"),
            new("Sales Assistant", "Shop sales Assistant - just sells", "StockRead, SalesSell"),
        };


        public static readonly List<BulkLoadTenantDto> TenantDefinition = new List<BulkLoadTenantDto>()
        {
            new("Malmö Stad", null,
            [
                new ("Utbildningsförvaltningen", null,
                [
                    new ("Limhamn", null,
                    [
                        new ("Linneskolan"),
                        new ("Bergaskolan")
                    ]),
                    new ("Oxie", null,
                    [
                        new ("Stenskolan"),
                    ])
                ]),
                new ("Fritidsförvaltningen", null,
                [
                    new ("Bandy"),
                    new ("Hockey"),
                ])
            ]),
            new("Lund Stad", null,
            [
                new ("Centrum", null,
                [
                    new ("Kattis"),
                    new ("Stenbäcksskolan")
                ]),
            ])
        };

        public static readonly List<BulkLoadUserWithRolesTenant> UsersRolesDefinition =
        [
            new ("Super1@silabs.se", null, "SuperAdmin"),
            new ("AppAdmin1@silabs.se", null, "App Admin"),
            //Malmö Stad
            new ("admin@Malmo1.se", null,
                "Tenant Admin, Area Manager", tenantNameForDataKey: "Malmö Stad"),
            new ("director@Malmo1.se", null,
                "Tenant Director, Area Manager", tenantNameForDataKey: "Malmö Stad"),
            new ("utbildning@Malmo1.se", null,
                "Area Manager", tenantNameForDataKey: "Malmö Stad | Utbildningsförvaltningen"),
            new ("fritid@Malmo1.se", null,
                "Area Manager", tenantNameForDataKey: "Malmö Stad | Fritidsförvaltningen"),
            //Linneskolan
            new ("linne-rektor@Malmo1.se", null,
                "Store Manager", tenantNameForDataKey: "Malmö Stad | Utbildningsförvaltningen | Limhamn | Linneskolan"),
            new ("linne-admin@Malmo1.se", null,
                "Sales Assistant", tenantNameForDataKey: "Malmö Stad | Utbildningsförvaltningen | Limhamn | Linneskolan"),
            //Bergaskolan
            new ("berga-rektor@Malmo1.se", null,
                "Store Manager", tenantNameForDataKey: "Malmö Stad | Utbildningsförvaltningen | Limhamn | Bergaskolan"),
            new ("berga-admin@Malmo1.se", null,
                "Sales Assistant", tenantNameForDataKey: "Malmö Stad | Utbildningsförvaltningen | Limhamn | Bergaskolan"),
            //Stenskolan
            new ("sten-rektor@Malmo1.se", null,
                "Store Manager", tenantNameForDataKey: "Malmö Stad | Utbildningsförvaltningen | Oxie | Stenskolan"),
            new ("sten-admin@Malmo1.se", null,
                "Sales Assistant", tenantNameForDataKey: "Malmö Stad | Utbildningsförvaltningen | Oxie | Stenskolan"),

            //Lund Stad
            new ("admin@Lund1.se", null,
                "Tenant Admin, Area Manager", tenantNameForDataKey: "Lund Stad"),
            new ("director@Lund1.se", null,
                "Tenant Director, Area Manager", tenantNameForDataKey: "Lund Stad"),
            //Kattis
            new ("kattis-rektor@Lund1.se", null,
                "Store Manager", tenantNameForDataKey: "Lund Stad | Centrum | Kattis"),
            new ("kattis-admin@Lund1.se", null,
                "Sales Assistant", tenantNameForDataKey: "Lund Stad | Centrum | Kattis"),
            //Stenbäcksskolan
            new ("sten-rektor@Lund1.se", null,
                "Store Manager", tenantNameForDataKey: "Lund Stad | Centrum | Stenbäcksskolan"),
            new ("sten-admin@Lund1.se", null,
                "Sales Assistant", tenantNameForDataKey: "Lund Stad | Centrum | Stenbäcksskolan"),
        ];
    }
}
