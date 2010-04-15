using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENG.Metar.Decoder
{
  /// <summary>
  /// Represents information about clouds.
  /// </summary>
  public class CloudInfo : List<Cloud>, IMetarItem
  {
    #region Properties

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private bool _IsSKC = true;
    ///<summary>
    /// Gets "is sky clear" value. That is SKC in metar.
    ///</summary>
    public bool IsSKC
    {
      get
      {
        return (_IsSKC);
      }
    }
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private bool _IsNSC = false;
    ///<summary>
    /// Gets "no significant cloud" value. That is NSC in metar.
    ///</summary>
    public bool IsNSC
    {
      get
      {
        return (_IsNSC);
      }
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private bool _IsVerticalVisibility = false;
    ///<summary>
    /// Gets if cloud info is represented by vertical visibility. E.g. VV040 in metar.
    ///</summary>
    public bool IsVerticalVisibility
    {
      get
      {
        return (_IsVerticalVisibility);
      }
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private int? _VVDistance = null;
    ///<summary>
    /// Gets vertical visibility distance (in houndreds of feet). E.g. VV040. 
    /// If not known, value is null. That is VV/// in metar.
    ///</summary>
    public int? VVDistance
    {
      get
      {
        return (_VVDistance);
      }
    }

    #endregion Properties

    #region Methods


    /// <summary>
    /// Sets "sky clear". <see cref="IsSKC"/>
    /// </summary>
    public void SetSKC()
    {
      _IsSKC = true;
      _IsNSC = false;
      _IsVerticalVisibility = false;
    }

    /// <summary>
    /// Sets "no significand cloud". <see cref="IsNSC"/>
    /// </summary>
    public void SetNSC()
    {
      _IsSKC = false;
      _IsNSC = true;
      _IsVerticalVisibility = false;
    }

    /// <summary>
    /// Sets vertical visibility. Argument is null if not known (that is VV///).
    /// </summary>
    /// <param name="distance">Distance in hounded feet. Null if not known.</param>
    public void SetVerticalVisibility(int? distance)
    {
      _IsSKC = false;
      _IsNSC = false;
      _IsVerticalVisibility = true;
      _VVDistance = distance;
    }

    #endregion Methods

    #region Implemented

#if INFO
    /// <summary>
    /// Returns item in text string.
    /// </summary>
    /// <param name="verbose">If false, only basic information is returned. If true, all (complex) information is provided.</param>
    /// <returns></returns>
    public string ToInfo(bool verbose)
    {
      StringBuilder ret = new StringBuilder();

      if (this.IsNSC)
        ret.AppendSpaced("No significant clouds.");
      else if (this.IsSKC)
        ret.AppendSpaced("Sky clear.");
      else if (this.IsVerticalVisibility)
      {
        ret.AppendSpaced("Vertical visibility");
        if (VVDistance.HasValue)
          ret.AppendSpaced ((VVDistance.Value*100).ToString() + " ft.");
        else
          ret.AppendSpaced (" unknown.");
        }
      else
      {
        if (this.Count > 0)
        {
        ret.AppendSpaced("Clouds: ");
        if (verbose)
          ret.AppendSpaced(this[0].ToInfo(verbose));
        else
        {
          this.ForEach(i => ret.AppendSpaced(i.ToInfo(verbose)));
          ret[ret.Length-2] = '.';
        }
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
      if (IsSKC)
        return "SKC";
      else if (IsNSC)
        return "NSC";
      else if (IsVerticalVisibility)
        return "VV" + (VVDistance.HasValue ? VVDistance.Value.ToString("000") : "///");
      else
      {
        StringBuilder ret = new StringBuilder();

        this.ForEach( 
          i => ret.AppendSpaced(i.ToMetar()));

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
      if (IsSKC && IsNSC)
        errors.Add("Both IsSKC and IsNSC cannot be set at one time.");
      if ((IsSKC || IsNSC) && IsVerticalVisibility)
        errors.Add("Vertical visibility cannot be set true with IsSKC or IsNSC flags.");
      if ((IsSKC || IsNSC || IsVerticalVisibility) && (Count > 0))
        warnings.Add ("When one of flags IsSKC, IsNSC or IsVerticalVisibility are set to true, cloud defining content (wich is now not empty) will be ignored.");
    }

    #endregion

    #endregion Implemented

  }
}
