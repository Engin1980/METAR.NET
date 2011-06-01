using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Codes;
using ENG.WMOCodes.Decoders.Internal;

namespace ENG.WMOCodes.Decoders
{
  class TafDecoder : PublicDecoder<Taf>
  {
    public override string Description
    {
      get { return "Whole TAF"; }
    }

    protected override Taf _Decode(string source)
    {
      string pom = source;
      Taf ret = new Taf();

      try
      {
        new TafPrefixDecoder().Decode(ref pom);
        ret.IsCorrected = new CORDecoder().Decode(ref pom);
        ret.IsAmmended = new AMDDecoder().Decode(ref pom);
        ret.ICAO = new ICAODecoder().Decode(ref pom);
        ret.DayTime = new DayTimeDecoder().Decode(ref pom);
        ret.IsMissing = new NILDecoder().Decode(ref pom);
        if (ret.IsMissing == false)
        {
          ret.Period = new DayHourDayHourFlagDecoder().Decode(ref pom);
          ret.IsCancelled = new CNLDecoder().Decode(ref pom);
          if (ret.IsCancelled == false)
          {
            ret.Wind = new WindDecoder() { Required=false }.Decode(ref pom);
            ret.Visibility = new TrendVisibilityDecoder() { Required = false }.Decode(ref pom);
            ret.Phenomens = new TrendPhenomInfoDecoder() { Required = false }.Decode(ref pom);
            ret.Clouds = new TrendCloudInfoDecoder() { Required = false }.Decode(ref pom);
            ret.MaxTemperature = new TXDecoder() { Required = false }.Decode(ref pom);
            ret.MinTemperature = new TNDecoder() { Required = false }.Decode(ref pom);
            ret.Trends = new TafSubReportListDecoder().Decode(ref pom);
          }
        }
        ret.Remark = new RemarkDecoder().Decode(ref pom);
      }
      catch (Exception ex)
      {
        throw new Exception("Decoding of TAF failed.", ex);
      }

      return ret;
    }

  }
}
