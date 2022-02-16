using System.IO;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Spaanjaars.Email.Development.Configuration;
using Spaanjaars.Email.Infrastructure;

namespace Spaanjaars.Email.Development
{
  /// <summary>
  /// Helper class for unit tests that always writes the email file to disk.
  /// </summary>
  public class DropMailOnLocalDiskMailSender : MailSenderBase
  {
    private static string _rootFolder;

    /// <summary>
    /// Creates a new instance of the DropMailOnLocalDiskMailSender class.
    /// </summary>
    /// <param name="optionsDelegate">The configuration for this class wrapped in an IOptionsMonitor to be notified of changes</param>
    public DropMailOnLocalDiskMailSender(IOptionsMonitor<MailOnDiskConfiguration> optionsDelegate)
    {
      EnsureFolder(optionsDelegate.CurrentValue.MailFolder);
      optionsDelegate.OnChange(x => EnsureFolder(x.MailFolder));
    }

    private static void EnsureFolder(string folder)
    {
      _rootFolder = folder;
      Directory.CreateDirectory(_rootFolder);
    }

    ///<summary>
    /// Sends an email.
    /// </summary>
    /// <param name="message">The message to send.</param>
    public override Task SendMessageAsync(MailMessage message)
    {
      using (var smtpClient = new SmtpClient { DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory, PickupDirectoryLocation = _rootFolder })
      {
        message.BodyEncoding = System.Text.Encoding.UTF8;
        smtpClient.Send(message);
      }
      return Task.CompletedTask;
    }
  }
}
