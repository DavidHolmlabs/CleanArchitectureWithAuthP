﻿using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Domain.Constants;

public enum Permissions : ushort //Must be ushort to work with AuthP
{
    NotSet = 0, //error condition

    [Display(GroupName = "TodoLists", Name = "Create", Description = "Can Create Todo List")]
    CreateTodoList = 10,

    [Display(GroupName = "TodoLists", Name = "GetTodos", Description = "Can view Todo List")]
    GetTodos = 11,

    [Display(GroupName = "TodoLists", Name = "Update", Description = "Can Update Todo List")]
    UpdateTodoList = 12,

    [Display(GroupName = "TodoLists", Name = "Update", Description = "Can Update Todo List")]
    DeleteTodoList = 13,

    [Display(GroupName = "TodoLists", Name = "Purge", Description = "Can Purge Todo List")]
    PurgeTodoLists = 14,

    //Used by tenant-level admin user
    [Obsolete]
    [Display(GroupName = "Employees", Description = "Can read tenant employees")]
    EmployeeRead = 930,
    [Obsolete]
    [Display(GroupName = "Employees", Description = "Can revoke or activate a tenant employee")]
    EmployeeRevokeActivate = 931,

    [Display(GroupName = "Employees", Description = "Can invite new users to join the tenant")]
    InviteUsers = 932,

    //40_000 - User admin
    [Display(GroupName = "UserAdmin", Name = "Read users", Description = "Can list User")]
    UserRead = 40_000,
    [Display(GroupName = "UserAdmin", Name = "Sync users", Description = "Syncs authorization provider with AuthUsers")]
    UserSync = 40_001,
    [Display(GroupName = "UserAdmin", Name = "Alter users", Description = "Can access the user update")]
    UserChange = 40_002,
    [Display(GroupName = "UserAdmin", Name = "Alter user's roles", Description = "Can add/remove roles from a user")]
    UserRolesChange = 40_003,
    [Display(GroupName = "UserAdmin", Name = "Move a user to another tenant", Description = "Can control what tenant they are in")]
    UserChangeTenant = 40_004,
    [Display(GroupName = "UserAdmin", Name = "Remove user", Description = "Can delete the user")]
    UserRemove = 40_005,

    //41_000 - Roles admin
    [Display(GroupName = "RolesAdmin", Name = "Read Roles", Description = "Can list Role")]
    RoleRead = 41_000,
    //This is an example of grouping multiple actions under one permission
    [Display(GroupName = "RolesAdmin", Name = "Change Role", Description = "Can create, update or delete a Role", AutoGenerateFilter = true)]
    RoleChange = 41_001,

    //41_100 - Permissions 
    [Display(GroupName = "RolesAdmin", Name = "See permissions", Description = "Can display the list of permissions", AutoGenerateFilter = true)]
    PermissionRead = 41_100,
    [Display(GroupName = "RolesAdmin", Name = "See all permissions", Description = "list will included filtered Permission ", AutoGenerateFilter = true)]
    IncludeFilteredPermissions = 41_101,

    //42_000 - tenant admin
    [Display(GroupName = "TenantAdmin", Name = "Read Tenants", Description = "Can list Tenants")]
    TenantList = 42_000,
    [Display(GroupName = "TenantAdmin", Name = "Create new Tenant", Description = "Can create new Tenants", AutoGenerateFilter = true)]
    TenantCreate = 42_001,
    [Display(GroupName = "TenantAdmin", Name = "Alter Tenants info", Description = "Can update Tenant's name", AutoGenerateFilter = true)]
    TenantUpdate = 42_002,
    [Display(GroupName = "TenantAdmin", Name = "Move tenant to another parent", Description = "Can move tenant to different parent (WARNING)", AutoGenerateFilter = true)]
    TenantMove = 42_003,
    [Display(GroupName = "TenantAdmin", Name = "Delete tenant", Description = "Can delete tenant (WARNING)", AutoGenerateFilter = true)]
    TenantDelete = 42_004,
    [Display(GroupName = "TenantAdmin", Name = "Access other tenant data", Description = "Sets DataKey of user to another tenant", AutoGenerateFilter = true)]
    TenantAccessData = 42_005,



    //Here is an example of detailed control over some feature
    [Display(GroupName = "Stock", Name = "Read", Description = "Can read stock")]
    StockRead = 510,
    [Display(GroupName = "Stock", Name = "Add new", Description = "Can add a new stock item")]
    StockAddNew = 513,
    [Display(GroupName = "Stock", Name = "Remove", Description = "Can remove stock")]
    StockRemove = 514,

    [Display(GroupName = "Sales", Name = "Read", Description = "Can read any sales")]
    SalesRead = 520,
    [Display(GroupName = "Sales", Name = "Sell", Description = "Can sell items from stock")]
    SalesSell = 521,
    [Display(GroupName = "Sales", Name = "Return", Description = "Can return an item to stock")]
    SalesReturn = 522,

    //----------------------------------------------------
    //This is an example of what to do with permission you don't used anymore.
    //You don't want its number to be reused as it could cause problems 
    //Just mark it as obsolete and the PermissionDisplay code won't show it
    [Obsolete]
    [Display(GroupName = "Old", Name = "Not used", Description = "example of old permission")]
    OldPermissionNotUsed = 1_000,

    //----------------------------------------------------
    // A enum member with no <see cref="DisplayAttribute"/> can be used, but its not shown in the PermissionDisplay at all
    // Useful if are working on new permissions but you don't want it to be used by anyone yet 
    AnotherPermission = 2_000,

    //----------------------------------------------------
    //Admin section

    //43_000
    [Display(GroupName = "AppStatus", Name = "list active app statues", Description = "Can list active statues", AutoGenerateFilter = true)]
    AppStatusList = 43_000,
    [Display(GroupName = "AppStatus", Name = "Stop all users accessing app", Description = "Stop all users, apart from user who set this", AutoGenerateFilter = true)]
    AppStatusAllDown = 43_002,
    [Display(GroupName = "AppStatus", Name = "Stop users linked to specific tenant", Description = "Stop users linked to specific tenant", AutoGenerateFilter = true)]
    AppStatusTenantDown = 43_003,
    [Display(GroupName = "AppStatus", Name = "Remove an active app statue", Description = "Can turn off any active statue", AutoGenerateFilter = true)]
    AppStatusRemove = 43_005,

    [Display(GroupName = "SuperAdmin", Name = "AccessAll", Description = "This allows the user to access every feature", AutoGenerateFilter = true)]
    AccessAll = ushort.MaxValue,
}
