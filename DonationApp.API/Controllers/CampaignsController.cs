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

        private readonly ICampaignLikeService _campaignLikeService;

        private readonly ICommentService _commentService;

        public CampaignsController(ICampaignService campaignService, IHubContext<MessageHub, IMessageHubClient> messageHub,
            ICampaignLikeService campaignLikeService, ICommentService commentService)
        {
            _campaignService = campaignService;
            _messageHub = messageHub;
            _campaignLikeService = campaignLikeService;
            _commentService = commentService;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCampaignByIdAsync([FromRoute] int id)
        {
            var result = await _campaignService.GetCampaignByIdAsync(id);
            if (!result.IsSuccess)
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");

            return Ok(result.Value);
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

        [HttpPost("like-campagin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LikeCampaignAsync([FromBody] LikeCampaignModel model)
        {
            var result = await _campaignLikeService.LikeCampaignAsync(model);

            if (result == -1)
            {
                return BadRequest();
            }
            else
            {
                return Ok("Success");
            }
        }

        [HttpPost("add-comment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddComment([FromBody] CommentModel model)
        {
            var result = await _commentService.AddCommentAsync(model);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }
            else
            {
                return Ok(result.Value);
            }
        }
    }
}
