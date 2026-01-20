using InvestmentSimulator.Api.Workers;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Hubs;
using Server.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IInvestmentService, InvestmentService>(); builder.Services.AddHostedService<InvestmentExpirationWorker>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=investments.db"));



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")     
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();                  
    });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("AllowFrontend"); 


app.UseAuthorization();

app.MapControllers();

app.MapHub<InvestmentHub>("/hubs/investments");


app.Run();