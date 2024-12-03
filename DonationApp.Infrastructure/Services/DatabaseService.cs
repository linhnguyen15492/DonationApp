using DonationApp.Core.Entities;
using DonationApp.Core.Interfaces;
using DonationApp.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.Infrastructure.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly ApplicationContext _context;

        public DatabaseService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<DatabaseInfo> GetDatabaseInfo()
        {
            var db = new DatabaseInfo()
            {
                CanConnect = await _context.Database.CanConnectAsync(),
                DatabaseName = _context.Database.GetDbConnection().Database,
                TableNames = new List<string>(),
            };

            var tableNames = _context.Model.GetEntityTypes()
                .Select(t => t.GetTableName())
                .Distinct()
                .ToArray();

            if (tableNames.Length == 0)
            {
                return db;
            }
            else
            {
                db.TableNames.AddRange(tableNames!);
            }

            if (db.CanConnect)
            {
                db.IsSeeded = await _context.Campaigns.AnyAsync();
            }

            return db;
        }

        public async Task<bool> CreateDatabaseAsync()
        {
            var res = await _context.Database.EnsureCreatedAsync();

            return res;
        }

        public async Task<bool> DropDatabaseAsync()
        {
            var res = await _context.Database.EnsureDeletedAsync();

            return res;
        }
    }
}