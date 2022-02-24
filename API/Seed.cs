using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Identity;

namespace API
{
    public class Seed
    {
        public static async Task SeedData(DataContext context, UserManager<AppUser> userManager,RoleManager<AppRole> roleManager)
        {

            
            var roles = new List<AppRole>
            {
                new AppRole{Name = "Admin"},
                new AppRole{Name = "Teacher"},
                new AppRole{Name = "Student"},
            };
            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }



            
        if (!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser{DisplayName = "Bob", UserName = "bob", Email = "bob@test.com"},
                    new AppUser{DisplayName = "Tom", UserName = "tom", Email = "tom@test.com"},
                    new AppUser{DisplayName = "Jane", UserName = "jane", Email = "jane@test.com"},
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "poipoi");
                    await userManager.AddToRoleAsync(user, "Student");
                }
            }


            var teacher = new AppUser
            {
                DisplayName = "teacher",
                UserName = "teacher",
                Email = "teacher@test.com"
            };

            await userManager.CreateAsync(teacher, "poipoi");
            await userManager.AddToRolesAsync(teacher, new[] {"Teacher"});


            var admin = new AppUser
            {
                DisplayName = "admin",
                UserName = "admin",
                Email = "admin@admin.com"
            };

            await userManager.CreateAsync(admin, "poipoi");
            await userManager.AddToRolesAsync(admin, new[] {"Admin"});

            
        }    
    }
}