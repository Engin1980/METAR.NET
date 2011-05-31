using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.Metar.Decoder.Decoders.Base;

namespace ENG.Metar.Decoder.Decoders.METAR
{
  class SeaStateDecoder : TypeDecoder<Common.eSeaState?>
  {
    public override string Description
    {
      get { return "Sea state"; }
    }

    public override string RegEx
    {
      get { return @"^/(\d{2})"; }
    }

    protected override Common.eSeaState? _Decode(System.Text.RegularExpressions.GroupCollection groups)
    {
      int pom = groups[1].GetIntValue();

      Common.eSeaState ret = (Common.eSeaState)pom;

      return ret;
    }
  }
}
