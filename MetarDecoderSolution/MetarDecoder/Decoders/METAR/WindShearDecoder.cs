﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.Metar.Decoder.Decoders.Base;

namespace ENG.Metar.Decoder.Decoders.METAR
{
  class WindShearDecoder : TypeDecoder<WindShear>
  {
    public override string Description
    {
      get { return "Wind shear for runway"; }
    }

    private const string R_WS = @"^RWY(\d{2}(R|L|C)?)";

    public override string RegEx
    {
      get { return R_WS; }
    }

    protected override WindShear _Decode(System.Text.RegularExpressions.GroupCollection groups)
    {
      WindShear ret = new WindShear();

      ret.Runway = groups[1].Value;

      return ret;
    }
  }
}
