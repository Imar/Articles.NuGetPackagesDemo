using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Spaanjaars.Email.Development.Configuration;
using Spaanjaars.Email.Infrastructure;

namespace Spaanjaars.Email.Development
{
  /// <summary>
  /// An implementation of IMailSender that redirects all recipients to a debug address.
  /// </summary>
  public class InternalMailSender : MailSenderBase
  {
    private readonly string _debugAddress;
    private readonly IMailSender _mailSender;

    /// <summary>
    /// Creates a new instance of the InternalMailSender class.
    /// </summary>
    /// <param name="configuration">An instance of InternalMailConfiguration with configuration data.</param>
    /// <param name="mailSender">Another instance of IMailSender to send the email with.</param>
    public InternalMailSender(InternalMailConfiguration configuration, IMailSender mailSender)
    {
      _debugAddress = configuration.DebugAddress;
      _mailSender = mailSender;
    }

    /// <summary>Sends an email.</summary>
    /// <param name="mailMessage">The message to send.</param>
    public override async Task SendMessageAsync(MailMessage mailMessage)
    {
      var sb = new StringBuilder();
      var lineBreak = mailMessage.IsBodyHtml ? "<br />" : "\r\n";
      foreach (var mailAddress in mailMessage.To)
      {
        sb.Append(BuildLine(nameof(mailMessage.To), mailAddress, lineBreak));
      }
      foreach (var mailAddress in mailMessage.CC)
      {
        sb.Append(BuildLine(nameof(mailMessage.CC), mailAddress, lineBreak));
      }
      foreach (var mailAddress in mailMessage.Bcc)
      {
        sb.Append(BuildLine(nameof(mailMessage.Bcc), mailAddress, lineBreak));
      }

      sb.Append(mailMessage.Body);
      mailMessage.Body = sb.ToString();

      mailMessage.To.Clear();
      mailMessage.CC.Clear();
      mailMessage.Bcc.Clear();
      mailMessage.To.Add(_debugAddress);

      mailMessage.BodyEncoding = Encoding.UTF8;
      await _mailSender.SendMessageAsync(mailMessage);
    }

    private string BuildLine(string collectionName, MailAddress address, string lineBreak)
    {
      var result = $"Message ({collectionName}) addressed to {address.Address}";
      if (!string.IsNullOrWhiteSpace(address.DisplayName))
      {
        result += $" ({address.DisplayName})";
      }
      return result + lineBreak;
    }
  }
}
