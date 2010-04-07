using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetarDecoder
{
  /// <summary>
  /// Represents windshear information for one runway.
  /// </summary>
  /// <seealso cref="T:MetarDecoder.MetarItem"/>
  public class WindShear : MetarItem
  {
    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private string _Runway;
    ///<summary>
    /// Sets/gets runway designator.
    ///</summary>
    public string Runway
    {
      get
      {
        return (_Runway);
      }
      set
      {
        _Runway = value;
      }
    }

    /// <summary>
    /// Returns item in metar string.
    /// </summary>
    /// <returns></returns>
    public string ToMetar()
    {
      return("RWY" + Runway);
    }

    #region MetarItem Members


    /// <summary>
    /// Proceed sanity check of inserted values.
    /// </summary>
    /// <param name="errors">Found errors.</param>
    /// <param name="warnings">Found warnings.</param>
    public void SanityCheck(ref List<string> errors, ref List<string> warnings)
    {
      if (string.IsNullOrEmpty(Runway))
        errors.Add("Runway name/sign is not set.");
    }

    #endregion
  }
}
