using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.Metar.Decoder.Formatters;

namespace ENG.Metar.Decoder
{
  /// <summary>
  /// Represents information about phenoms. E.g. (+RAHZ -SN)
  /// </summary>
  public class PhenomInfo : List<ePhenomCollection>, IMetarItem
  {
    private bool isRE;

    #region Properties

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private bool _IsNSW;
    ///<summary>
    /// Sets/gets if no-significant-weather flag is used (NSW).
    ///</summary>
    public bool IsNSW
    {
      get
      {
        return ((this.Count == 0) && _IsNSW);
      }
      set
      {
        _IsNSW = value;
        if (value)
          this.Clear();
      }
    }

    #endregion Properties

    #region .ctor

    /// <summary>
    /// Creates new instance. Parameter is true if instance is used for re-phenoms. See Metar.RePhenomens.
    /// </summary>
    /// <param name="isRe">True if re-phenoms, false otherwise.</param>
    /// <remarks>
    /// If argument is true, when converted, data are represented in metar with prefix RE.
    /// </remarks>
    public PhenomInfo(bool isRe)
    {
      this.isRE = isRe;
    }
    #endregion .ctor

    #region Inherited

    /// <summary>
    /// Returns item in text string.
    /// </summary>
    /// <param name="verbose">If false, only basic information is returned. If true, all (complex) information is provided.</param>
    /// <returns></returns>
    public string ToInfo(InfoFormatter formatter)
    {
      string ret = null;

      /* PHENOMS-INFO
       * 0 - true when isNSW
       * 1 - true if some phenoms are present
       * 2 - PHENOM-INFO
       * */

      string f = null;
      try
      {
        f = isRE ? formatter.RePhenomsFormat : formatter.PhenomsFormat;
      }
      catch { }
      if (f == null)
        return null;
      else if (f.Length == 0)
        return "";

      ret = formatter.Format(
            f,
            this.IsNSW,
            this.Count != 0,
            GetPhenomInfo(formatter)
            );

      return ret;
    }

    private string GetPhenomInfo(InfoFormatter formatter)
    {
      StringBuilder ret = new StringBuilder();

      this.ForEach(ph => ret.Append(ph.ToInfo(formatter)));

      return ret.ToString();
    }

    /// <summary>
    /// Returns item in metar string.
    /// </summary>
    /// <returns></returns>
    public string ToMetar()
    {
      if (IsNSW)
        return "NSW";
      else
      {
        StringBuilder ret = new StringBuilder();

        this.ForEach(
          i => ret.AppendSpaced(((isRE) ? "RE" : "") + i.ToMetar()));

        return ret.ToString().TrimEnd();
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
      if (IsNSW && (this.Count > 0))
        warnings.Add("When IsNSW flag is set to true, phenom definitions will be ignored (now list is nonempty).");
    }

    #endregion

    #endregion Inherited

  }
}
