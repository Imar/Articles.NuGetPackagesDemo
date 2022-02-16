namespace Spaanjaars.Email.Development.Configuration
{
  /// <summary>
  /// Configuration class to use when configuring IMailSenders.
  /// </summary>
public class MailOnDiskConfiguration
{
  /// <summary>
  /// The folder where the emails are saved.
  /// </summary>
  public string MailFolder { get; set; }
}
}
