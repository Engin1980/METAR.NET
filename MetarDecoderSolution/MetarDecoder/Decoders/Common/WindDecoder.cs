using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ENG.Metar.Decoder.Decoders.Base;

namespace ENG.Metar.Decoder.Decoders
{
  class WindDecoder : TypeDecoder<Wind>
  {
    public override string Description { get { return "Wind"; } }
    public override string RegEx { get { return @"^((\d{3}|VRB)(\d{2})(G(\d{2,3}))?(KT|MPS|KMH))"; } }

    protected override Wind _Decode(GroupCollection grp)
    {
      Wind ret = new Wind();

      if (grp[2].Value == "VRB")
        ret.IsVariable = true;
      else
        ret.Direction = grp[2].GetIntValue();
      ret.Speed = grp[3].GetIntValue();
      ret.GustSpeed = (grp[5].Success ? (int?)grp[5].GetIntValue() : null);
      ret.Unit = (ENG.Metar.Decoder.Common.eSpeedUnit)Enum.Parse(typeof(ENG.Metar.Decoder.Common.eSpeedUnit), grp[6].Value.ToLower(), false);      

      return ret;
    }
  }
}
