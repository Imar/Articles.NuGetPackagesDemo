using System.Net.Mail;
using System.Threading.Tasks;

namespace Spaanjaars.Email.Infrastructure.Tests;

public class TestMailSender : MailSenderBase
{
  public override Task SendMessageAsync(MailMessage mailMessage)
  {
    return Task.CompletedTask;
  }
}
