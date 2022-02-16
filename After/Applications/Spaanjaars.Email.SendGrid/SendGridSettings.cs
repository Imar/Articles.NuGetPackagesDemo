namespace Spaanjaars.Email.SendGrid
{
  /// <summary>
  /// Settings class for SendGrid.
  /// </summary>
  public class SendGridSettings
  {
    /// <summary>
    /// Gets the API key used to connect to SendGrid.
    /// </summary>
    public string ApiKey { get; private set; }
  }
}