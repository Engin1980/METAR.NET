using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.Metar.Decoder.Types.TAF;
using ENG.Metar.Decoder.Decoders.Base;

namespace ENG.Metar.Decoder.Decoders.TAF
{
  class TNDecoder : TypeDecoder<TemperatureExtremeTN>
  {
    public override string Description
    {
      get { return "TAF TN"; }
    }

    public override string RegEx
    {
      get { return @"^TN(\d{2})/(\d{2})(\d{2})Z"; }
    }

    protected override TemperatureExtremeTN _Decode(System.Text.RegularExpressions.GroupCollection groups)
    {
      TemperatureExtremeTN ret = new TemperatureExtremeTN();

      ret.Temperature = groups[1].GetIntValue();
      ret.Time = new Types.Common.DayHourFlag();
      ret.Time.Day = groups[2].GetIntValue();
      ret.Time.Hour = groups[3].GetIntValue();

      return ret;
    }
  }
}
