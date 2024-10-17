using DonationApp.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.Core.UseCases
{
    public interface IGetAllReliefOperations
    {
        Task<IEnumerable<ReliefOperationDto>> GetAllReliefOperations();
    }
}
