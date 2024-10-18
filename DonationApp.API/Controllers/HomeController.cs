using DonationApp.Core.Entities;
using DonationApp.Core.Interfaces;
using DonationApp.Infrastructure.DataContext;
using DonationApp.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DonationApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ISeedDataService _seeder;

        public HomeController(ApplicationContext context, ISeedDataService seeder, RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            _seeder = new SeedDataService(context, roleManager, userManager);
        }

        [HttpPost("seed")]
        public async Task<IActionResult> Seed()
        {
            try
            {
                await _seeder.SeedDataAsync();

                return Ok(_seeder.Messages);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
