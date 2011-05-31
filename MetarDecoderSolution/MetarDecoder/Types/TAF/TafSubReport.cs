using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESystem.Extensions;

namespace ENG.Metar.Decoder.Types.TAF
{
  public class TafSubReport : TrendReport
  {
    #region Properties

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private Intervals.Interval _Interval = new Intervals.FromToInterval(
      Intervals.FromToInterval.eType.BECMG, new Common.DayHourDayHourFlag());
      ///<summary>
      /// Sets/gets Interval value. Default value is new Intervals.FromToInterval(){ Type= Intervals.FromToInterval.eType.BECMG}.
      ///</summary>
      public Intervals.Interval Interval 
      { 
        get
        {
          return (_Interval);
        }
        set
        {
          _Interval = value;
        }
      }

    #endregion Properties

    public override string ToCode()
    {
      StringBuilder ret = new StringBuilder();

      ret.AppendPreSpaced(Interval.ToCode());
      ret.AppendPreSpaced(base.ToCode());

      return ret.ToString();
    }

    public override void SanityCheck(ref List<string> errors, ref List<string> warnings)
    {
      base.SanityCheck(ref errors, ref warnings);
      Interval.SanityCheck(ref errors, ref warnings);
    }
  }
}
