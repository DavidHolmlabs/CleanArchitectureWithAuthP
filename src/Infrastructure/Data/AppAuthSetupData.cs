// Copyright (c) 2021 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using AuthPermissions.BaseCode.SetupCode;

namespace CleanArchitecture.Infrastructure.Data
{
    public static class AppAuthSetupData
    {
        public static readonly List<BulkLoadRolesDto> RolesDefinition = new()
        {
            new("Staff", "Staff Role", "GetTodos"),
            new("Manager",  "Manager Role", "CreateTodoList, GetTodos, UpdateTodoList, DeleteTodoList, PurgeTodoLists"),
            new("Administrator",  "Administrator Role", "AccessAll"),
            new("SuperAdmin", "Super admin - only use for setup", "AccessAll"),
        };

        public static readonly List<BulkLoadUserWithRolesTenant> UsersWithRolesDefinition = new()
        {
            new ("Staff@g1.com", null, "Staff"),
            new ("Manager@g1.com", null, "Manager"),
            new ("Administrator@g1.com", null, "Administrator"),
            new ( "Super@g1.com", null, "SuperAdmin"),
        };
    }
}
