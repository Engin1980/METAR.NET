using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENG.WMOCodes.Downloaders.Retrievers.TAF
{
  public class NoaaGovRetriever : IRetriever
  {
    #region IRetriever Members

    private const string url = @"http://weather.noaa.gov/pub/data/forecasts/taf/stations/";
    public string GetUrlForICAO(string icao)
    {
      return url + icao.ToUpper() + ".TXT";
    }

    public string DecodeWMOCode(System.IO.Stream sourceStream)
    {
      StringBuilder ret = new StringBuilder();

      System.IO.StreamReader rdr = new System.IO.StreamReader(sourceStream);
      rdr.ReadLine();
      string line = rdr.ReadLine();
      while (line != null)
      {
        ret.Append(line + " ");
        line = rdr.ReadLine();
      }

      return ret.ToString();
    }

    #endregion
  }
}
