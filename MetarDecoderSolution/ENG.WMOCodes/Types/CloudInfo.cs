using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Types.Basic;
using ESystem.Extensions;

namespace ENG.WMOCodes.Types
{
  /// <summary>
  /// Represents information about clouds.
  /// </summary>
  public class CloudInfo : List<Cloud>, ICodeItem
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
        return ((this.Count == 0) && _IsSKC);
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
        return ((this.Count == 0) && _IsNSC);
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
        return ((this.Count == 0) && _IsVerticalVisibility);
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
      SetAllFlagsOff();
      _IsSKC = true;
    }

    /// <summary>
    /// Sets "no significand cloud". <see cref="IsNSC"/>
    /// </summary>
    public void SetNSC()
    {
      SetAllFlagsOff();
      _IsNSC = true;
    }

    /// <summary>
    /// Sets vertical visibility. Argument is null if not known (that is VV///).
    /// </summary>
    /// <param name="distance">Distance in hounded feet. Null if not known.</param>
    public void SetVerticalVisibility(int? distance)
    {
      SetAllFlagsOff();
      _IsVerticalVisibility = true;
      _VVDistance = distance;
    }

    /// <summary>
    /// Sets all flags (VV, NSC, etc.) off.
    /// </summary>
    protected virtual void SetAllFlagsOff()
    {
      _IsSKC = false;
      _IsNSC = false;
      _IsVerticalVisibility = false;
      _VVDistance = null;
      this.Clear();
    }

    #endregion Methods

    #region Implemented

    /// <summary>
    /// Returns item in code string.
    /// </summary>
    /// <returns></returns>
    public virtual string ToCode()
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
          i => ret.AppendPreSpaced(i.ToCode()));

        return ret.ToString().TrimEnd();
      }
    }

    /// <summary>
    /// Returns a <see cref="T:System.String"/> that represents the current instance.
    /// </summary>
    /// <returns>A <see cref="T:System.String"/> that represents the current instance.</returns>
    public override string ToString()
    {
      return ESystem.Extensions.ObjectExt.ToInlineInfoString(this);
    }

    #region MetarItem Members

    /// <summary>
    /// Proceed sanity check of inserted values.
    /// </summary>
    /// <param name="errors">Found errors.</param>
    /// <param name="warnings">Found warnings.</param>
    public virtual void SanityCheck(ref List<string> errors, ref List<string> warnings)
    {
      if (IsSKC && IsNSC)
        errors.Add("Both IsSKC and IsNSC cannot be set at one time.");
      if ((IsSKC || IsNSC) && IsVerticalVisibility)
        errors.Add("Vertical visibility cannot be set true with IsSKC or IsNSC flags.");
      if ((IsSKC || IsNSC || IsVerticalVisibility) && (Count > 0))
        warnings.Add("When one of flags IsSKC, IsNSC or IsVerticalVisibility are set to true, cloud defining content (wich is now not empty) will be ignored.");
    }

    #endregion

    #endregion Implemented


    internal bool IsEmpty()
    {
      return (!IsNSC && !IsSKC && !IsVerticalVisibility && this.Count == 0);
    }
  }
}
