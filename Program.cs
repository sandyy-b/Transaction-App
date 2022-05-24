using FluentAssertions.Common;
using Microsoft.EntityFrameworkCore;
using TransactionApp.DAL;
using TransactionApp.Services;
using TransactionApp.Services.Implementations;
using TransactionApp.Services.Interfaces;
using TransactionApp.Utils;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BankingDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("BankingDbConnection"));

});
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Transaction app API doc",
        Version = "v1",
        Description = "A Worth Transaction App!",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Sandy B",
            Email = "sandy@appwrk.com"
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

app.UseSwagger();

app.UseSwaggerUI(x =>
{
    var prefix = string.IsNullOrEmpty(x.RoutePrefix) ? "." : "..";
    x.SwaggerEndpoint($"{prefix}/swagger/v1/swagger.json", "Transaction app API doc");
});