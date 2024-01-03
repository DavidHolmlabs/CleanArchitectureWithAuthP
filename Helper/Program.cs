
using CleanArchitecture.Infrastructure.Data;

string tot = AppAuthSetupData.UsersRolesDefinition.Select(x => x.Email).Aggregate((tot, part) => $"{tot}, {part}");

//Use this string in "DemoUsers" in appsettings.json
Console.WriteLine(tot);
