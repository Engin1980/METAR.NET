﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.Metar.Decoder.Formatters;

namespace ENG.Metar.Decoder
{
  /// <summary>
  /// Represets sets of runway conditions.
  /// </summary>
  public class RunwayConditionInfo : List<RunwayCondition>, IMetarItem
  {
    #region Properties

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

      if (IsSNOCLO)
        ret.AppendSpaced("Closed due to snow.");
      else
      {
        if (this.Count > 0)
        {
          ret.AppendSpaced("Runway conditions:");

          this.ForEach(i => ret.AppendSpaced(i.ToInfo(verbose)));

          ret[ret.Length - 2] = '.';
        }
      }

      return ret.ToString();
    }
#endif //INFO

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
    public string ToInfo(InfoFormatter formatter)
    {
      string ret = null;

      /* RUNWAYS CONDITIONS INFO
       * 0 - true if SNOCLO, or false
       * 1 - true if runway condition list is non-empty, or false
       * 2 - RUNWAY CONDITION INFO
       * */

      string f = null;
      try
      {
        f = formatter.RunwayConditionsFormat;
      }
      catch { }
      if (f == null)
        return null;
      else if (f.Length == 0)
        return "";

      ret = formatter.Format(
        formatter.RunwayConditionsFormat,
        this.IsSNOCLO,
        this.Count != 0,
        GetRunwayConditionInfo(formatter)
        );

      return ret;
    }

    private object GetRunwayConditionInfo(InfoFormatter formatter)
    {
      StringBuilder ret = new StringBuilder();

      this.ForEach(r => ret.Append(r.ToInfo(formatter)));

      return ret;
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

    #endregion Inherited

  }
}
