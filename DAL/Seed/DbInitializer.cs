using System.Linq;
using Microsoft.AspNetCore.Identity;
using Model;
using DAL.Interface;
using Model.DB;
using System.Collections.Generic;

namespace DAL.Seed
{
    public class DbInitializer : IDbInitializer
    {
        private readonly HRUDbContext context;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IUnitOfWork unitOfWork;

        public DbInitializer(
            HRUDbContext context,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IUnitOfWork unitOfWork)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.unitOfWork = unitOfWork;
        }

        public void Initialize()
        {
            context.Database.EnsureCreated();

            if (!context.Roles.Any(r => r.Name == "Administrator"))
            {
                roleManager.CreateAsync(new IdentityRole("Administrator")).Wait();
                string userAdmin = "admin@gmail.com";
                string passwordAdmin = "Admin_123";
                userManager.CreateAsync(new User { UserName = userAdmin, Email = userAdmin, EmailConfirmed = true }, passwordAdmin).Wait();
                var t = userManager.FindByNameAsync(userAdmin);
                userManager.AddToRoleAsync(t.Result, "Administrator").Wait();
            }
            unitOfWork.Save();
        }
    }
}
