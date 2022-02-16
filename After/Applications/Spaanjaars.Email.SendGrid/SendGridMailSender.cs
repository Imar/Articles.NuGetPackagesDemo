using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SendGrid;
using Spaanjaars.Email.Infrastructure;

namespace Spaanjaars.Email.SendGrid
{
  /// <summary>
  /// An implementation if IMailSeder using SendGrid.
  /// </summary>
  public class SendGridMailSender : MailSenderBase
  {
    private readonly string _apiKey;

    /// <summary>
    /// Creates a new instance of the SendGridMailSender class.
    /// </summary>
    /// <param name="sendGridSettings">The settings for this class.</param>
    public SendGridMailSender(IOptions<SendGridSettings> sendGridSettings) : this(sendGridSettings.Value)
    {
    }

    /// <summary>
    /// Creates a new instance of the SendGridMailSender class.
    /// </summary>
    /// <param name="sendGridSettings">The settings for this class.</param>
    public SendGridMailSender(SendGridSettings sendGridSettings) : this(sendGridSettings.ApiKey)
    {
    }

    /// <summary>
    /// Creates a new instance of the SendGridMailSender class.
    /// </summary>
    /// <param name="apiKey">The api key used to connect to SendGrid. You would normally not add this constructor but use the one with IOptions instead.</param>
    public SendGridMailSender(string apiKey)
    {
      _apiKey = apiKey;
    }

    /// <summary>Sends an email.</summary>
    /// <param name="mailMessage">The message to send.</param>
    public override async Task SendMessageAsync(MailMessage mailMessage)
    {
      var client = new SendGridClient(_apiKey);
      var msg = mailMessage.GetSendGridMessage();
      await client.SendEmailAsync(msg);
    }
  }
}
