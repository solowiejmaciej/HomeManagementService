using System.Net.NetworkInformation;
using System.Reflection;
using ReportingServiceWorker.Interfaces;
using ReportingServiceWorker.Models.Options;
using ReportingServiceWorker.Services;
using ReportingServiceWorker.Workers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<HostOptions>(options =>
    {
        options.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
        options.ShutdownTimeout = TimeSpan.Zero;
    });
builder.Services.AddSingleton<INotificationApiClient, NotificationApiClient>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddSingleton<Ping>();
builder.Services.AddSingleton<IPingService, PingService>();
builder.Services.AddOptions<Devices>().BindConfiguration("Devices");
builder.Services.AddHostedService<PingWorker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();