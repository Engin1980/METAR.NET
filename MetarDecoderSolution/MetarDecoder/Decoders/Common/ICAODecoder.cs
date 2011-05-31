using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.Metar.Decoder.Decoders.Base;

namespace ENG.Metar.Decoder.Decoders
{
  class ICAODecoder : TypeDecoder<string>
  {
    public override string Description
    {
      get { return "ICAO"; }
    }

    public override string RegEx
    {
      get { return "[A-Z]{4}"; }
    }

    protected override string _Decode(System.Text.RegularExpressions.GroupCollection groups)
    {
      string ret = groups[0].Value;

      return ret;
    }


  }
}
