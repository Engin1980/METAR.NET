using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.Metar.Decoder.Decoders.Base;

namespace ENG.Metar.Decoder.Decoders.METAR
{
  class VisibilityDecoder : TypeDecoder<VisibilityForMetar>
  {
    public override string Description
    {
      get { return "Visibility"; }
    }

    public override string RegEx
    {
      get
      {
        return
          @"^((CAVOK)|(SKC)|((\d{4})(NE|SW|NW|SE|N|E|S|W)?( (\d{4})(N|NE|E|SE|S|SW|W|NW))?)|((M)?(\d+)(/(\d))?SM))";
      }
    }

    protected override VisibilityForMetar _Decode(System.Text.RegularExpressions.GroupCollection groups)
    {
      VisibilityForMetar ret = new VisibilityForMetar();

      if (groups[2].Success)
        ret.SetCAVOK();
      else if (groups[3].Success)
        ret.SetSKC();
      else if (groups[4].Success)
      {
        int distance = groups[5].GetIntValue();
        ENG.Metar.Decoder.Common.eDirection? dir = null;
        int? otherDist = null;
        ENG.Metar.Decoder.Common.eDirection? otherDir = null;

        if (groups[6].Success)
          dir = (ENG.Metar.Decoder.Common.eDirection)Enum.Parse(
            typeof(ENG.Metar.Decoder.Common.eDirection), groups[6].Value, false);

        if (groups[7].Success)
        {
          otherDist = groups[8].GetIntValue();
          otherDir = (ENG.Metar.Decoder.Common.eDirection)Enum.Parse(
            typeof(ENG.Metar.Decoder.Common.eDirection), groups[9].Value, false);
        }

        ret.SetMeters(distance, dir, otherDist, otherDir);
      }
      else
        ret.SetMiles(new Racional(
          groups[12].GetIntValue(),
          (groups[14].Success) ? groups[14].GetIntValue() : 1), groups[11].Success);

      return ret;
    }
  }
}
