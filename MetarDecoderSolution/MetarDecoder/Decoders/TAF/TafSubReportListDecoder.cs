using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.Metar.Decoder.Decoders;
using System.Text.RegularExpressions;
using ENG.Metar.Decoder.Types.Common;
using ENG.Metar.Decoder.Decoders.Base;

namespace ENG.Metar.Decoder.Types.TAF
{
  class TafSubReportListDecoder : CustomDecoder<List<TafSubReport>>
  {
    public override string Description
    {
      get { return "Trend/Tempo/Becoming sets"; }
    }

    public string regexPattern
    {
      get { return "(^TEMPO)|(^BECMG)|(^FM)|(^PROB40)|(^PROB30)"; }
    }

    protected override List<TafSubReport> _Decode(ref string source)
    {
      List<TafSubReport> ret = new List<TafSubReport>();
      TafSubReport report = null;

      string p = source;
      bool found = true;
      while (found)
      {
        var m = Regex.Match(p, regexPattern);
        if (m.Success)
        {
          try
          {
            report = new TafSubReportDecoder().Decode(ref p);
          } // try
          catch (Exception ex)
          {
            throw new DecodeException(this.Description, ex);
          } // catch (Exception ex)
          ret.Add(report);
        }
        else
          found = false;
      }

      source = p;
      return ret;
    }
  }
}
