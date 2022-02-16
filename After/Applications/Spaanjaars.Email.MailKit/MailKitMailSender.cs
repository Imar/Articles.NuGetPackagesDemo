using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Spaanjaars.Email.Infrastructure;

namespace Spaanjaars.Email.MailKit
{
  /// <summary>
  /// An implementation if IMailSeder using MailKit.
  /// </summary>
  public class MailKitMailSender : MailSenderBase
  {
    private readonly SmtpSettings _emailSettings;

    /// <summary>
    /// Creates a new instance of the MailKitMailSender class.
    /// </summary>
    /// <param name="emailSettings">A collection of SMTP settings. This implementation currently  only uses the entry with the highest priority.</param>
    public MailKitMailSender(IOptions<List<SmtpSettings>> emailSettings)
    {
      if (!emailSettings.Value.Any())
      {
        throw new ArgumentException("Must specify at least one SMTP option.");
      }
      _emailSettings = emailSettings.Value.OrderBy(x => x.Priority).First();
    }

    /// <summary>Sends an email.</summary>
    /// <param name="mailMessage">The message to send.</param>
    public override async Task SendMessageAsync(System.Net.Mail.MailMessage mailMessage)
    {
      var message = (MimeMessage)mailMessage;

      using (var client = new SmtpClient())
      {
        await client.ConnectAsync(_emailSettings.MailServer, _emailSettings.Port, _emailSettings.UseSsl);
        await client.AuthenticateAsync(_emailSettings.UserName, _emailSettings.Password);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
      }
    }
  }
}
