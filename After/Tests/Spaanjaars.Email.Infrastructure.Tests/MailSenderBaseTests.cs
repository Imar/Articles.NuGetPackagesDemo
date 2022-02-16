using System.Net.Mail;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Xunit;

namespace Spaanjaars.Email.Infrastructure.Tests;

public class MailSenderBaseTests
{
  private const string FromAddress = "from@example.org";
  private const string ToAddress = "to@example.org";
  private const string Subject = "Subject";
  private const string Body = "This is the email body";

  [Fact]
  public async Task SimpleOverloadsCallFinalMethod()
  {
    var sut = new Mock<TestMailSender>();

    sut.Setup(x => x.SendMessageAsync(It.IsAny<MailMessage>())).Callback((MailMessage message) =>
    {
    // This fires after SendMessageAsync below has been called and verifies the results.
    message.IsBodyHtml.Should().BeTrue();
      message.From?.Address.Should().Be(FromAddress);
      message.Subject.Should().Be(Subject);
    });
    await sut.Object.SendMessageAsync(FromAddress, ToAddress, Subject, Body);
    sut.Verify(x => x.SendMessageAsync(It.IsAny<MailMessage>()), Times.Once);
  }

  [Fact]
  public async Task HtmlOverloadsCallFinalMethodAndSetsHtml()
  {
    var sut = new Mock<TestMailSender>();
    sut.Setup(x => x.SendMessageAsync(It.IsAny<MailMessage>())).Callback((MailMessage message) =>
    {
    // This fires after SendMessageAsync below has been called and verifies the results.
    message.IsBodyHtml.Should().BeFalse();
      message.From?.Address.Should().Be(FromAddress);
      message.Subject.Should().Be(Subject);
    });
    await sut.Object.SendMessageAsync(FromAddress, ToAddress, Subject, Body, false);
    sut.Verify(x => x.SendMessageAsync(It.IsAny<MailMessage>()), Times.Once);
  }
}