using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.Metar.Decoder.Decoders.Base;

namespace ENG.Metar.Decoder.Decoders.TAF
{
  internal class CORDecoder : TypeDecoder<bool>
  {
    public override string Description
    {
      get { return "COR - correction"; }
    }

    public override string RegEx
    {
      get { return "(COR)?"; }
    }

    protected override bool _Decode(System.Text.RegularExpressions.GroupCollection groups)
    {
      bool val = groups[1].Length > 0;
      return val;
    }
  }
}
