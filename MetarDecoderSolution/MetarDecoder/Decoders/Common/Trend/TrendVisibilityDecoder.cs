using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.Metar.Decoder.Decoders.Base;

namespace ENG.Metar.Decoder.Decoders
{
  class TrendVisibilityDecoder : TypeDecoder<TrendVisibility>
  {
    public override string Description
    {
      get { return "Trend visibility"; }
    }

    public override string RegEx
    {
      get
      {
        return
          @"^((CAVOK)|(\d{4})|((M)?(\d+)(/(\d))?SM))";
      }
    }

    protected override TrendVisibility _Decode(System.Text.RegularExpressions.GroupCollection grp)
    {
      TrendVisibility ret = null;

      if (grp[0].Success)
      {
        ret = new TrendVisibility();
        if (grp[2].Success)
          ret.SetCAVOK();
        else if (grp[3].Success)
        {
          ret.SetMeters(grp[3].GetIntValue());
        }
        else
          ret.SetMiles(new Racional(
            grp[6].GetIntValue(),
            (grp[8].Success) ? grp[8].GetIntValue() : 1), grp[5].Success);
      }

      return ret;
    }
  }
}
