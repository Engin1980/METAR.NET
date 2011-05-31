using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESystem.Extensions;

namespace ENG.Metar.Decoder.Types.Common
{
  public class TimeFlag
  {
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private int _Hour = 0;
    ///<summary>
    /// Sets/gets Hour value. Default value is 0.
    ///</summary>
    public int Hour
    {
      get
      {
        return (_Hour);
      }
      set
      {
        if (value.IsBetween(0, 23) == false) throw new ArgumentException("Value have to be between 0-23.");
        _Hour = value;
      }
    }
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private int _Minute = 0;
    ///<summary>
    /// Sets/gets Minute value. Default value is 0.
    ///</summary>
    public int Minute
    {
      get
      {
        return (_Minute);
      }
      set
      {
        if (value.IsBetween(0, 60) == false) throw new ArgumentException("Value have to be between 0-60.");
        _Minute = value;
      }
    }

    public TimeFlag() { }
    public TimeFlag(int hour, int minute) { Hour = hour; Minute = minute; }
  }
}
