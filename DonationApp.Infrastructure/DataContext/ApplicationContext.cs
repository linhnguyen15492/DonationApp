using DonationApp.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DonationApp.Infrastructure.DataContext
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        private readonly string _connectionString = "Host=localhost;port=5433;Database=DonationApp;Username=postgres;Password=181117";

        // Tạo ILoggerFactory 
        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        {
            builder
                   .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Warning)
                   .AddFilter(DbLoggerCategory.Query.Name, LogLevel.Debug)
                   .AddConsole();
        });

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseNpgsql(_connectionString)
                .UseLoggerFactory(loggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Bỏ tiền tố AspNet của các bảng: mặc định các bảng trong IdentityDbContext có
            // tên với tiền tố AspNet như: AspNetUserRoles, AspNetUser ...
            // Đoạn mã sau chạy khi khởi tạo DbContext, tạo database sẽ loại bỏ tiền tố đó
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();

                if (tableName is not null)
                {
                    if (tableName.StartsWith("AspNet"))
                    {
                        entityType.SetTableName(tableName.Substring(6));
                    }
                }
            }
        }

        //public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{
        //    var entries = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);
        //    foreach (var entry in entries)
        //    {
        //        if (entry.State == EntityState.Added)
        //        {
        //            ((BaseEntity)entry.Entity).CreatedAt = DateTime.UtcNow;
        //        }
        //        else
        //        {
        //            ((BaseEntity)entry.Entity).UpdatedAt = DateTime.UtcNow;
        //        }

        //    }


        //    return base.SaveChangesAsync(cancellationToken);
        //}

        //public override int SaveChanges()
        //{
        //    var entries = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);
        //    foreach (var entry in entries)
        //    {

        //        if (entry.State == EntityState.Added)
        //        {
        //            ((BaseEntity)entry.Entity).CreatedAt = DateTime.UtcNow;
        //        }
        //        else
        //        {
        //            ((BaseEntity)entry.Entity).UpdatedAt = DateTime.UtcNow;
        //        }

        //    }

        //    return base.SaveChanges();
        //}


        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<CampaignAccount> CampaignAccounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }


    }
}
