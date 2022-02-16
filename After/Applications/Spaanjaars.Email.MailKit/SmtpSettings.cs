namespace Spaanjaars.Email.MailKit
{
  /// <summary>
  /// Settings class for the SmtpClient.
  /// </summary>
  public class SmtpSettings
  {
    /// <summary>
    /// Gets the mail server to connect to.
    /// </summary>
    public string MailServer { get; private set; }

    /// <summary>
    /// Gets the mail server's port.
    /// </summary>
    public int Port { get; private set; }

    /// <summary>
    /// Gets the user name to authenticate.
    /// </summary>
    public string UserName { get; private set; }

    /// <summary>
    /// Gets the password to authenticate.
    /// </summary>
    public string Password { get; private set; }

    /// <summary>
    /// Gets a value that determines if the connection should use SSL.
    /// </summary>
    public bool UseSsl { get; private set; }

    /// <summary>
    /// Gets the priority of this settings item relative to others in a list.
    /// </summary>
    public int Priority { get; private set; }
  }
}