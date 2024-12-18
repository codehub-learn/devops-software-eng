using Microsoft.EntityFrameworkCore;
using Consul;
using System.Net;
using TelephoneAPI.Models;
	

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<TelephoneContext> (
	  opt => opt.UseInMemoryDatabase("TelephonesList")
);

builder.Services.AddRouting();

var consulURI = Environment.GetEnvironmentVariable("CONSUL_URI");
if (consulURI == null){
    consulURI = "http://consul:8500";
} 

builder.Services.AddSingleton<IConsulClient, ConsulClient>(
    p => new ConsulClient(
        config => {
            config.Address = new Uri(consulURI);
        }
    )
);

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

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

var consulClient = app.Services.GetRequiredService<IConsulClient>();
RegisterServiceWithConsul(consulClient, app.Lifetime);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void RegisterServiceWithConsul(IConsulClient consulClient, IHostApplicationLifetime lifetime){
    var registration = new AgentServiceRegistration{
        ID = Guid.NewGuid().ToString(),
        Name = "TelephoneAPI",
        Address = "telephoneapi",//Dns.GetHostName(),
        Port = 8080,
        Tags = new[] {"http"},
        Check = new AgentServiceCheck {
            HTTP = "http://telephoneapi:8080/health",
            Interval = TimeSpan.FromSeconds(10),
            Timeout = TimeSpan.FromSeconds(5)
        }
        
    };

    consulClient.Agent.ServiceRegister(registration).Wait();

    lifetime.ApplicationStopping.Register(() => 
        {
            consulClient.Agent.ServiceDeregister(registration.ID).Wait();
        }
    );    
}
