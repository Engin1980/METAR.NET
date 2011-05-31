using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.Metar.Decoder.Decoders.Base;

namespace ENG.Metar.Decoder.Decoders.METAR
{
  class AUTODecoder : TypeDecoder<bool>
  {
    public override string Description
    {
      get { return "AUTO - automated report"; }
    }

    public override string RegEx
    {
      get { return "(AUTO)?"; }
    }

    protected override bool _Decode(System.Text.RegularExpressions.GroupCollection groups)
    {
#warning TODO Nemelo by byt napsano nejak jinak, aby se to nemuselo kontrolovat?
      
      bool val = groups[1].Length > 0;
      return val;
    }
  }
}
