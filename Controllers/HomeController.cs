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
      ViewBag.TwilioAuthToken = _configuration.GetValue<string>("Twilio:AuthToken");
      ViewBag.TwilioAccountSid = _configuration.GetValue<string>("Twilio:AccountSid");
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
