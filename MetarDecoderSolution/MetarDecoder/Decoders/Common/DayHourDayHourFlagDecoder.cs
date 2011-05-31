using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.Metar.Decoder.Decoders.Base;
using ENG.Metar.Decoder.Types.Common;

namespace ENG.Metar.Decoder.Decoders.TAF
{
  class DayHourDayHourFlagDecoder : TypeDecoder<DayHourDayHourFlag>
  {
    public override string Description
    {
      get { return "TAF period"; }
    }

    public override string RegEx
    {
      get { return @"((\d{2})(\d{2})/(\d{2})(\d{2}))"; }
    }

    protected override DayHourDayHourFlag _Decode(System.Text.RegularExpressions.GroupCollection groups)
    {
      DayHourDayHourFlag ret = null;
      
        ret = DecodeRegularTaf(groups);

      return ret;
    }

    private DayHourDayHourFlag DecodeRegularTaf(System.Text.RegularExpressions.GroupCollection groups)
    {
      DayHourDayHourFlag ret = new DayHourDayHourFlag();


      int fd = groups[2].GetIntValue();
      int fh = groups[3].GetIntValue();
      int td = groups[4].GetIntValue();
      int th = groups[5].GetIntValue();

      ret.From = new DayHourFlag(fd, fh);
      ret.To = new DayHourFlag(td, th);

      return ret;
    }
  }
}
