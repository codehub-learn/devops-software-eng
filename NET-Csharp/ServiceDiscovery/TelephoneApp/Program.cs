using Consul;
using TelephoneApp.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();

var consulURI = Environment.GetEnvironmentVariable("CONSUL_URI");
if (consulURI == null){
    consulURI = "http://localhost:8500";
} 

// Register Consul Client in Dependency Injection (DI)
builder.Services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(config =>
{
    // Consul agent address (make sure Consul is running here)
    config.Address = new Uri(consulURI);
}));
// Register ConsulServiceDiscovery as a service in DI
builder.Services.AddSingleton<ConsulServiceDiscovery>();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
