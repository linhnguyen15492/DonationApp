using DonationApp.Core.Entities;
using DonationApp.Core.Interfaces;
using DonationApp.Infrastructure.DataContext;
using DonationApp.Infrastructure.Services;
using DonationApp.UseCase.UseCases;
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
        private readonly IDatabaseService _databaseService;

        public HomeController(ApplicationContext context, ISeedDataService seeder, RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            IDatabaseService databaseService, ICampaignService campaignService)
        {
            _seeder = new SeedDataService(context, roleManager, userManager, campaignService);
            _databaseService = databaseService;
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


        [HttpPost("db/create-database")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateDatabaseAsync()
        {
            var res = await _databaseService.CreateDatabaseAsync();

            if (res)
            {
                return Ok();
            }
            else
            {
                return StatusCode(500);
            }

        }

        [HttpGet("db/info")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDatabaseInfo()
        {
            try
            {
                var result = await _databaseService.GetDatabaseInfo();

                //result.Server = _databaseSettings.Value.Server;
                //result.Port = _databaseSettings.Value.Port;
                //result.User = _databaseSettings.Value.User;

                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        [HttpPost("db/drop-database")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DropDatabaseAsync()
        {
            var res = await _databaseService.DropDatabaseAsync();
            if (res)
            {
                return Ok();
            }
            else
            {
                return StatusCode(500);
            }
        }

    }
}
