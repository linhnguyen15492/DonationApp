using DonationApp.Core.Entities;
using DonationApp.Core.Interfaces;
using DonationApp.UseCase.Models;
using DonationApp.UseCase.UseCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DonationApp.API.Controllers
{
    [Route("api/campaign")]
    [ApiController]
    public class CampaignsController : ControllerBase
    {
        private readonly ICampaignService _campaignService;

        public CampaignsController(ICampaignService campaignService)
        {
            _campaignService = campaignService;
        }

        [HttpPost]
        [Route("create-campaign")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCampaignAsync([FromBody] CampaignModel model)
        {
            var result = await _campaignService.CreateCampaignAsync(model);

            if (!result.IsSuccess)
            {

                return BadRequest(result.Errors);
            }
            else
            {
                return Ok(result.Value);
            }
        }

        [HttpGet]
        [Route("get-campaign/{id}")]
        public async Task<IActionResult> GetCampaignByIdAsync([FromRoute] object id)
        {
            var result = await _campaignService.GetCampaignByIdAsync(id);
            return Ok(result);
        }

        [HttpGet]
        [Route("get-all-campaigns")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCampaignsAsync()
        {
            var result = await _campaignService.GetAllCampaignsAsync();

            if (!result.IsSuccess)
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            else
            {
                return Ok(result.Value);
            }
        }
    }
}
