// ReSharper disable CheckNamespace // Using Microsoft.Extensions.DependencyInjection for these extension methods is a best practice.

using Microsoft.Extensions.Configuration;
using Spaanjaars.Email.Development;
using Spaanjaars.Email.Development.Configuration;
using Spaanjaars.Email.Infrastructure;

namespace Microsoft.Extensions.DependencyInjection
{
  /// <summary>
  /// Extension class for services registration.
  /// </summary>
  public static class MailOnDiskServiceExtensions
  {
    /// <summary>
    /// Adds and configures the necessary SMTP classes.
    /// </summary>
    /// <param name="services">The services collection being extended.</param>
    /// <param name="configuration">The configuration data used to register and configure the services.</param>
    public static IServiceCollection AddEmailToLocalDisk(this IServiceCollection services, IConfiguration configuration)
    {
      services.Configure<MailOnDiskConfiguration>(configuration);
      services.AddSingleton<IMailSender, DropMailOnLocalDiskMailSender>();
      return services;
    }
  }
}