using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENG.Metar.Downloader
{
  /// <summary>
  /// Represents type able to define source for metar downloading.
  /// </summary>
  public interface IMetarRetriever
  {
    /// <summary>
    /// Returns URL where METAR information is stored.
    /// </summary>
    /// <param name="icao">ICAO code of airport/station.</param>
    /// <returns></returns>
    string GetUrlForICAO(string icao);

    /// <summary>
    /// Decodes metar from stream. Stream should be downloaded from URL address obtained 
    /// from GetUrlForICAO() method. <seealso cref="GetUrlForICAO"/>.
    /// </summary>
    /// <param name="sourceStream">Source stream, from which the metar will be obtained.</param>
    /// <returns>Metar string.</returns>
    /// <exception cref="MetarDownloadException">
    /// Returns if anything fails. Inner exception should contain more accurate info.
    /// </exception>
    string DecodeMetar(System.IO.Stream sourceStream);
  }
}
