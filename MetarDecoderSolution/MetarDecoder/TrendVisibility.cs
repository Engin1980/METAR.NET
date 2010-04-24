using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.Metar.Decoder.Formatters;

namespace ENG.Metar.Decoder
{
  /// <summary>
  /// Represents information about trend visibility.
  /// </summary>
  public class TrendVisibility : IMetarItem
  {
    #region Properties

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private bool _UseEUStyle;
    ///<summary>
    /// Sets/gets if to use E-U style in ToMetar method.
    ///</summary>
    public bool UseEUStyle
    {
      get
      {
        return (_UseEUStyle);
      }
      protected set
      {
        _UseEUStyle = value;
      }
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private bool _IsDevicesMinimumValue;
    ///<summary>
    /// Sets/gets if measured distance is equipments minimal measurable value.
    ///</summary>
    public bool IsDevicesMinimumValue
    {
      get
      {
        return (_IsDevicesMinimumValue);
      }
      protected set
      {
        _IsDevicesMinimumValue = value;
      }
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private bool _IsClear;
    ///<summary>
    /// Sets/gets clear visibility value. If true, most of other properties are omitted.
    ///</summary>
    public bool IsClear
    {
      get
      {
        return (_IsClear);
      }
      protected set
      {
        _IsClear = value;
      }
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private Racional? _Distance;
    ///<summary>
    /// Sets/gets distance value.
    ///</summary>
    public Racional? Distance
    {
      get
      {
        return (_Distance);
      }
      protected set
      {
        _Distance = value;
      }
    }

    #endregion Properties

    #region Methods
    /// <summary>
    /// Sets "cloud and visibility ok" (CAVOK) weather (that is clear sky in EU style).
    /// </summary>
    public void SetCAVOK()
    {
      SetClear(false);
    }
    /// <summary>
    /// Sets "sky clear" (SKC) weather (that is clear sky in US style).
    /// </summary>
    public void SetSKC()
    {
      SetClear(true);
    }
    /// <summary>
    /// Sets clear sky. Parameter define if to use EU style (US otherwise).
    /// </summary>
    /// <param name="useEUStyle">True if EU style to use, false otherwise (that is US style).</param>
    public void SetClear(bool useEUStyle)
    {
      IsClear = true;
      UseEUStyle = useEUStyle;
    }
    /// <summary>
    /// Set distance in meters. Sets EU style.
    /// </summary>
    /// <param name="distance">Visibility distance.</param>
    public virtual void SetMeters(int distance)
    {
      UseEUStyle = true;
      Distance = distance;
      IsDevicesMinimumValue = false;
    }
    /// <summary>
    /// Sets visibility distance in miles. Sets US style (non EU style).
    /// </summary>
    /// <param name="distance">Distance</param>
    /// <param name="isDevicesMinimumValue">True if value is minimum of measuring equipment.</param>
    public void SetMiles(Racional distance, bool isDevicesMinimumValue)
    {
      IsDevicesMinimumValue = isDevicesMinimumValue;
      UseEUStyle = false;
      Distance = distance;
    }
    /// <summary>
    /// Sets visibility distance in miles. Sets US style (non EU style).
    /// </summary>
    /// <param name="distance">Distance</param>
    /// <param name="isDevicesMinimumValue">True if value is minimum of measuring equipment.</param>
    public void SetMiles(int distance, bool isDevicesMinimumValue)
    {
      SetMiles((Racional)distance, isDevicesMinimumValue);
    }

    #endregion Methods

    #region Inherited

    /// <summary>
    /// Returns item in text string.
    /// </summary>
    /// <param name="formatter">If false, only basic information is returned. If true, all (complex) information is provided.</param>
    /// <returns></returns>
    public virtual string ToInfo(InfoFormatter formatter)
    {
      string ret = "";

      /* VISIBILITY
      * 0 - isClear
      * 1 - distanceOrNull
      * 2 - distance unit
      * 3 - distance unit long
      * 4 - distance direction (if any), or null; not used at trend visibility (= is null)
      * 5 - not used
      * 6 - true if it is minimum measurable distance, or false
      * 7 - other distance if used, or null; not used at trend visibility (= is null)
      * 8 - other distance direction if other distance used, or null; not used at trend visibility (= is null)
      * 9 - true if runwayVisibility definitions is present, false otherwise; not used at trend visibility (= is null)
      * 10 - (iter) RUNWAY-VISIBILITY; not used at trend visibility (= is null)
      * */

      string f = null;
      try
      {
        f = formatter.VisibilityFormat;
      }
      catch { }
      if (f == null)
        return null;
      else if (f.Length == 0)
        return "";

      ret = formatter.Format(
        formatter.VisibilityFormat,
        this.IsClear, //0
        this.Distance.HasValue ? this.Distance.Value.ToString(false) : null,
  this.UseEUStyle ?
    formatter.eUnitToString(Common.eUnit.m, false) : formatter.eUnitToString(Common.eUnit.mi, false),
  this.UseEUStyle ?
    formatter.eUnitToString(Common.eUnit.m, true) : formatter.eUnitToString(Common.eUnit.mi, true),
        null,
        null,
        this.IsDevicesMinimumValue, // 6
        null,
        null
        , null
        , null
        );

      return ret;
    }

    /// <summary>
    /// Returns item in metar string.
    /// </summary>
    /// <returns></returns>
    public virtual string ToMetar()
    {
      StringBuilder ret = new StringBuilder();

      if (IsClear)
      {
        if (UseEUStyle)
          ret.Append("SKC");
        else
          ret.Append("CAVOK");
      }
      else if (UseEUStyle)
      {
        ret.Append(((int)Distance.Value).ToString("0000"));
      }
      else
      {
        if (IsDevicesMinimumValue)
          ret.Append("M");
        ret.Append(Distance.Value.ToString(false) + "SM");
      }

      return ret.ToString();
    }

    /// <summary>
    /// Returns a <see cref="T:System.String"/> that represents the current instance.
    /// </summary>
    /// <returns>A <see cref="T:System.String"/> that represents the current instance.</returns>
    public override string ToString()
    {
      return ESystem.Extensions.ObjectExt.ToInlineInfoString(this);
    }

    /// <summary>
    /// Proceed sanity check of inserted values.
    /// </summary>
    /// <param name="errors">Found errors.</param>
    /// <param name="warnings">Found warnings.</param>
    public virtual void SanityCheck(ref List<string> errors, ref List<string> warnings)
    {
      if (UseEUStyle && Distance.HasValue && (Distance.Value > 10000))
        errors.Add("Maximum value for EU distance is 9999 meters. If more, use CAVOK instead.");
      else if (!UseEUStyle && Distance.HasValue && (Distance.Value > 10))
        errors.Add("Maximum value for non-EU (USA) distance is 10 miles. If more, use SKC instead.");

      if (UseEUStyle && IsDevicesMinimumValue)
        warnings.Add("IsDeviceMinimumValue flag is not used in EU style and will be ignored.");
    }

    #endregion Inherited

  }
}
