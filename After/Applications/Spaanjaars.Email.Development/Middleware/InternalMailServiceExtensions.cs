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
  public static class InternalMailServiceExtensions
  {
    /// <summary>
    /// Registers an instance of InternalMailSender to redirect all email to a predefined email address.
    /// </summary>
    /// <param name="services">The services collection being extended.</param>
    /// <param name="configuration">The configuration data used to register and configure the services.</param>
    /// <param name="mailSender">An instance of IMailSender to send the email with.</param>
    public static IServiceCollection AddInternalMailSender(this IServiceCollection services, IConfigurationSection configuration, IMailSender mailSender)
    {
      var mailConfig = new InternalMailConfiguration();
      configuration.Bind(mailConfig, options => options.BindNonPublicProperties = true);
      services.AddSingleton<IMailSender, InternalMailSender>(x => new InternalMailSender(mailConfig, mailSender));
      return services;
    }
  }
}