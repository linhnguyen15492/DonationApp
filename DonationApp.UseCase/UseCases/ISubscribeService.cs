using DonationApp.Core.Entities;
using DonationApp.UseCase.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.UseCase.UseCases
{
    public interface ISubscribeService
    {
        Task<IEnumerable<Subscribers>> GetSubscribersByCampaignId(int campaignId);
    }
}
