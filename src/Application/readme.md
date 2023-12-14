dotnet new ca-usecase --name CreateTenant --feature-name Tenants --usecase-type command --return-type CreateTenantDto

dotnet new ca-usecase --name CreateTenant --feature-name Tenants --usecase-type command 

dotnet new ca-usecase --name ListTenants --feature-name Tenants --usecase-type query --return-type List<Tenant>

dotnet new ca-usecase --name ListAuthUsers --feature-name AuthUsers --usecase-type query --return-type List<AuthUsers>

dotnet new ca-usecase --name ListRoles --feature-name Roles --usecase-type query --return-type List<Role>
