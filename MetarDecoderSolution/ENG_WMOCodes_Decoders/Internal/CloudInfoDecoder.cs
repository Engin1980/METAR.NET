using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;

namespace ENG.WMOCodes.Decoders.Internal
{
  class CloudInfoDecoder : CustomDecoder<CloudInfoWithNCD>
  {
    public override string Description
    {
      get { return "Clouds"; }
    }

    private const string prefixPattern = "NCD";

    protected override CloudInfoWithNCD _Decode(ref string source)
    {
      CloudInfoWithNCD ret = null;

      if (source.StartsWith(prefixPattern))
      {
        ret = new CloudInfoWithNCD();
        ret.SetNCD();
        source = source.Substring(prefixPattern.Length);
      }
      else
        ret = DecodeFromTrendCloudInfo(ref source);

      return ret;
    }

    private CloudInfoWithNCD DecodeFromTrendCloudInfo(ref string source)
    {
      CloudInfo pom =
        new TrendCloudInfoDecoder().Decode(ref source);
      CloudInfoWithNCD ret = new CloudInfoWithNCD();

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
