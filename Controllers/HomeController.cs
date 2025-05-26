using System.Diagnostics;
using AppSettingsAndSecrets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AppSettingsAndSecrets.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration _configuration;
    private readonly IOptions<TwilioSettings> _twilioOptions;
    private readonly TwilioSettings _twilioSettings;
    private readonly SocialLoginSettings _socialLoginSettings;


    public HomeController(
      ILogger<HomeController> logger,
      IConfiguration configuration,
      IOptions<TwilioSettings> twilioOptions,
      TwilioSettings twilioSettings,
      SocialLoginSettings socialLoginSettings)
    {
      _logger = logger;
      _configuration = configuration;
      _twilioOptions = twilioOptions;
      _twilioSettings = twilioSettings;
      _socialLoginSettings = socialLoginSettings;
    }

    public IActionResult Index()
    {
      ViewBag.SendGridKey = _configuration.GetValue<string>("SendGrid");

      // IOptions
      //ViewBag.TwilioAuthToken = _twilioOptions.Value.AuthToken;
      //ViewBag.TwilioAccountSid = _twilioOptions.Value.AccountSid;
      //ViewBag.TwilioPhoneNumber = _twilioOptions.Value.PhoneNumber;

      // Binding in Startup
      ViewBag.TwilioAuthToken = _twilioSettings.AuthToken;
      ViewBag.TwilioAccountSid = _twilioSettings.AccountSid;
      ViewBag.TwilioPhoneNumber = _twilioSettings.PhoneNumber;


      ViewBag.ThreeLevelSettings = _configuration.GetValue<string>("FirstLevelSettings:SecondLevelSettings:BottomLevelSettings");

      ViewBag.FacebookKey = _socialLoginSettings.FacebookSettings.Key;
      ViewBag.GoogleKey = _socialLoginSettings.GoogleSettings.Key;


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
