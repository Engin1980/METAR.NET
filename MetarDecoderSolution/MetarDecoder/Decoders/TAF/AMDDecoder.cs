using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.Metar.Decoder.Decoders.Base;

namespace ENG.Metar.Decoder.Decoders.TAF
{
  internal class AMDDecoder : TypeDecoder<bool>
  {
    public override string Description
    {
      get { return "AMD - amended"; }
    }

    public override string RegEx
    {
      get { return "(AMD)?"; }
    }

    protected override bool _Decode(System.Text.RegularExpressions.GroupCollection groups)
    {
      bool val = groups[1].Length > 0;
      return val;
    }
  }
}
