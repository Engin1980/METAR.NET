﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.Metar.Decoder.Formatters;

namespace ENG.Metar.Decoder
{
  /// <summary>
  /// Represents information about clouds.
  /// </summary>
  public class TrendCloudInfo : List<Cloud>, ICodeItem
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
      _IsSKC = true;
      _IsNSC = false;
      _IsVerticalVisibility = false;
      this.Clear();
    }

    /// <summary>
    /// Sets "no significand cloud". <see cref="IsNSC"/>
    /// </summary>
    public void SetNSC()
    {
      _IsSKC = false;
      _IsNSC = true;
      _IsVerticalVisibility = false;
      this.Clear();
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
      this.Clear();
    }

    #endregion Methods

    #region Implemented

    /// <summary>
    /// Returns item in text string.
    /// </summary>
    /// <param name="formatter">Formatter used to format string.</param>
    /// <returns></returns>
    public string ToInfo(InfoFormatter formatter)
    {
      string ret = null;

      string f = null;
      try
      {
        f = formatter.CloudsFormat;
      }
      catch { }
      if (f == null)
        return null;
      else if (f.Length == 0)
        return "";

      ret = formatter.Format(
        formatter.CloudsFormat,
        this.IsNSC,
        this.IsSKC,
        this.VVDistance,
        (IsNSC || IsSKC || IsVerticalVisibility || this.Count == 0) ? null : GetCloudInfos(formatter)
        );

      return ret;
    }

    private object GetCloudInfos(InfoFormatter formatter)
    {
      StringBuilder ret = new StringBuilder();

      this.ForEach(c => ret.Append(c.ToInfo(formatter)));

      return ret;
    }

    /// <summary>
    /// Returns item in code string.
    /// </summary>
    /// <returns></returns>
    public string ToCode()
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
          i => ret.AppendSpaced(i.ToCode()));

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
    public void SanityCheck(ref List<string> errors, ref List<string> warnings)
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