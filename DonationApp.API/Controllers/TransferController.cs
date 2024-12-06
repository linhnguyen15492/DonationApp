using DonationApp.API.Hubs;
using DonationApp.Core.Interfaces;
using DonationApp.Infrastructure.Services;
using DonationApp.UseCase.Models;
using DonationApp.UseCase.UseCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace DonationApp.API.Controllers
{
    [Route("api/transfer")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private readonly ITransferManager _transferManager;
        private IHubContext<MessageHub, IMessageHubClient> _messageHub;

        List<string> notifications = new List<string>();

        public TransferController(ITransferManager transferManager, IHubContext<MessageHub, IMessageHubClient> messageHub)
        {
            _transferManager = transferManager;
            _messageHub = messageHub;
        }


        [HttpPost("donate")]
        public async Task<IActionResult> DonateAsync([FromBody] TransferModel model)
        {
            if (model.TransferType != Core.Enums.TransferTypeEnum.Donation)
            {
                return BadRequest();
            }

            var result = await _transferManager.DonateAsync(model);
            if (!result.IsSuccess)
            {
                return BadRequest(result.ResultCode.ToString());
            }

            notifications.Add($"{model.Sender} vừa thực hiện quyên góp cho {model.Receiver} số tiền {model.Amount:N0} đồng");
            await _messageHub.Clients.All.PushNotificationAsync(notifications);
            return Ok(result);
        }


        [HttpPost("disburse")]
        public async Task<IActionResult> DisburseAsync([FromBody] TransferModel model)
        {
            if (model.TransferType != Core.Enums.TransferTypeEnum.Disbursement)
            {
                return BadRequest();
            }

            var result = await _transferManager.DisburseAsync(model);
            if (!result.IsSuccess)
            {
                return BadRequest(result.ResultCode.ToString());
            }
            return Ok(result);
        }

        [HttpPost("mock-donate")]
        public async Task<IActionResult> MockDonateAsync([FromBody] TransferModel model)
        {

            notifications.Add($"{model.Sender} vừa thực hiện quyên góp cho {model.Receiver} số tiền {model.Amount:N0} đồng");
            await _messageHub.Clients.All.PushNotificationAsync(notifications);

            var result = TransactionResult.Success("", sender: model.Sender, receiver: model.Receiver);

            return Ok(await Task.FromResult(result));
        }
    }
}
