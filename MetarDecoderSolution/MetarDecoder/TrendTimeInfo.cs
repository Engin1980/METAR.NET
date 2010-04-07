using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetarDecoder
{
  /// <summary>
  /// Represents set of time infos in metar trend.
  /// </summary>
  public class TrendTimeInfo : List<TrendTime>, MetarItem
  {
    /// <summary>
    /// Returns item in metar string.
    /// </summary>
    /// <returns></returns>
    public string ToMetar()
    {
      StringBuilder ret = new StringBuilder();

      this.ForEach(i => ret.AppendSpaced(i.ToMetar()));

      return ret.ToString().TrimEnd();
    }

    #region MetarItem Members


    /// <summary>
    /// Proceed sanity check of inserted values.
    /// </summary>
    /// <param name="errors">Found errors.</param>
    /// <param name="warnings">Found warnings.</param>
    public void SanityCheck(ref List<string> errors, ref List<string> warnings)
    {
#warning TODO Dokončit ověření na FM/TL/AT
    }

    #endregion
  }
}
