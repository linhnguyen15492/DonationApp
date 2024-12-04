using DonationApp.Core.Interfaces;
using DonationApp.UseCase.Models;
using DonationApp.UseCase.UseCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DonationApp.API.Controllers
{
    [Route("api/transfer")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private readonly ITransferManager _transferManager;

        public TransferController(ITransferManager transferManager)
        {
            _transferManager=transferManager;
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
    }
}
