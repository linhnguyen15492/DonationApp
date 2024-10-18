using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.Core.Interfaces
{
    public interface ISeedDataService
    {
        Task SeedDataAsync();
        Queue<string> Messages { get; set; }
    }
}
