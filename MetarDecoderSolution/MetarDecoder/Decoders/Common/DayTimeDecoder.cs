using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.Metar.Decoder.Decoders.Base;

namespace ENG.Metar.Decoder.Decoders
{
  class DayTimeDecoder : TypeDecoder<DayTime>
  {
    public override string Description
    {
      get { return "Day-time"; }
    }

    public override string RegEx
    {
      get { return @"(\d{2})(\d{2})(\d{2})Z"; }
    }

    protected override DayTime _Decode(System.Text.RegularExpressions.GroupCollection groups)
    {
      DayTime ret = new DayTime();

      ret.Day = groups[1].GetIntValue();
      ret.Hour = groups[2].GetIntValue();
      ret.Minute = groups[3].GetIntValue();

      return ret;
    }
  }
}
