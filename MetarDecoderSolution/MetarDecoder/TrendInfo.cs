using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetarDecoder
{
  /// <summary>
  /// Represents information about trend. To mark trend as not used. set null value into property type.
  /// </summary>
  /// <seealso cref="T:MetarDecoder.MetarItem"/>
  public class TrendInfo: MetarItem
  {
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

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private eType? _Type = eType.NOSIG;
    ///<summary>
    /// Sets/gets Type value.
    ///</summary>
    public eType? Type
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

    /// <summary>
    /// Returns item in metar string.
    /// </summary>
    /// <returns></returns>
    public string ToMetar()
    {
      if (this.Type == null)
        return "";
      else
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
    }

    #region MetarItem Members


    /// <summary>
    /// Returns item in text string.
    /// </summary>
    /// <returns></returns>
    public string ToInfo()
    {
      throw new NotImplementedException();
    }

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
  }
}
