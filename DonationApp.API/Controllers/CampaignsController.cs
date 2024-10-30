using DonationApp.API.Hubs;
using DonationApp.Core.Entities;
using DonationApp.Core.Interfaces;
using DonationApp.UseCase.Models;
using DonationApp.UseCase.UseCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace DonationApp.API.Controllers
{
    [Route("api/campaign")]
    [ApiController]
    public class CampaignsController : ControllerBase
    {
        private readonly ICampaignService _campaignService;
        private IHubContext<MessageHub, IMessageHubClient> _messageHub;

        public CampaignsController(ICampaignService campaignService, IHubContext<MessageHub, IMessageHubClient> messageHub)
        {
            _campaignService = campaignService;
            _messageHub = messageHub;
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
                var messages = new List<string> { $"New Campaign {model.Name} Created" };
                await _messageHub.Clients.All.PushNotificationAsync(messages);

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
