using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.Metar.Decoder.Decoders.Base;
using ENG.Metar.Decoder.Decoders.TAF;
using ENG.Metar.Decoder.Decoders.Common;

namespace ENG.Metar.Decoder.Decoders.METAR
{
  class MetarDecoder : PublicDecoder<Metar>
  {
    public override string Description
    {
      get { return "Whole METAR"; }
    }

    protected override Metar _Decode(string source)
    {
      Metar ret = new Metar();
      string p = source;

      ret.Type = new MetarPrefixDecoder().Decode(ref p);
      ret.IsCorrected = new CORDecoder().Decode(ref p);
      ret.ICAO = new ICAODecoder().Decode(ref p);
      ret.Date = new DayTimeDecoder().Decode(ref p);
      ret.IsMissing = new NILDecoder().Decode(ref p);
      ret.IsAUTO = new AUTODecoder().Decode(ref p);
      ret.Wind = new WindWithVariabilityDecoder().Decode(ref p);
      ret.Visibility = new VisibilityDecoder().Decode(ref p);
      ret.Visibility.Runways =
        new RunwaysVisibilityDecoder().Decode(ref p);
      ret.Phenomens = new PhenomInfoDecoder() { Required = false }.Decode(ref p);
      ret.Clouds = new CloudInfoWithNCDDecoder().Decode(ref p);
      ret.Temperature = new TemperatureDecoder().Decode(ref p);
      ret.DewPoint = new DewPointDecoder().Decode(ref p);
      ret.Pressure = new PressureInfoDecoder().Decode(ref p);
      ret.RePhenomens = new RePhenomInfoDecoder().Decode(ref p);
      ret.WindShears = new WindShearInfoDecoder().Decode(ref p);
      ret.SeaSurfaceTemperature = new SeaSurfaceTemperatureDecoder() { Required = false }.Decode(ref p);
      ret.SeaState = new SeaStateDecoder() { Required = false }.Decode(ref p);
      ret.RunwayConditions = new RunwayConditionInfoDecoder().Decode(ref p);
      ret.Trend = new TrendInfoDecoder().Decode(ref p);
      ret.Remark = new RemarkDecoder() { Required = false }.Decode(ref p);

      return ret;
    }
  }
}
