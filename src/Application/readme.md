dotnet new ca-usecase --name CreateTenant --feature-name Tenants --usecase-type command --return-type CreateTenantDto
dotnet new ca-usecase --name CreateTenant --feature-name Tenants --usecase-type command 
dotnet new ca-usecase --name ListTenants --feature-name Tenants --usecase-type query --return-type List<Tenant>
dotnet new ca-usecase --name StartAccess --feature-name Tenants --usecase-type command --return-type string
dotnet new ca-usecase --name StopAccess --feature-name Tenants --usecase-type command --return-type string

dotnet new ca-usecase --name ListAuthUsers --feature-name AuthUsers --usecase-type query --return-type List<AuthUsers>
dotnet new ca-usecase --name NavMenu --feature-name AuthUsers --usecase-type query --return-type NavMenu
dotnet new ca-usecase --name InviteAuthUsers --feature-name AuthUsers --usecase-type command --return-type List<InviteDto>
dotnet new ca-usecase --name AcceptInvite --feature-name AuthUsers --usecase-type command 

dotnet new ca-usecase --name ListRoles --feature-name Roles --usecase-type query --return-type List<Role>

## Products
dotnet new ca-usecase --name ListProducts --feature-name Products --usecase-type query --return-type List<Product>
dotnet new ca-usecase --name CreateProduct --feature-name Products --usecase-type command --return-type Product
dotnet new ca-usecase --name UpdateProduct --feature-name Products --usecase-type command --return-type Product

## Orders
dotnet new ca-usecase --name ListOrders --feature-name Orders --usecase-type query --return-type List<Order>
dotnet new ca-usecase --name CreateOrder --feature-name Orders --usecase-type command --return-type Order
dotnet new ca-usecase --name AvailableOrders --feature-name Orders --usecase-type query --return-type List<OrderDto>

