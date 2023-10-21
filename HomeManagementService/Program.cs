using System.Net.NetworkInformation;
using System.Reflection;
using EasyWakeOnLan;
using ReportingServiceWorker.Auth;
using ReportingServiceWorker.Extensions;
using ReportingServiceWorker.Interfaces;
using ReportingServiceWorker.Models.Options;
using ReportingServiceWorker.Services;
using ReportingServiceWorker.Workers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerServiceCollection();
builder.Services.AddGeneralServiceCollection();
builder.Services.AddPingServiceCollection();
builder.Services.AddAuthServiceCollection();
builder.Services.AddWoLServiceCollection();
builder.Services.AddHueServiceCollection();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();