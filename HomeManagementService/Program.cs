#region

using FluentValidation;
using FluentValidation.AspNetCore;
using HomeManagementService.Extensions;
using HomeManagementService.Handlers;
using HomeManagementService.Middleware;
using HomeManagementService.Services;

#endregion

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerServiceCollection();
builder.Services.AddGeneralServiceCollection();
builder.Services.AddPingServiceCollection();
builder.Services.AddAuthServiceCollection();
builder.Services.AddWoLServiceCollection();
builder.Services.AddHueServiceCollection();
builder.Services.AddHangfireServiceCollection(builder.Configuration);
builder.Services.AddAutomationServiceCollection();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<AddAutomationCommand>, AddAutomationCommandValidator>();
builder.Services.AddScoped<IValidator<LocationCommand>, LocationCommandValidator>();
builder.Services.AddScoped<IValidator<ManualTriggerAutomationCommand>, ManualTriggerAutomationCommandValidator>();
builder.Services.AddScoped<IValidator<RemoveAutomationCommand>, RemoveAutomationCommandValidator>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();



var app = builder.Build();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHangfire(app.Configuration);
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();