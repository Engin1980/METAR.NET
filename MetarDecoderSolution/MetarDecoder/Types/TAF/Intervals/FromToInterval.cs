using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.Metar.Decoder.Types.Common;
using ESystem.Extensions;

namespace ENG.Metar.Decoder.Types.TAF.Intervals
{
  public class FromToInterval : Interval
  {
    public enum eType
    {
      BECMG,
      TEMPO,
      TEMPO_PROB30,
      TEMPO_PROB40,
      INTER,
      PROB30,
      PROB40
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private eType _Type = eType.BECMG;
    ///<summary>
    /// Sets/gets Type value. Default value is eType.BECMG.
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

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private DayHourDayHourFlag _Interval = new DayHourDayHourFlag();
    ///<summary>
    /// Sets/gets Interval value. Default value is new FromDayTimeToDayTimeFlag(.
    ///</summary>
    public DayHourDayHourFlag Interval
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

    public FromToInterval(eType type, DayHourDayHourFlag intervalFlag)
    {
      Type = type;
      Interval = intervalFlag;
    }

    /// <summary>
    /// Returns item in code string.
    /// </summary>
    /// <returns></returns>
    public override string ToCode()
    {
      StringBuilder ret = new StringBuilder();

      ret.AppendPreSpaced(GetTypeCode());
      ret.AppendPreSpaced(Interval.ToCode());

      return ret.ToString();
    }

    private string GetTypeCode()
    {
      string ret = null;
      switch (Type)
      {
        case eType.TEMPO_PROB30:
          ret = "PROB30 TEMPO";
          break;
        case eType.TEMPO_PROB40:
          ret = "PROB40 TEMPO";
          break;
        default:
          ret = Type.ToString();
          break;
      }

      return ret;
    }

    /// <summary>
    /// Proceed sanity check of inserted values.
    /// </summary>
    /// <param name="errors">Found errors.</param>
    /// <param name="warnings">Found warnings.</param>
    public override void SanityCheck(ref List<string> errors, ref List<string> warnings)
    {
      // nothing to do
    }
  }
}
