using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESystem.Extensions;

namespace ENG.Metar.Decoder.Types.Common
{
  public class DayHourDayHourFlag :ICodeItem
  {
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private DayHourFlag _From = new DayHourFlag();
    ///<summary>
    /// Sets/gets From value. Default value is null.
    ///</summary>
    public DayHourFlag From
    {
      get
      {
        return (_From);
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException();
        _From = value;
      }
    }
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private DayHourFlag _To = new DayHourFlag();
    ///<summary>
    /// Sets/gets To value. Default value is null.
    ///</summary>
    public DayHourFlag To
    {
      get
      {
        return (_To);
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException();
        _To = value;
      }
    }

    #region ICodeItem Members

    /// <summary>
    /// Returns item in code string.
    /// </summary>
    /// <returns></returns>
    public string ToCode()
    {
      StringBuilder ret = new StringBuilder();

      ret.Append(From.ToCode());
      ret.Append("/");
      ret.Append(To.ToCode());

      return ret.ToString();
    }

    /// <summary>
    /// Proceed sanity check of inserted values.
    /// </summary>
    /// <param name="errors">Found errors.</param>
    /// <param name="warnings">Found warnings.</param>
    public void SanityCheck(ref List<string> errors, ref List<string> warnings)
    {
      From.SanityCheck(ref errors, ref warnings);
      To.SanityCheck(ref errors, ref warnings);
    }

    #endregion
  }
}
