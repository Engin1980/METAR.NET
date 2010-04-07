using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetarDownloader
{
  /// <summary>
  /// Raised when some error occurs during metar downloading or decoding.
  /// Inner exception should contain more accurate information.
  /// </summary>
  [global::System.Serializable]
  public class MetarDownloadException : ApplicationException
  {
    /// <summary>
    /// Initializes a new Instance of MetarDownloader.MetarDownloadException
    /// </summary>
    /// <param name="message"></param>
    public MetarDownloadException(string message) : base(message) { }
    /// <summary>
    /// Initializes a new Instance of MetarDownloader.MetarDownloadException
    /// </summary>
    /// <param name="message"></param>
    /// <param name="inner"></param>
    public MetarDownloadException(string message, Exception inner) : base(message, inner) { }
    /// <summary>
    /// Initializes a new Instance of MetarDownloader.MetarDownloadException
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected MetarDownloadException(
    System.Runtime.Serialization.SerializationInfo info,
    System.Runtime.Serialization.StreamingContext context)
      : base(info, context) { }
  }
}
