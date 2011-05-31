﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.Metar.Decoder.Decoders.Base;

namespace ENG.Metar.Decoder.Decoders.METAR
{
  class PressureInfoDecoder : TypeDecoder<PressureInfo>
  {
    public override string Description
    {
      get { return "Air pressure"; }
    }

    public override string RegEx
    {
      get { return @"(^(Q|A)(\d{4}))"; }
    }

    protected override PressureInfo _Decode(System.Text.RegularExpressions.GroupCollection groups)
    {
      PressureInfo ret = new PressureInfo();

      if (groups[2].Value == "Q")
        ret.Set(groups[3].GetIntValue(), PressureInfo.eUnit.hPa);
      else
        ret.Set(groups[3].GetIntValue() / 100.0, ENG.Metar.Decoder.PressureInfo.eUnit.mmHq);

      return ret;
    }
  }
}
