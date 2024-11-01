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
                await SeedRoles();
                await SeedUsers();
                await SeedUserAccounts();

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
                FullName = "Lĩnh",
                Email = "admin@gmail.com",
                PhoneNumber = "0123456789",
                CreatedAt = DateTime.UtcNow,
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
                FullName = "Linh",
                Email = "donor@gmail.com",
                PhoneNumber = "0123456789",
                CreatedAt = DateTime.UtcNow,
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
                UserName = "charity",
                FullName = "Quỹ vì tương lai",
                Email = "charity@gmail.com",
                PhoneNumber = "0123456789",
                CreatedAt = DateTime.UtcNow,
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


            var bank = new ApplicationUser
            {
                UserName = "bank",
                FullName = "BankingSystem",
                Email = "system@gmail.com",
                PhoneNumber = "0123456789",
                CreatedAt = DateTime.UtcNow,
            };

            var result3 = await _userManager.CreateAsync(bank, "Abc@123");

            if (result3.Succeeded)
            {
                await _userManager.AddToRoleAsync(bank, UserRoleEnum.Administrator.ToString());
                Messages.Enqueue($"Seed data User {bank.UserName} thành công");
            }
            else
            {
                result3.Errors.ToList().ForEach(error => Messages.Enqueue(error.Description));
                Messages.Enqueue("Seed data User thất bại");
            }
        }

        private async Task SeedRoles()
        {
            if (!await _roleManager.RoleExistsAsync(UserRoleEnum.Administrator.ToString()))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoleEnum.Administrator.ToString()));
                Messages.Enqueue($"Seed data Role {UserRoleEnum.Administrator} thành công");
            }

            if (!await _roleManager.RoleExistsAsync(UserRoleEnum.Donor.ToString()))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoleEnum.Donor.ToString()));
                Messages.Enqueue($"Seed data Role {UserRoleEnum.Donor} thành công");
            }

            if (!await _roleManager.RoleExistsAsync(UserRoleEnum.CharitableOrganization.ToString()))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoleEnum.CharitableOrganization.ToString()));
                Messages.Enqueue($"Seed data Role {UserRoleEnum.CharitableOrganization} thành công");
            }
        }

        private async Task SeedUserAccounts()
        {
            var user = await _userManager.FindByNameAsync("admin");

            if (user == null)
            {
                Messages.Enqueue("Không tìm thấy User");
                return;
            }
            else
            {
                var account = new UserAccount
                {
                    UserId = user.Id,
                    AccountNumber = "123456789",
                    Balance = 1000000,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = user.Id
                };

                await _context.UserAccounts.AddAsync(account);
                await _context.SaveChangesAsync();

                Messages.Enqueue("Seed data UserAccount thành công");
            }

            var bank = await _userManager.FindByNameAsync("bank");

            if (bank == null)
            {
                Messages.Enqueue("Không tìm thấy User");
                return;
            }
            else
            {
                var account = new UserAccount
                {
                    UserId = bank.Id,
                    AccountNumber = "999999999",
                    Balance = 0,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = bank.Id,
                    MinimumRequiredAmount = double.MinValue,
                };

                await _context.UserAccounts.AddAsync(account);
                await _context.SaveChangesAsync();

                Messages.Enqueue("Seed data UserAccount thành công");
            }
        }
    }
}
