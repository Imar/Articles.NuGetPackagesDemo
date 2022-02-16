// ReSharper disable CheckNamespace // Using Microsoft.Extensions.DependencyInjection for these extension methods is a best practice.

using System;
using Spaanjaars.Email.Infrastructure;
using Spaanjaars.Email.SendGrid;

namespace Microsoft.Extensions.DependencyInjection
{
  /// <summary>
  /// Extension class for services registration.
  /// </summary>
  public static class SendGridExtensions
  {
    /// <summary>
    /// Adds and configures the SendGridMailSender.
    /// </summary>
    /// <param name="services">The services collection being extended.</param>
    /// <param name="settings">An Action to populate an instance of the SendGridSettings class.</param>
    public static IServiceCollection AddSendGrid(this IServiceCollection services, Action<SendGridSettings> settings)
    {
      var instance = new SendGridSettings();
      settings(instance);
      services.AddSingleton<IMailSender, SendGridMailSender>(x => new SendGridMailSender(instance.ApiKey));
      return services;
    }
  }
}