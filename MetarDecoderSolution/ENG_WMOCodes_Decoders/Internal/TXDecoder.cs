﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;
using ENG.WMOCodes.Types.DateTimeTypes;

namespace ENG.WMOCodes.Decoders.Internal
{
  class TXDecoder : TypeDecoder<TemperatureExtremeTX>
  {
    public override string Description
    {
      get { return "TAF TX"; }
    }

    public override string RegEx
    {
      get { return @"^TX(\d{2})/(\d{2})(\d{2})Z"; }
    }

    protected override TemperatureExtremeTX _Decode(System.Text.RegularExpressions.GroupCollection groups)
    {
      TemperatureExtremeTX ret = new TemperatureExtremeTX();

      ret.Temperature = groups[1].GetIntValue();
      ret.Time = new DayHour();
      ret.Time.Day = groups[2].GetIntValue();
      ret.Time.Hour = groups[3].GetIntValue();

      return ret;
    }
  }
}
