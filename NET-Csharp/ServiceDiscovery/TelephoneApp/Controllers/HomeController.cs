using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TelephoneApp.Models;
using TelephoneApp.Services;
using Newtonsoft.Json;


namespace TelephoneApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly HttpClient _httpClient;
    private readonly ConsulServiceDiscovery _serviceDiscovery;

    public HomeController(ILogger<HomeController> logger, 
                        HttpClient httpClient,
                        ConsulServiceDiscovery consulServiceDiscovery)
    {
        _logger = logger;
        _serviceDiscovery = consulServiceDiscovery;
        _httpClient = httpClient;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  	public async Task<IActionResult> TelephoneResults(){
        var serviceUri = await _serviceDiscovery.DiscoverServiceAsync("TelephoneAPI");
        var response = _httpClient.GetAsync(serviceUri+"api/telephoneAPI");
        HttpResponseMessage responseMessage = response.Result;
        var result = responseMessage.Content.ReadAsStringAsync().Result;
        List<Model> results = JsonConvert.DeserializeObject<List<Model>>(result);
  	    return View(results);
   	}
    public async Task<IActionResult> TelephoneResult(string telephoneID){
        var serviceUri = await _serviceDiscovery.DiscoverServiceAsync("TelephoneAPI");
	    var response = _httpClient.GetAsync(serviceUri + $"api/TelephoneAPI/{telephoneID}");
	    HttpResponseMessage responseMessage = response.Result;
	    var result = responseMessage.Content.ReadAsStringAsync().Result;
	    Model results = JsonConvert.DeserializeObject<Model>(result);
  	    return View(results);
	}
    public IActionResult Search(){
        return View();
    }
}
