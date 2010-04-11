using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENG.Metar.Decoder
{
  /// <summary>
  /// Represents set of time infos in metar trend.
  /// </summary>
  public class TrendTimeInfo : List<TrendTime>, IMetarItem
  {
    #region Inherited

#if INFO
   /// <summary>
    /// Returns item in text string.
    /// </summary>
    /// <param name="verbose">If false, only basic information is returned. If true, all (complex) information is provided.</param>
    /// <returns></returns>
public string ToInfo(bool verbose)
    {
      StringBuilder ret = new StringBuilder();

      this.ForEach(i => ret.AppendSpaced(i.ToInfo(verbose)));

      return ret.ToString();
    }
#endif //INFO

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
      if (this.Count > 2)
        warnings.Add("Expected one value (at) or two values (from-to) in collection.");

      if (this.Count == 2)
      {
        if ((this[0].Type != TrendTime.eType.FM) || (this[1].Type != TrendTime.eType.TL))
          warnings.Add("For two types expected pair from-to.");
      }
    }

    #endregion

    #endregion Inherited
  }
}
