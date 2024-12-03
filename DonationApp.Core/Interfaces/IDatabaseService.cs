using DonationApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.Core.Interfaces
{
    public interface IDatabaseService
    {
        Task<bool> CreateDatabaseAsync();
        Task<DatabaseInfo> GetDatabaseInfo();

        Task<bool> DropDatabaseAsync();
    }
}
