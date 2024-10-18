using DonationApp.Core.Entities;
using DonationApp.Core.Enums;
using DonationApp.Core.Interfaces;
using DonationApp.Infrastructure.DataContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DonationApp.Infrastructure.Services
{
    public class SeedDataService : ISeedDataService
    {
        private readonly ApplicationContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public Queue<string> Messages { get; set; } = new Queue<string>();

        public SeedDataService(ApplicationContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {

            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task SeedDataAsync()
        {
            try
            {

                await SeedUsers();

            }
            catch
            (Exception ex)
            {
                Messages.Enqueue(ex.Message);
            }
        }


        private async Task SeedUsers()
        {
            var admin = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@gmail.com",
                PhoneNumber = "0123456789",
                CreatedDate = DateTime.UtcNow,
            };

            var result = await _userManager.CreateAsync(admin, "Abc@123");

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(admin, UserRoleEnum.Administrator.ToString());
                Messages.Enqueue($"Seed data User {admin.UserName} thành công");
            }
            else
            {
                result.Errors.ToList().ForEach(error => Messages.Enqueue(error.Description));
                Messages.Enqueue("Seed data User thất bại");
            }



            var donor = new ApplicationUser
            {
                UserName = "donor",
                Email = "donor@gmail.com",
                PhoneNumber = "0123456789",
                CreatedDate = DateTime.UtcNow,
            };

            var result1 = await _userManager.CreateAsync(donor, "Abc@123");

            if (result1.Succeeded)
            {
                await _userManager.AddToRoleAsync(donor, UserRoleEnum.Donor.ToString());
                Messages.Enqueue($"Seed data User {donor.UserName} thành công");
            }
            else
            {
                result1.Errors.ToList().ForEach(error => Messages.Enqueue(error.Description));
                Messages.Enqueue("Seed data User thất bại");
            }


            var charity = new ApplicationUser
            {
                UserName = "seller",
                Email = "seller@gmail.com",
                PhoneNumber = "0123456789",
                CreatedDate = DateTime.UtcNow,
            };

            var result2 = await _userManager.CreateAsync(charity, "Abc@123");

            if (result2.Succeeded)
            {
                await _userManager.AddToRoleAsync(charity, UserRoleEnum.CharitableOrganization.ToString());
                Messages.Enqueue($"Seed data User {charity.UserName} thành công");
            }
            else
            {
                result2.Errors.ToList().ForEach(error => Messages.Enqueue(error.Description));
                Messages.Enqueue("Seed data User thất bại");
            }
        }
    }
}
