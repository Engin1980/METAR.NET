using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENG.Metar.Decoder
{
  /// <summary>
  /// Represets sets of runway conditions.
  /// </summary>
  public class RunwayConditionInfo : List<RunwayCondition>, MetarItem
  {
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private bool _IsSNOCLO;
    ///<summary>
    /// Sets/gets if airport is closed due to snow (that is SNOCLO in metar).
    ///</summary>
    public bool IsSNOCLO
    {
      get
      {
        return (_IsSNOCLO);
      }
      set
      {
        _IsSNOCLO = value;
      }
    }

    /// <summary>
    /// Returns item in metar string.
    /// </summary>
    /// <returns></returns>
    public string ToMetar()
    {
      if (IsSNOCLO)
        return "SNOCLO";
      else
      {
        StringBuilder ret = new StringBuilder();

        foreach (var fItem in this)
        {
          ret.AppendSpaced(fItem.ToMetar());
        } // foreach (var fItem in RunwayConditions)

        return ret.ToString().TrimEnd();
      }
    }

    #region MetarItem Members


    /// <summary>
    /// Returns item in text string.
    /// </summary>
    /// <returns></returns>
    public string ToInfo()
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Proceed sanity check of inserted values.
    /// </summary>
    /// <param name="errors">Found errors.</param>
    /// <param name="warnings">Found warnings.</param>
    public void SanityCheck(ref List<string> errors, ref List<string> warnings)
    {
      foreach (var fItem in this)
      {
        fItem.SanityCheck(ref errors, ref warnings);
      } // foreach (var fItem in this)
    }

    #endregion
  }
}
