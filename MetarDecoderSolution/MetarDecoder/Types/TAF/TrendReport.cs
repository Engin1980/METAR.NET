using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENG.Metar.Decoder.Types.TAF
{
  public class TrendReport : ICodeItem
  {
    #region Properties

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private Wind _Wind = null;
    ///<summary>
    /// Sets/gets Wind value. Default value is Wind.Calm.
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

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private TrendVisibility _Visibility = null;
    ///<summary>
    /// Sets/gets Visibility value. Default value is null.
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

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private TrendPhenomInfo _Phenomens = null;
    ///<summary>
    /// Sets/gets Phenomens value. Default value is null.
    ///</summary>
    public TrendPhenomInfo Phenomens
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

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private TrendCloudInfo _Clouds = null;
    ///<summary>
    /// Sets/gets Clouds value. Default value is null.
    ///</summary>
    public TrendCloudInfo Clouds
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

    #region ICodeItem Members

    /// <summary>
    /// Returns item in code string.
    /// </summary>
    /// <returns></returns>
    public virtual string ToCode()
    {
      StringBuilder ret = new StringBuilder();

      if (Wind != null)
        ret.AppendSpaced(Wind.ToCode());
      if (Visibility != null)
        ret.AppendSpaced(this.Visibility.ToCode());
      if (Phenomens != null)
        ret.AppendSpaced(this.Phenomens.ToCode());
      if (Clouds != null)
        ret.AppendSpaced(this.Clouds.ToCode());

      return ret.ToString().TrimEnd();
    }

    /// <summary>
    /// Proceed sanity check of inserted values.
    /// </summary>
    /// <param name="errors">Found errors.</param>
    /// <param name="warnings">Found warnings.</param>
    public virtual void SanityCheck(ref List<string> errors, ref List<string> warnings)
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
