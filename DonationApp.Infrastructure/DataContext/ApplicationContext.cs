using DonationApp.Core.Entities;
using DonationApp.Core.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace DonationApp.Infrastructure.DataContext
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
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

            builder.Entity<CampaignLike>().HasKey(cl => new { cl.CampaignId, cl.UserId });

            builder.Entity<CampaignLikeCount>()
                .HasIndex(c => c.CampaignId)
                .IsUnique();

            builder.Entity<SubscribeCampaign>().HasKey(sc => new { sc.CampaignId, sc.UserId });
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);
            foreach (var entry in entries)
            {
                if (entry.Entity is IAuditEntity auditEntity)
                {

                    if (entry.State == EntityState.Added)
                    {
                        auditEntity.CreatedAt = DateTime.UtcNow;
                    }
                    else
                    {
                        auditEntity.UpdatedAt = DateTime.UtcNow;
                    }

                    //// Lấy type của entry.Entity
                    //Type entityType = entry.Entity.GetType();
                    //if (entityType.IsGenericType && entityType.GetGenericTypeDefinition() == typeof(AuditEntity<>))
                    //{
                    //    Type genericArgument = entityType.GetGenericArguments()[0]; // Lấy ra kiểu của T

                    //    if (entry.State == EntityState.Added)
                    //    {
                    //        auditEntity.CreatedAt = DateTime.UtcNow;
                    //    }
                    //    else
                    //    {
                    //        auditEntity.UpdatedAt = DateTime.UtcNow;
                    //    }
                    //}
                }

                //if (entry.Entity is IAuditEntity)
                //{

                //    if (entry.State == EntityState.Added)
                //    {
                //        //((BaseEntity)entry.Entity).CreatedAt = DateTime.UtcNow;

                //        ((AuditEntity<t)>)entry.Entity).CreatedAt = DateTime.UtcNow;
                //    }
                //    else
                //    {
                //        //((BaseEntity)entry.Entity).UpdatedAt = DateTime.UtcNow;
                //        ((AuditEntity<Type>)entry.Entity).UpdatedAt = DateTime.UtcNow;
                //    }
                //}
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);
            foreach (var entry in entries)
            {
                if (entry.Entity is IAuditEntity auditEntity)
                {

                    if (entry.State == EntityState.Added)
                    {
                        auditEntity.CreatedAt = DateTime.UtcNow;
                    }
                    else
                    {
                        auditEntity.UpdatedAt = DateTime.UtcNow;
                    }


                    // Lấy type của entry.Entity
                    //Type entityType = entry.Entity.GetType();
                    //if (entityType.IsGenericType && entityType.GetGenericTypeDefinition() == typeof(AuditEntity<>))
                    //{
                    //    Type genericArgument = entityType.GetGenericArguments()[0]; // Lấy ra kiểu của T

                    //    if (entry.State == EntityState.Added)
                    //    {
                    //        auditEntity.CreatedAt = DateTime.UtcNow;
                    //    }
                    //    else
                    //    {
                    //        auditEntity.UpdatedAt = DateTime.UtcNow;
                    //    }
                    //}
                }



                //if (entry.Entity is BaseEntity)
                //{
                //    if (entry.State == EntityState.Added)
                //    {
                //        ((BaseEntity)entry.Entity).CreatedAt = DateTime.UtcNow;
                //    }
                //    else
                //    {
                //        ((BaseEntity)entry.Entity).UpdatedAt = DateTime.UtcNow;
                //    }
                //}
            }

            return base.SaveChanges();
        }


        public DbSet<Campaign> Campaigns { get; set; }

        public DbSet<UserAccount> UserAccounts { get; set; }

        public DbSet<CampaignAccount> CampaignAccounts { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<CampaignLike> CampaignLikes { get; set; }

        public DbSet<CampaignLikeCount> CampaignLikeCounts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<SubscribeCampaign> SubscribeCampaigns { get; set; }
    }
}
