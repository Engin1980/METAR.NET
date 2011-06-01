using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESystem.Extensions;

namespace ENG.WMOCodes.Types.DateTimeTypes
{
  public class DayHour : DateTimeTypes.DateTimeType
  {
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private int _Day = DateTime.Now.Day;
    ///<summary>
    /// Sets/gets Day value. Default value is current day.
    ///</summary>
    public int Day
    {
      get
      {
        return (_Day);
      }
      set
      {
        if (value.IsBetween(1, 31) == false) throw new ArgumentException("Value have to be between 1-31.");
        _Day = value;
      }
    }
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private int _Hour = DateTime.Now.Hour;
    ///<summary>
    /// Sets/gets Hour value. Default value is current hour.
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

    public DayHour(int day, int hour)
    {
      this.Day = day;
      this.Hour = hour;
    }
    public DayHour() { }

    public override string ToCode()
    {
      return Day.ToString("00") + Hour.ToString("00");
    }

    public override void SanityCheck(ref List<string> errors, ref List<string> warnings)
    {
      // nothing to do
    }

  }
}
