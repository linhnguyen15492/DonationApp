using DonationApp.Core.Interfaces;
using DonationApp.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.UseCase.UseCases
{
    public interface ICampaignLikeUnitOfWork : IUnitOfWork
    {
        ICampaignLikeRepository CampaignLikeRepository { get; }
        ICampaignLikeCountRepository CampaignLikeCounterRepository { get; }
    }
}
