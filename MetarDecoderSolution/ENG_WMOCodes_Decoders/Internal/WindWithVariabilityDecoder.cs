using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;

namespace ENG.WMOCodes.Decoders.Internal
{
  class WindWithVariabilityDecoder : CustomDecoder<WindWithVariability>
  {

    protected override WindWithVariability _Decode(ref string source)
    {
      Wind w = new WindDecoder().Decode(ref source);
      WindVariable wv = new WindVariabilityDecoder() { Required = false }.Decode(ref source);

      WindWithVariability ret = new WindWithVariability();
      w.CopyPropertiesTo(ret);
      ret.Variability = wv;

      return ret;
    }

    public override string Description
    {
      get { return "Wind with variability"; }
    }
  }
}
