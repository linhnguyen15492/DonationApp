using DonationApp.API.Hubs;
using DonationApp.Core.Entities;
using DonationApp.Core.Interfaces;
using DonationApp.Core.Interfaces.Repositories;
using DonationApp.Core.Shared;
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

        private readonly ISubscribeService _subscribeService;

        public CampaignsController(ICampaignService campaignService, IHubContext<MessageHub, IMessageHubClient> messageHub,
            ICampaignLikeService campaignLikeService, ICommentService commentService, ISubscribeService subscribeService)
        {
            _campaignService = campaignService;
            _messageHub = messageHub;
            _campaignLikeService = campaignLikeService;
            _commentService = commentService;
            _subscribeService = subscribeService;
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

        [HttpGet]
        [Route("get-campaigns/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCampaignsAsync([FromRoute] string userId)
        {
            var result = await _campaignService.GetAllCampaignByUserId(userId);

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

        [HttpPost("unlike-campagin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UnlikeCampaignAsync([FromBody] LikeCampaignModel model)
        {
            var result = await _campaignLikeService.UnlikeCampaignAsync(model);

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


        [HttpGet("isLiked/{userId}/{campaignId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> IsUserLikeCampaign(string userId, int campaignId)
        {
            var result = await _campaignLikeService.IsUserLikeCampaign(userId, campaignId);
            if (result)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }


        [HttpPost("subscribe")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SubscribeCampaign([FromBody] SubscribeModel model)
        {
            var result = await _campaignService.SubscribeCampaign(model);
            if (result)
            {
                return Ok(true);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("is-subscribe")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> IsSubscribedCampaign([FromBody] SubscribeModel model)
        {
            var result = await _campaignService.IsSubscribedCampaign(model);
            if (result)
            {
                return Ok(true);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("get-subscribers/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetSubscribersByCampaignId(int id)
        {
            var result = await _subscribeService.GetSubscribersByCampaignId(id);
            if (result is not null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("deactivate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeactivateCampaign([FromBody] int campaignId)
        {
            var result = await _campaignService.DeactivateCampaign(campaignId);

            if (result)
            {
                return Ok(true);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("activate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ActivateCampaign([FromBody] int campaignId)
        {
            var result = await _campaignService.ActivateCampaign(campaignId);

            if (result)
            {
                return Ok(true);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("verify-subscriber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ApproveSubscriber([FromBody] VerifySubscriberModel model)
        {
            var result = await _subscribeService.ApproveSubscriber(model.CampaignId, model.UserId);

            if (result)
            {
                return Ok(true);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("reject-subscriber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RejectSubscriber([FromBody] VerifySubscriberModel model)
        {
            var result = await _subscribeService.RejectSubscriber(model.CampaignId, model.UserId);

            if (result)
            {
                return Ok(true);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
