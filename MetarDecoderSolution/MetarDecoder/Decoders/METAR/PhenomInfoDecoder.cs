﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.Metar.Decoder.Decoders.Base;
using System.Text.RegularExpressions;

namespace ENG.Metar.Decoder.Decoders.METAR
{
  class PhenomInfoDecoder : TypeDecoder<PhenomInfo>
  {
    public override string Description
    {
      get { return "Phenomens"; }
    }

    private const string R_PHENOM_ITEM = @"(\-|\+| |VC|MI|BC|PR|DR|BL|SH|TS|FZ|DZ|RA|SN|SG|IC|PL|GR|GS|BR|FG|FU|VA|DU|SA|HZ|PO|SQ|FC|SS|DS)";

    public override string RegEx
    {
      get { return @"(^" + R_PHENOM_ITEM + "+)"; }
    }

    protected override PhenomInfo _Decode(System.Text.RegularExpressions.GroupCollection groups)
    {
      PhenomInfoWithNSW ret = null;

      if (groups[0].Success)
      {
          ret = new PhenomInfoWithNSW();

          string str = groups[1].Value;
          Match m = Regex.Match(str, RegEx);
          List<ePhenomCollection> colls = null;

          colls = DecodePhenomSets(m);

          ret.AddRange(colls);
      }
      else
        ret = null;

      return ret;
    }

    private static List<ePhenomCollection> DecodePhenomSets(Match setMatch)
    {
      List<ePhenomCollection> ret = new List<ePhenomCollection>();
      ePhenomCollection curr = new ePhenomCollection();

      Match m = Regex.Match(setMatch.Value, R_PHENOM_ITEM);

      while (m.Success)
      {
        string val = m.Value.Trim();
        if (val == "-")
          curr.Add(ePhenomCollection.ePhenom.Light);
        else if (val == "+")
          curr.Add(ePhenomCollection.ePhenom.Heavy);
        else if (val == "")
        {
          if (curr.Count > 0)
          {
            ret.Add(curr);
            curr = new ePhenomCollection();
          }
        }
        else
        {
          var ph = (ePhenomCollection.ePhenom)Enum.Parse(typeof(ePhenomCollection.ePhenom), m.Value, false);
          curr.Add(ph);
        }
        m = m.NextMatch();
      }

      if (curr.Count > 0)
        ret.Add(curr);

      return ret;
    }
  }
}
