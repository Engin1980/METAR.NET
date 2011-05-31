using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.Metar.Decoder.Decoders.Base;

namespace ENG.Metar.Decoder.Decoders
{
  class RemarkDecoder : TypeDecoder<string>
  {
    public override string Description
    {
      get { return "Remark"; }
    }

    public override string RegEx
    {
      get { return "(.*)"; }
    }

    protected override string _Decode(System.Text.RegularExpressions.GroupCollection groups)
    {
      return groups[0].Value;
    }
  }
}
