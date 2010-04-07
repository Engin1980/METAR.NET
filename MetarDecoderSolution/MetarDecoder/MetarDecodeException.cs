using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENG.Metar.Decoder
{
  /// <summary>
  /// Raised when error captured during metar decode process.
  /// </summary>
  public class MetarDecodeException : Exception
  {
    /// <summary>
    /// Initializes a new Instance of ENG.Metar.Decoder.MetarDecodeException
    /// </summary>
    /// <param name="metar"></param>
    /// <param name="error"></param>
    public MetarDecodeException(string metar, string error) 
      :this (metar, error, null) {}
    /// <summary>
    /// Initializes a new Instance of ENG.Metar.Decoder.MetarDecodeException
    /// </summary>
    /// <param name="metar"></param>
    /// <param name="error"></param>
    /// <param name="innerException"></param>
    public MetarDecodeException(string metar, string error, Exception innerException)
      : base("Retrieving metar from string " + metar + " failed. Reason: " + error, innerException) {}
  }
}
