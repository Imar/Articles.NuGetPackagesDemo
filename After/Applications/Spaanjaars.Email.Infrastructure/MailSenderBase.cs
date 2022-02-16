using System.IO;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Spaanjaars.Email.Infrastructure
{
  /// <summary>
  /// Base class for IMailSenders to inherit from and get some overloads + behavior for free.
  /// </summary>
  public abstract class MailSenderBase : IMailSender
  {
    /// <summary>
    /// Sends an email.
    /// </summary>
    /// <param name="from">The sender's email address.</param>
    /// <param name="to">The recipient's email address.</param>
    /// <param name="subject">The subject of the email.</param>
    /// <param name="body">The body text of the email.</param>
    public async Task SendMessageAsync(string from, string to, string subject, string body)
    {
      await SendMessageAsync(from, to, subject, body, true);
    }

    /// <summary>
    /// Sends an email.
    /// </summary>
    /// <param name="from">The sender's email address.</param>
    /// <param name="to">The recipient's email address.</param>
    /// <param name="subject">The subject of the email.</param>
    /// <param name="body">The body text of the email.</param>
    /// <param name="isBodyHtml">Determines if the body is sent as HTML or as plain text.</param>
    public async Task SendMessageAsync(string from, string to, string subject, string body, bool isBodyHtml)
    {
      var mailMessage = new MailMessage
      {
        From = new MailAddress(from),
        Subject = subject,
        Body = body,
        IsBodyHtml = isBodyHtml
      };
      mailMessage.To.Add(new MailAddress(to));
      await SendMessageAsync(mailMessage);
    }

    /// <summary>
    /// Sends an email.
    /// </summary>
    /// <param name="from">The sender's email address.</param>
    /// <param name="to">The recipient's email address.</param>
    /// <param name="subject">The subject of the email.</param>
    /// <param name="body">The body text of the email.</param>
    /// <param name="isBodyHtml">Determines if the body is sent as HTML or as plain text.</param>
    /// <param name="fileName">The name of the file to attach.</param>
    /// <param name="fileContents">The contents of the file to attach.</param>
    public async Task SendMessageAsync(string from, string to, string subject, string body, bool isBodyHtml, string fileName, Stream fileContents)
    {
      var mailMessage = new MailMessage
      {
        From = new MailAddress(from),
        Subject = subject,
        Body = body,
        IsBodyHtml = isBodyHtml
      };
      mailMessage.To.Add(new MailAddress(to));
      mailMessage.Attachments.Add(new Attachment(fileContents, fileName));
      await SendMessageAsync(mailMessage);
    }

    /// <summary>
    /// Sends an email.
    /// </summary>
    /// <param name="mailMessage">The message to send.</param>
    public abstract Task SendMessageAsync(MailMessage mailMessage);
  }
}