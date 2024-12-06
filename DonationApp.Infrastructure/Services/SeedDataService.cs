using DonationApp.Core.Entities;
using DonationApp.Core.Enums;
using DonationApp.Core.Interfaces;
using DonationApp.Infrastructure.DataContext;
using DonationApp.UseCase.Models;
using DonationApp.UseCase.UseCases;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DonationApp.Infrastructure.Services
{
    public class SeedDataService : ISeedDataService
    {
        private readonly ApplicationContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICampaignService _campaignService;

        public Queue<string> Messages { get; set; } = new Queue<string>();

        public SeedDataService(ApplicationContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, ICampaignService campaignService)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _campaignService = campaignService;
        }

        public async Task SeedDataAsync()
        {
            try
            {
                await SeedRoles();
                await SeedUsers();
                await SeedUserAccounts();
                await SeedCampaigns();

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


            var donor1 = new ApplicationUser
            {
                UserName = "hcmus",
                FullName = "Trường Đại Học Khoa Học Tự Nhiên TP.HCM",
                Email = "system@gmail.com",
                PhoneNumber = "0123456789",
                CreatedAt = DateTime.UtcNow,
            };

            var result4 = await _userManager.CreateAsync(donor1, "Abc@123");

            if (result4.Succeeded)
            {
                await _userManager.AddToRoleAsync(donor1, UserRoleEnum.Donor.ToString());
                Messages.Enqueue($"Seed data User {donor1.UserName} thành công");
            }
            else
            {
                result4.Errors.ToList().ForEach(error => Messages.Enqueue(error.Description));
                Messages.Enqueue("Seed data User thất bại");
            }

            var donee1 = new ApplicationUser
            {
                UserName = "donee1",
                FullName = "Nguyễn Văn A",
                Email = "system@gmail.com",
                PhoneNumber = "0123456789",
                CreatedAt = DateTime.UtcNow,
            };

            var result5 = await _userManager.CreateAsync(donee1, "Abc@123");

            if (result5.Succeeded)
            {
                await _userManager.AddToRoleAsync(donee1, UserRoleEnum.Donor.ToString());
                Messages.Enqueue($"Seed data User {donee1.UserName} thành công");
            }
            else
            {
                result5.Errors.ToList().ForEach(error => Messages.Enqueue(error.Description));
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

            if (!await _roleManager.RoleExistsAsync(UserRoleEnum.Donee.ToString()))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoleEnum.Donee.ToString()));
                Messages.Enqueue($"Seed data Role {UserRoleEnum.Donee} thành công");
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
                    AccountNumber = "111111",
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

            var hcmus = await _userManager.FindByNameAsync("hcmus");

            if (hcmus == null)
            {
                Messages.Enqueue("Không tìm thấy User");
                return;
            }
            else
            {
                var account = new UserAccount
                {
                    UserId = hcmus.Id,
                    AccountNumber = "888888",
                    Balance = 2000000000,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = hcmus.Id,
                    MinimumRequiredAmount = double.MinValue,
                };

                await _context.UserAccounts.AddAsync(account);
                await _context.SaveChangesAsync();

                Messages.Enqueue("Seed data UserAccount thành công");
            }
        }

        private async Task SeedCampaigns()
        {
            var userId = await _userManager.GetUserIdAsync(await _userManager.FindByNameAsync("charity"));

            if (userId == null)
            {
                Messages.Enqueue("Không tìm thấy User");
                return;
            }

            var campaign = new CampaignModel
            {
                Name = "Chung tay vượt qua cơn bão",
                Description = "Chung tay vượt qua cơn bão",
                Location = "Hải Phòng",
                StartDate = new DateOnly(2021, 1, 1),
                EndDate = new DateOnly(2021, 12, 31),
                OrganizationId = userId
            };

            var result = await _campaignService.CreateCampaignAsync(campaign);
            if (result.IsSuccess)
            {
                Messages.Enqueue("Seed data Campaign thành công");
            }
            else
            {
                Messages.Enqueue("Seed data Campaign thất bại");
            }

            var campaign1 = new CampaignModel
            {
                Name = "Chung sức vì miền Trung",
                Description = "Chung sức vì miền Trung",
                Location = "Miền Trung",
                StartDate = new DateOnly(2021, 1, 1),
                EndDate = new DateOnly(2021, 12, 31),
                OrganizationId = userId
            };

            var result1 = await _campaignService.CreateCampaignAsync(campaign1);
            if (result1.IsSuccess)
            {
                Messages.Enqueue("Seed data Campaign thành công");
            }
            else
            {
                Messages.Enqueue("Seed data Campaign thất bại");
            }


            var campaign2 = new CampaignModel
            {
                Name = "Xây dựng lại quê hương",
                Description = "Xây dựng lại quê hương",
                Location = "Miền Trung",
                StartDate = new DateOnly(2021, 1, 1),
                EndDate = new DateOnly(2021, 12, 31),
                OrganizationId = userId
            };

            var result2 = await _campaignService.CreateCampaignAsync(campaign2);
            if (result2.IsSuccess)
            {
                Messages.Enqueue("Seed data Campaign thành công");
            }
            else
            {
                Messages.Enqueue("Seed data Campaign thất bại");
            }

            var campaign3 = new CampaignModel
            {
                Name = "Cả nước chung tay vì đồng bào",
                Description = "Cả nước chung tay vì đồng bào",
                Location = "Miền Trung",
                StartDate = new DateOnly(2021, 1, 1),
                EndDate = new DateOnly(2021, 12, 31),
                OrganizationId = userId
            };

            var result3 = await _campaignService.CreateCampaignAsync(campaign3);
            if (result3.IsSuccess)
            {
                Messages.Enqueue("Seed data Campaign thành công");
            }
            else
            {
                Messages.Enqueue("Seed data Campaign thất bại");
            }

            var campaign4 = new CampaignModel
            {
                Name = "Doanh nghiệp đồng hành cùng cộng đồng",
                Description = "Doanh nghiệp đồng hành cùng cộng đồng",
                Location = "Miền Trung",
                StartDate = new DateOnly(2021, 1, 1),
                EndDate = new DateOnly(2021, 12, 31),
                OrganizationId = userId
            };

            var result4 = await _campaignService.CreateCampaignAsync(campaign4);
            if (result4.IsSuccess)
            {
                Messages.Enqueue("Seed data Campaign thành công");
            }
            else
            {
                Messages.Enqueue("Seed data Campaign thất bại");
            }

            var campaign5 = new CampaignModel
            {
                Name = "Mỗi người một tấm lòng",
                Description = "Mỗi người một tấm lòng",
                Location = "Miền Trung",
                StartDate = new DateOnly(2021, 1, 1),
                EndDate = new DateOnly(2021, 12, 31),
                OrganizationId = userId
            };

            var result5 = await _campaignService.CreateCampaignAsync(campaign5);
            if (result5.IsSuccess)
            {
                Messages.Enqueue("Seed data Campaign thành công");
            }
            else
            {
                Messages.Enqueue("Seed data Campaign thất bại");
            }
        }
    }
}
