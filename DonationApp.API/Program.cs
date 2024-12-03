using DonationApp.API.Configuration;
using DonationApp.API.Hubs;
using DonationApp.Core.Entities;
using DonationApp.Core.Interfaces;
using DonationApp.Core.Interfaces.Repositories;
using DonationApp.Infrastructure.DataContext;
using DonationApp.Infrastructure.Repositories;
using DonationApp.Infrastructure.Services;
using DonationApp.Infrastructure.UnitOfWork;
using DonationApp.UseCase.Repositories;
using DonationApp.UseCase.UseCases;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("PostgreSQL"));

builder.Services.AddDbContext<ApplicationContext>((provider, options) =>
{
    var settings = provider.GetRequiredService<IOptions<DatabaseSettings>>().Value;
    var connectionString = $"Host={settings.Host};port={settings.Port};Database={settings.Database};Username={settings.Username};Password={settings.Password}";
    options.UseNpgsql(connectionString);
});

builder.Services.AddDbContext<ApplicationContext>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ICampaignRepository, CampaignRepository>();
builder.Services.AddScoped<ICampaignAccountRepository, CampaignAccountRepository>();
builder.Services.AddScoped<IUserAccountRepository, UserAccountRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<ICampaignLikeRepository, CampaignLikeRepository>();
builder.Services.AddScoped<ICampaignLikeCountRepository, CampaignLikeCountRepository>();


builder.Services.AddScoped<ITransactionUnitOfWork, TransactionUnitOfWork>();
builder.Services.AddScoped<ICampaignLikeUnitOfWork, CampaignLikeUnitOfWork>();


builder.Services.AddScoped<ISeedDataService, SeedDataService>();
builder.Services.AddScoped<ICampaignService, CampaignService>();
builder.Services.AddScoped<ICampaignLikeService, CampaignLikeService>();
builder.Services.AddScoped<IDatabaseService, DatabaseService>();
//builder.Services.AddScoped<ICommentService, ICommentService>();

builder.Services.AddScoped<ITransferManager, TransferManager>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationContext>()
    .AddDefaultTokenProviders();

builder.Services.AddSignalR();


builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy", builder => builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed((hosts) => true));
});


builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CORSPolicy");

app.UseAuthorization();

app.MapHub<MessageHub>("/messageHub");
app.MapControllers();

app.Run();
