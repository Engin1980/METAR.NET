using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENG.WMOCodes.Downloaders
{
  /// <summary>
  /// Raised when some error occurs during metar downloading or decoding.
  /// Inner exception should contain more accurate information.
  /// </summary>
#if SILVERLIGHT == false
  [global::System.Serializable]
#endif
  public class DownloadException : Exception
  {
    public DownloadException(string message) : base(message) { }

    public DownloadException(string message, Exception inner) : base(message, inner) { }

#if SILVERLIGHT == FALSE

    protected DownloadException(
    System.Runtime.Serialization.SerializationInfo info,
    System.Runtime.Serialization.StreamingContext context)
      : base(info, context) { }

#endif
  }
}
