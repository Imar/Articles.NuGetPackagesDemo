// ReSharper disable CheckNamespace // Using Microsoft.Extensions.DependencyInjection for these extension methods is a best practice.

using Spaanjaars.FileProviders.Azure;
using Spaanjaars.FileProviders.Azure.Configuration;
using Spaanjaars.FileProviders.Infrastructure.Files;

namespace Microsoft.Extensions.DependencyInjection
{
  /// <summary>
  /// Extension class for services registration.
  /// </summary>
  public static class AzureStorageProviderExtensions
  {
    /// <summary>
    /// Adds and configures the AzureStorageFileProvider.
    /// </summary>
    /// <param name="services">The services collection being extended.</param>
    /// <param name="configuration">An instance of AzureStorageConfiguration with the configuration data.</param>
    public static IServiceCollection AddAzureStorageFileProvider(this IServiceCollection services, AzureStorageConfiguration configuration)
    {
      services.AddSingleton<IFileProvider, AzureStorageFileProvider>(x => new AzureStorageFileProvider(configuration.ConnectionString));
      return services;
    }
  }
}