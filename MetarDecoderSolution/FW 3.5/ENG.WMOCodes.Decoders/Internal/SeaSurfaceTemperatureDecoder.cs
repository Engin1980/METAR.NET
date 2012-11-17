﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;

namespace ENG.WMOCodes.Decoders.Internal
{
  class SeaSurfaceTemperatureDecoder : TypeDecoder<int?>
  {
    public override string Description
    {
      get { return "Sea surface temperature"; }
    }

    public override string RegEx
    {
      get { return @"^W(M)?(\d{2})"; }
    }

    protected override int? _Decode(System.Text.RegularExpressions.GroupCollection groups)
    {
      int ret = groups[2].GetIntValue();

      if (groups[1].Success)
        ret = -ret;

      return ret;
    }
  }
}
