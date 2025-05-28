using AppSettingsAndSecrets.Models;
using AppSettingsAndSecrets.Services;

namespace AppSettingsAndSecrets
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      // Add services to the container.
      builder.Services.AddControllersWithViews();


      builder.Services.AddConfiguration<TwilioSettings>(builder.Configuration, "Twilio");

      builder.Services.AddConfiguration<SocialLoginSettings>(builder.Configuration, "SocialLoginSettings");
      // Configuration to use DI in View
      builder.Services.Configure<SocialLoginSettings>(builder.Configuration.GetSection("SocialLoginSettings"));

      builder.Services.Configure<TwilioSettings>(builder.Configuration.GetSection("Twilio"));

      // Configuration
      builder.Configuration.Sources.Clear();
      builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
      //builder.Configuration.AddJsonFile("customSettings.json", optional: true, reloadOnChange: true);
      builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

      if (builder.Environment.IsDevelopment())
      {
        builder.Configuration.AddUserSecrets<Program>();
      }
      builder.Configuration.AddEnvironmentVariables();
      builder.Configuration.AddJsonFile("customSettings.json", optional: true, reloadOnChange: true);
      builder.Configuration.AddCommandLine(args);

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
    }
  }
}
