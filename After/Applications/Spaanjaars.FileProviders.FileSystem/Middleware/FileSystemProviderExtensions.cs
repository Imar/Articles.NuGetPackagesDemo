// ReSharper disable CheckNamespace // Using Microsoft.Extensions.DependencyInjection for these extension methods is a best practice.

using Spaanjaars.FileProviders.FileSystem;
using Spaanjaars.FileProviders.Infrastructure.Files;

namespace Microsoft.Extensions.DependencyInjection
{
  /// <summary>
  /// Extension class for services registration.
  /// </summary>
  public static class FileSystemProviderExtensions
  {
    /// <summary>
    /// Adds and configures the FileSystemFileProvider.
    /// </summary>
    /// <param name="services">The services collection being extended.</param>
    /// <param name="rootFolder">The folder to use as the root of the file system.</param>
    public static IServiceCollection AddFileSystemFileProvider(this IServiceCollection services, string rootFolder)
    {
      services.AddSingleton<IFileProvider, FileSystemFileProvider>(x => new FileSystemFileProvider(rootFolder));
      return services;
    }
  }
}