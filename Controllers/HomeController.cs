using System.Diagnostics;
using AppSettingsAndSecrets.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppSettingsAndSecrets.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

    public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
    {
      _logger = logger;
      _configuration = configuration;
    }

    public IActionResult Index()
    {
      ViewBag.SendGridKey = _configuration.GetValue<string>("SendGrid");
      ViewBag.TwilioAuthToken = _configuration.GetSection("Twilio").GetValue<string>("AuthToken");
      ViewBag.TwilioAccountSid = _configuration.GetValue<string>("Twilio:AccountSid");
      
      ViewBag.ThreeLevelSettings = _configuration.GetValue<string>("FirstLevelSettings:SecondLevelSettings:BottomLevelSettings");

      // Option 2
      //ViewBag.ThreeLevelSettings = _configuration
      //  .GetSection("FirstLevelSettings")
      //  .GetSection("SecondLevelSettings")
      //  .GetValue<string>("BottomLevelSettings");

      // Option 3
      //ViewBag.ThreeLevelSettings = _configuration
      //  .GetSection("FirstLevelSettings")
      //  .GetSection("SecondLevelSettings")
      //  .GetSection("BottomLevelSettings").Value;

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
    }
}
