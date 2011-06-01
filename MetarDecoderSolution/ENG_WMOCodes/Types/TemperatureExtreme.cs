using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Types.DateTimeTypes;
using ENG.WMOCodes.Types.Basic;

namespace ENG.WMOCodes.Types
{
  public abstract class TemperatureExtreme : ICodeItem
  {
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private int _Temperature = 0;
    ///<summary>
    /// Sets/gets Temperature value. Default value is 0.
    ///</summary>
    public int Temperature
    {
      get
      {
        return (_Temperature);
      }
      set
      {
        _Temperature = value;
      }
    }
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private DayHour _Time = new DayHour();
    ///<summary>
    /// Sets/gets Time value. Default value is new DayHourFlag().
    ///</summary>
    public DayHour Time
    {
      get
      {
        return (_Time);
      }
      set
      {
        _Time = value;
      }
    }

    #region ICodeItem Members

    public abstract string ToCode();

    public abstract void SanityCheck(ref List<string> errors, ref List<string> warnings);

    #endregion
  }
}
