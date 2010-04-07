using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESystem;

namespace ENG.Metar.Decoder
{

  /// <summary>
  /// Represents trend time information.
  /// </summary>
  /// <seealso cref="T:ENG.Metar.Decoder.MetarItem"/>
  public class TrendTime : MetarItem
  {
    /// <summary>
    /// Type of trend time.
    /// </summary>
    public enum eType
    {
      /// <summary>
      /// From date/time
      /// </summary>
      FM,
      /// <summary>
      /// Until date/time
      /// </summary>
      TL,
      /// <summary>
      /// At date/time
      /// </summary>
      AT
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private eType _Type;
    ///<summary>
    /// Sets/gets time type.
    ///</summary>
    public eType Type
    {
      get
      {
        return (_Type);
      }
      set
      {
        _Type = value;
      }
    }
    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private int _Hour;
    ///<summary>
    /// Sets/gets Hour value.
    ///</summary>
    public int Hour
    {
      get
      {
        return (_Hour);
      }
      set
      {
        if (!value.IsBetween(0, 24))
          throw new ArgumentOutOfRangeException("Value must be between 0 and 24.");
        _Hour = value;
      }
    }
    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private int _Minute;
    ///<summary>
    /// Sets/gets Minute value.
    ///</summary>
    public int Minute
    {
      get
      {
        return (_Minute);
      }
      set
      {
        if (!value.IsBetween(0, 59))
          throw new ArgumentOutOfRangeException("Value must be between 0 and 59.");
        _Minute = value;
      }
    }

    /// <summary>
    /// Returns item in metar string.
    /// </summary>
    /// <returns></returns>
    public string ToMetar()
    {
      return Type.ToString() + Hour.ToString("00") + Minute.ToString("00");
    }

    #region MetarItem Members


    /// <summary>
    /// Proceed sanity check of inserted values.
    /// </summary>
    /// <param name="errors">Found errors.</param>
    /// <param name="warnings">Found warnings.</param>
    public void SanityCheck(ref List<string> errors, ref List<string> warnings)
    {
      if ((Hour == 24) && (Type != eType.TL))
        warnings.Add("Hour value 24 should be used only with Type TL (until).");
      if ((Hour == 24) && (Minute != 0))
        errors.Add("Hour value 24 should be used only when minute value is 00.");
    }

    #endregion
  }
}
