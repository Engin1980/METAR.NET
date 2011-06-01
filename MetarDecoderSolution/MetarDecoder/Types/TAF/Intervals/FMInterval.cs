﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.Metar.Decoder.Types.Common;

namespace ENG.Metar.Decoder.Types.TAF.Intervals
{
  public class FMInterval : Interval
  {
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private DayHourMinute _From = new DayHourMinute();
    ///<summary>
    /// Sets/gets From value. Default value is new DayHourMinute().
    ///</summary>
    public DayHourMinute From
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

    /// <summary>
    /// Initializes a new instance of the <see cref="FMInterval"/> class.
    /// </summary>
    /// <param name="intervalFlag">The interval flag.</param>
    public FMInterval(DayHourMinute intervalFlag)
    {
      this.From = intervalFlag;
    }

    /// <summary>
    /// Returns item in code string.
    /// </summary>
    /// <returns></returns>
    public override string ToCode()
    {
      return "FM" + From.ToCode();
    }

    /// <summary>
    /// Proceed sanity check of inserted values.
    /// </summary>
    /// <param name="errors">Found errors.</param>
    /// <param name="warnings">Found warnings.</param>
    public override void SanityCheck(ref List<string> errors, ref List<string> warnings)
    {
      // nothing to do here
    }
  }
}
