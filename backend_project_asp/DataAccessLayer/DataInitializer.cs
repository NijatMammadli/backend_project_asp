using backend_project_asp.Data;
using backend_project_asp.Models;
using FrontToBack_hw.DataAccessLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_project_asp.DataAccessLayer
{
    public class DataInitializer
    {
        private readonly AppDbContext _dbContext;



        private readonly UserManager<User> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public DataInitializer(AppDbContext dbContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager; 
           
        }

        public async Task SeedDataAsync()
        {
           await _dbContext.Database.MigrateAsync();

            var roles = new List<string> { RoleConstants.Admin, RoleConstants.CourseModerator, RoleConstants.UserRole };

            foreach (var item in roles)
            {
                if (await _roleManager.RoleExistsAsync(item))
                    continue;

                await _roleManager.CreateAsync(new IdentityRole(item)); 

            }

            var user = new User
            {
                Email = "admin@code.az",
                UserName = "Admin",
                Fullname = "Admin Admin"
            };

            if(await _userManager.FindByEmailAsync(user.Email) ==null)
            {

                await _userManager.CreateAsync(user, "Admin@123");
                await _userManager.AddToRoleAsync(user, RoleConstants.Admin); 

            }
                    
            
            
        }
    }
}
