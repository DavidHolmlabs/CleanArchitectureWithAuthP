// Copyright (c) 2023 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System.Runtime.CompilerServices;
using CleanArchitecture.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

[assembly: InternalsVisibleTo("Infrastructure.IntegrationTests")]
namespace CleanArchitecture.Infrastructure
{
    internal static class AspNetApplicationUserExtension
    {
        /// <summary>
        /// This will add a user with the given email if they don't all ready exist
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static async Task<ApplicationUser> CheckAddNewApplicationUserAsync(this UserManager<ApplicationUser> userManager,
            string email, string password)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
                return user;
            user = new ApplicationUser { UserName = email, Email = email };
            var result = await userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                var errorDescriptions = string.Join("\n", result.Errors.Select(x => x.Description));
                throw new InvalidOperationException(
                    $"Tried to add user {email}, but failed. Errors:\n {errorDescriptions}");
            }

            return user;
        }
    }
}
