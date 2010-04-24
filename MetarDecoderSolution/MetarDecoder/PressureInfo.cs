using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESystem.Extensions;
using ENG.Metar.Decoder.Formatters;

namespace ENG.Metar.Decoder
{
  /// <summary>
  /// Represents pressure info.
  /// </summary>
  public class PressureInfo : IMetarItem
  {
    #region Nested

    /// <summary>
    /// Pressure unit.
    /// </summary>
    public enum eUnit
    {
      /// <summary>
      /// Hectopascals. E.g. for Q1013
      /// </summary>
      hPa,
      /// <summary>
      /// Milimeters of Hq. E.g. for A29.92
      /// </summary>
      mmHq
    }

    #endregion Nested

    #region Properties

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private eUnit _Unit = eUnit.hPa;
    ///<summary>
    /// Sets/gets Unit value.
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

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private double _Value = 1013;
    ///<summary>
    /// Sets/gets pressure value.
    ///</summary>
    public double Value
    {
      get
      {
        return (_Value);
      }
    }

    ///<summary>
    /// Sets/gets QNH value.
    ///</summary>
    public int QNH
    {
      get
      {
        return ((int)Value);
      }
      set
      {
        _Value = value;
      }
    }
    ///<summary>
    /// Sets/gets mmHq value.
    ///</summary>
    public double mmHq
    {
      get
      {
        return (Value / 33.86);
      }
      set
      {
        _Value = value * 33.86;
      }
    }

    #endregion Properties

    #region Methods

    /// <summary>
    /// Sets pressure value.
    /// </summary>
    /// <param name="value">Pressure value.</param>
    /// <param name="unit">Pressure unit.</param>
    public void Set(double value, eUnit unit)
    {
      Unit = unit;
      if (value <= 0)
        throw new ArgumentException("Pressure value cannot be less or equal 0.", "value");
      if (unit == eUnit.hPa)
        QNH = (int)value;
      else
        mmHq = value;
    }

    #endregion Methods

    #region Inherited
    /// <summary>
    /// Returns item in text string.
    /// </summary>
    /// <param name="formatter">Formatter used to format string.</param>
    /// <returns></returns>
    public string ToInfo(InfoFormatter formatter)
    {
      string ret = null;

      /* PRESSURE
       * 0 - value in mmHq
       * 1 - value in hPa
       * 2 - value in current unit
       * 3 - current unit (short)
       * 4 - current unit (long)
       * */

      string f = null;
      try
      {
        f = formatter.PressureFormat;
      }
      catch { }
      if (f == null)
        return null;
      else if (f.Length == 0)
        return "";

      ret = formatter.Format(
            formatter.PressureFormat,
            this.mmHq,
            this.QNH,
            (this.Unit == eUnit.hPa) ? this.QNH : this.mmHq,
            formatter.PressureInfoUnitToString (this.Unit, false),
            formatter.PressureInfoUnitToString (this.Unit, true)
            );

      return ret;
    }

    /// <summary>
    /// Returns item in metar string.
    /// </summary>
    /// <returns></returns>
    public string ToMetar()
    {
      StringBuilder ret = new StringBuilder();

      if (Unit == eUnit.hPa)
        ret.Append("Q" + QNH.ToString("0000"));
      else
        ret.Append(
          "A" + (mmHq * 100).ToString("0000"));

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

    #region MetarItem Members


    /// <summary>
    /// Proceed sanity check of inserted values.
    /// </summary>
    /// <param name="errors">Found errors.</param>
    /// <param name="warnings">Found warnings.</param>
    public void SanityCheck(ref List<string> errors, ref List<string> warnings)
    {
      if (!Value.IsBetween(800, 1100))
      {
        if (Unit == eUnit.hPa)
          warnings.Add("Current pressure value in hPA is not probably correct (" + Value.ToString() + ")");
        else if (Unit == eUnit.mmHq)
          warnings.Add("Current pressure value in mmHq is not probably correct (" + this.mmHq.ToString() + ")");
      }
    }

    #endregion

    #endregion Inherited

  }
}
