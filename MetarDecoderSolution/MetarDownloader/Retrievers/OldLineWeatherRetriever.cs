using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ENG.Metar.Downloader.Retrievers
{
  /// <summary>
  /// This class is able to download metar from web OldLineWeather.
  /// </summary>
  /// <seealso cref="T:ENG.Metar.Downloader.IMetarRetrieve"/>
  public class OldLineWeatherRetriever : IMetarRetrieve
  {
    #region IMetarRetrieve Members

    /// <summary>
    /// Returns URL where METAR information is stored.
    /// </summary>
    /// <param name="icao">ICAO code of airport/station.</param>
    /// <returns></returns>
    public string GetUrlForICAO(string icao)
    {
      return "http://www.oldlineweather.com/wxmetar.php?station=" + icao;
    }

    /// <summary>
    /// Decodes metar from stream. Stream should be downloaded from URL address obtained 
    /// from GetUrlForICAO() method. <seealso cref="GetUrlForICAO"/>.
    /// </summary>
    /// <param name="sourceStream">Source stream, from which the metar will be obtained.</param>
    /// <returns>Metar string.</returns>
    /// <exception cref="MetarDownloadException">Returns if anything fails. Inner exception should contain more accurate info.</exception>
    public string DecodeMetar(System.IO.Stream sourceStream)
    {
      string ret = null;

      System.IO.StreamReader rdr = new System.IO.StreamReader(sourceStream);
      string pom = rdr.ReadToEnd();
      rdr = null;

      string rgx = @"METAR = (([A-Z]|[0-9]|/| )+)";

      Match m = Regex.Match(pom, rgx);
      if (m.Success)
        ret = m.Groups[1].Value;
      else
        throw new MetarDownloadException("Unable to decode information from page. Incorrect ICAO?");

      ret = "METAR " + ret;

      return ret;
    }

    #endregion
  }
}
