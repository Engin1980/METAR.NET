using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetarDecoder
{
  /// <summary>
  /// Represents information about windshears in metar.
  /// </summary>
  public class WindShearInfo : List<WindShear>, MetarItem
  {
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private bool _IsAllRunways;
    ///<summary>
    /// Sets/gets if winshear warning is true for all runways (WS ALL RWY). If so, collection data are ignored.
    ///</summary>
    public bool IsAllRunways
    {
      get
      {
        return (_IsAllRunways);
      }
      set
      {
        _IsAllRunways = value;
      }
    }

    /// <summary>
    /// Returns item in metar string.
    /// </summary>
    /// <returns></returns>
    public string ToMetar()
    {
      if (IsAllRunways)
        return "WS ALL RWY";
      else
      {
        StringBuilder b = new StringBuilder();        

        foreach (var fItem in this)
        {
          b.AppendSpaced(fItem.ToMetar());
        } // foreach (var fItem in WindShears)

        if (b.Length > 0)
          return "WS " + b.ToString().TrimEnd();
        else
          return "";
      }
    }

    #region MetarItem Members


    /// <summary>
    /// Proceed sanity check of inserted values.
    /// </summary>
    /// <param name="errors">Found errors.</param>
    /// <param name="warnings">Found warnings.</param>
    public void SanityCheck(ref List<string> errors, ref List<string> warnings)
    {
      if (IsAllRunways && (this.Count > 0))
        warnings.Add("If IsAllRunways flag is set to true, ws definitions for concrete runways will be ignored (now are non-empty).");
    }

    #endregion
  }
}
