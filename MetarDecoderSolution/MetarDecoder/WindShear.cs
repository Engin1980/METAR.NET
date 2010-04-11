using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENG.Metar.Decoder
{
  /// <summary>
  /// Represents windshear information for one runway.
  /// </summary>
  /// <seealso cref="T:ENG.Metar.Decoder.MetarItem"/>
  public class WindShear : IMetarItem
  {
    #region Properties

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
    #endregion Properties

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

      ret.AppendSpaced("windsheer at runway " + Runway);

      return ret.ToString();
    }
#endif //INFO
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

    #endregion Inherited

  }
}
