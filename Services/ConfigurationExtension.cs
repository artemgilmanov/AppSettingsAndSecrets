using Microsoft.Extensions.Options;

namespace AppSettingsAndSecrets.Services;

public static class ConfigurationExtension
{
  public static void AddConfiguration<T>(this IServiceCollection services, IConfiguration configuration, string configuraionSection = null)
    where T : class
  {
    if (string.IsNullOrEmpty(configuraionSection))
    {
      configuraionSection = nameof(T);
    }

    var instance = Activator.CreateInstance<T>();
    new ConfigureFromConfigurationOptions<T>(configuration.GetSection(configuraionSection)).Configure(instance);
    services.AddSingleton(instance);
  }
}
