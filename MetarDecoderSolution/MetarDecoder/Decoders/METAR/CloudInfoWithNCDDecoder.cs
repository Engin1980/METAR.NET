using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.Metar.Decoder.Types.METAR;
using ENG.Metar.Decoder.Decoders.Base;

namespace ENG.Metar.Decoder.Decoders.METAR
{
  class CloudInfoWithNCDDecoder : CustomDecoder<CloudInfoWithNCD>
  {
    private const string NCD = "NCD";
    protected override CloudInfoWithNCD _Decode(ref string source)
    {
      CloudInfoWithNCD ret = new CloudInfoWithNCD();

      if (source.StartsWith(NCD))
      {
        ret.SetNCD();
        source = source.Substring(NCD.Length).TrimStart();
    }
      else
      {
        CloudInfo ci = new CloudInfoDecoder() { Required = this.Required }.Decode(ref source);
        ci.CopyPropertiesTo(ret);
      }

      return ret;
    }

    public override string Description
    {
      get { return "Cloud info with NCD"; }
    }
  }
}
