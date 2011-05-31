using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.Metar.Decoder.Types.METAR;
using ENG.Metar.Decoder.Decoders.Base;
using System.Text.RegularExpressions;

namespace ENG.Metar.Decoder.Decoders.METAR
{
  class CloudInfoDecoder : CustomDecoder<CloudInfo>
  {
    public override string Description
    {
      get { return "Clouds"; }
    }

    private const string prefixPattern = "NCD";

    protected override CloudInfo _Decode(ref string source)
    {
      CloudInfo ret = null;

      if (source.StartsWith(prefixPattern))
      {
        ret = new CloudInfo();
        ret.SetNCD();
        source = source.Substring(prefixPattern.Length);
      }
      else
        ret = DecodeFromTrendCloudInfo(ref source);

      return ret;
    }

    private CloudInfo DecodeFromTrendCloudInfo(ref string source)
    {
      TrendCloudInfo pom =
        new TrendCloudInfoDecoder().Decode(ref source);
      CloudInfo ret = new CloudInfo();

      if (pom.IsSKC)
        ret.SetSKC();
      else if (pom.IsNSC)
        ret.SetNSC();
      else if (pom.IsVerticalVisibility)
        ret.SetVerticalVisibility(pom.VVDistance);
      else
        ret.AddRange(pom);

      return ret;
    }
  }
}
