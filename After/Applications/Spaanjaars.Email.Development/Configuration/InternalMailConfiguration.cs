namespace Spaanjaars.Email.Development.Configuration
{
  /// <summary>
  /// Configuration class to use when configuring IMailSenders.
  /// </summary>
  public class InternalMailConfiguration
  {
    /// <summary>
    /// The folder where the emails are saved.
    /// </summary>
    public string DebugAddress { get; private set; }
  }
}
