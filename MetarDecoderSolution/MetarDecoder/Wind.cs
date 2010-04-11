using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESystem;

namespace ENG.Metar.Decoder
{
  /// <summary>
  /// Represents information about wind.
  /// </summary>
  /// <seealso cref="T:ENG.Metar.Decoder.MetarItem"/>
  public class Wind : IMetarItem
  {
    #region Nested
    /// <summary>
    /// Wind-speed unit
    /// </summary>
    public enum eUnit
    {
      /// <summary>
      /// Metres per second (used e.g. in Russia)
      /// </summary>
      MPS,
      /// <summary>
      /// Knots (most common)
      /// </summary>
      KT,
      /// <summary>
      /// Kilometers per hour
      /// </summary>
      KMH
    }
    #endregion Nested

    #region Properties

    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private eUnit _Unit = eUnit.KT;
    ///<summary>
    /// Sets/gets unit of wind speed.
    ///</summary>
    public eUnit Unit
    {
      get
      {
        return (_Unit);
      }
      set
      {
        _Unit = value;
      }
    }

    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private int? _Direction = 220;
    ///<summary>
    /// Sets/gets wind direction value.
    ///</summary>
    public int? Direction
    {
      get
      {
        return (_Direction);
      }
      set
      {
        if ((value.HasValue) && (!value.Value.IsBetween (0,360)))
          throw new ArgumentOutOfRangeException ("Value must be between 0 and 360.");
        _Direction = value;
      }
    }

    ///<summary>
    /// Sets/gets true if wind si variable (VRB). Do not confuse with wind variability!
    ///</summary>
    public bool IsVariable
    {
      get
      {
        return (Direction == null);
      }
      set
      {
        if (value)
          Direction = null;
        else
          throw new ArgumentException("To unset variable wind insert value (heading) into Direction property.");
      }
    }

    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private NonNegInt _Speed = 3;
    ///<summary>
    /// Sets/gets wind speed.
    ///</summary>
    public NonNegInt Speed
    {
      get
      {
        return (_Speed);
      }
      set
      {
        _Speed = value;
      }
    }

    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private NonNegInt? _GustSpeed = null;
    ///<summary>
    /// Sets/gets GustSpeed value. Null if no gusts defined.
    ///</summary>
    public NonNegInt? GustSpeed
    {
      get
      {
        return (_GustSpeed);
      }
      set
      {
        _GustSpeed = value;
      }
    }

    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private WindVariable _Variability = null;
    ///<summary>
    /// Sets/gets variable value. Null if not used.
    ///</summary>
    public WindVariable Variability
    {
      get
      {
        return (_Variability);
      }
      set
      {
        _Variability = value;
      }
    }

    /// <summary>
    /// Returns true if wind is varying between two headings..
    /// </summary>
    /// <value></value>
    public bool IsVarying
    {
      get
      {
        return (Variability != null);
      }
    }
    #endregion Properties

#if INFO
    /// <summary>
    /// Returns item in text string.
    /// </summary>
    /// <param name="verbose">If false, only basic information is returned. If true, all (complex) information is provided.</param>
    /// <returns></returns>
    public string ToInfo(InfoFormatter formatter)
    {
      /* 
     * WIND:
     * 0 - direction
     * 1 - direction as N/NE/...
     * 2 - wind speed
     * 3 - wind speed unit
     * 4 - wind-gust-speed, if none, wind speed is used
     * 5 - WIND-VARIES
     * */

      StringBuilder ret = new StringBuilder();

      string s = (GustSpeed.HasValue ? formatter.WindGustingFormat : formatter.WindFormat);

      ret.AppendFormat(
        s,
        Direction,
        Direction.HasValue ? Common.HeadingToString(Direction.Value) : "",
        Speed,
        Unit.ToString(),
        GustSpeed.HasValue ? GustSpeed.Value : Speed,
        IsVarying ? Variability.ToInfo (formatter) : "");

      return ret.ToString();
    }
#endif //INFO
    #region Inherits

    /// <summary>
    /// Returns item in metar string.
    /// </summary>
    /// <returns></returns>
    public string ToMetar()
    {
      StringBuilder ret = new StringBuilder();

      if (IsVariable)
        ret.Append("VRB");
      else
        ret.Append(Direction.Value.ToString("000"));
      ret.Append(Speed.ToString("00"));
      if (GustSpeed.HasValue)
        ret.Append("G" + GustSpeed.Value.ToString("00"));
      ret.Append(Unit.ToString());

      if (IsVarying)
      {
        ret.Append(" " + Variability.ToMetar());
      }

      return ret.ToString();
    }

    /// <summary>
    /// Proceed sanity check of inserted values.
    /// </summary>
    /// <param name="errors">Found errors.</param>
    /// <param name="warnings">Found warnings.</param>
    public void SanityCheck(ref List<string> errors, ref List<string> warnings)
    {
      if (GustSpeed.HasValue)
      {
        if (GustSpeed.Value < Speed)
          errors.Add("Wind gust speed is less than wind speed.");
        if (GustSpeed.Value == Speed)
          warnings.Add("Wind gust speed is equal to wind speed.");
      }

      if (Variability != null)
        Variability.SanityCheck(ref errors, ref warnings);
    }

    #endregion Inherits

  }
}
