using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.Metar.Decoder.Formatters;

namespace ENG.Metar.Decoder
{
  /// <summary>
  /// Represents information about trend. To mark trend as not used. set null value into property type.
  /// </summary>
  /// <seealso cref="T:ENG.Metar.Decoder.MetarItem"/>
  public class TrendInfo : IMetarItem
  {
    #region Nested

    /// <summary>
    /// Type of trend.
    /// </summary>
    public enum eType
    {
      /// <summary>
      /// No significant change trend.
      /// </summary>
      NOSIG,
      /// <summary>
      /// Becoming trend.
      /// </summary>
      BECMG,
      /// <summary>
      /// Temporaly trend.
      /// </summary>
      TEMPO
    }

    #endregion Nested

    #region Properties

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private eType _Type = eType.NOSIG;
    ///<summary>
    /// Sets/gets Type value.
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
    private TrendTimeInfo _Times = new TrendTimeInfo();
    ///<summary>
    /// Sets/gets Dates value.
    ///</summary>
    public TrendTimeInfo Times
    {
      get
      {
        return (_Times);
      }
      set
      {
        _Times = value;
      }
    }
    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private Wind _Wind;
    ///<summary>
    /// Sets/gets Wind value. Null if not specified in trend.
    ///</summary>
    public Wind Wind
    {
      get
      {
        return (_Wind);
      }
      set
      {
        _Wind = value;
      }
    }

    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private TrendVisibility _Visibility;
    ///<summary>
    /// Sets/gets Visibility value. Null if not specified in trend.
    ///</summary>
    public TrendVisibility Visibility
    {
      get
      {
        return (_Visibility);
      }
      set
      {
        _Visibility = value;
      }
    }
    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private PhenomInfo _Phenomens;
    ///<summary>
    /// Sets/gets Phenomens value. Null if not specified in trend.
    ///</summary>
    public PhenomInfo Phenomens
    {
      get
      {
        return (_Phenomens);
      }
      set
      {
        if (value != null)
          value.SetRePhenomenFlag(false);
        _Phenomens = value;
      }
    }
    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private CloudInfo _Clouds;
    ///<summary>
    /// Sets/gets Clouds value. Null if not specified in trend.
    ///</summary>
    public CloudInfo Clouds
    {
      get
      {
        return (_Clouds);
      }
      set
      {
        _Clouds = value;
      }
    }

    #endregion Properties

    #region Inherited


    /// <summary>
    /// Returns item in text string.
    /// </summary>
    /// <param name="formatter">Formatter used to format string.</param>
    /// <returns></returns>
    public string ToInfo(InfoFormatter formatter)
    {
      string ret = null;

      /*TREND-INFO
       * 0 - true if trend is NOSIG
       * 1 - trend type short (e.g. NOSIG)
       * 2 - trend type long (e.g. no signifi...)
       * 3 - true if trend-times are present
       * 4 - TREND-TIMES-INFO
       * 5 - WIND-INFO for trend, or null
       * 6 - VISIBILITY-INFO for trend, or null
       * 7 - PHENOMS-INFO, or null
       * 8 - CLOUD-INFO, or null
       * */

      string f = null;
      try
      {
        f = formatter.TrendFormat;
      }
      catch { }
      if (f == null)
        return null;
      else if (f.Length == 0)
        return "";

      ret = formatter.Format(
        formatter.TrendFormat,
       this.Type == eType.NOSIG,
        formatter.TrendInfoTypeToString(this.Type, false),
        formatter.TrendInfoTypeToString (this.Type, true),
        (this.Times != null && this.Times.Count != 0),
        this.Times == null ? null : GetTimesInfo(formatter),
        this.Wind != null ? this.Wind.ToInfo(formatter) : null,
        this.Visibility != null ? this.Visibility.ToInfo(formatter) : null,
        this.Phenomens != null ? this.Phenomens.ToInfo(formatter) : null,
        this.Clouds != null ? this.Clouds.ToInfo(formatter) : null
        );

      return ret;
    }

    private string GetTimesInfo(InfoFormatter formatter)
    {
      StringBuilder ret = new StringBuilder();

      this.Times.ForEach(t => ret.Append(t.ToInfo(formatter)));

      return ret.ToString();
    }

    /// <summary>
    /// Returns item in metar string.
    /// </summary>
    /// <returns></returns>
    public string ToMetar()
    {
      StringBuilder ret = new StringBuilder();

      ret.AppendSpaced(this.Type.ToString());
      this.Times.ForEach(
        i => ret.AppendSpaced(i.ToMetar()));
      if (Wind != null)
        ret.AppendSpaced(Wind.ToMetar());
      if (Visibility != null)
        ret.AppendSpaced(this.Visibility.ToMetar());
      if (Phenomens != null)
        ret.AppendSpaced(this.Phenomens.ToMetar());
      if (Clouds != null)
        ret.AppendSpaced(this.Clouds.ToMetar());

      return ret.ToString().TrimEnd();
    }

    #region MetarItem Members

#if INFO
    /// <summary>
    /// Returns item in text string.
    /// </summary>
    /// <returns></returns>
    public string ToInfo()
    {
      throw new NotImplementedException();
    }
#endif //INFO

    /// <summary>
    /// Proceed sanity check of inserted values.
    /// </summary>
    /// <param name="errors">Found errors.</param>
    /// <param name="warnings">Found warnings.</param>
    public void SanityCheck(ref List<string> errors, ref List<string> warnings)
    {
      if (Wind != null)
        Wind.SanityCheck(ref errors, ref warnings);
      if (Visibility != null)
        this.Visibility.SanityCheck(ref errors, ref warnings);
      if (Phenomens != null)
        this.Phenomens.SanityCheck(ref errors, ref warnings);
      if (Clouds != null)
        this.Clouds.SanityCheck(ref errors, ref warnings);
    }

    #endregion

    #endregion Inherited
  }
}
