using DonationApp.Core.Interfaces;
using DonationApp.Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.UseCase.UseCases
{
    public interface ICommentService
    {
        Task<Result<int>> AddCommentAsync(IModel model);
        Task<Result<int>> DeleteCommentAsync(int id);
        Task<Result<int>> UpdateCommentAsync(IModel model);
        Task<Result<IDto>> GetCommentsByCampaignIdAsync(int id);
    }
}
