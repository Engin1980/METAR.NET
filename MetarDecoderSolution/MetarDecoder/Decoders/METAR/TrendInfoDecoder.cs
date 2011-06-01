using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.Metar.Decoder.Decoders.Base;
using System.Text.RegularExpressions;
using ENG.Metar.Decoder.Types.TAF;

namespace ENG.Metar.Decoder.Decoders.METAR
{
  class TrendInfoDecoder : CustomDecoder<MetarTrendInfo>
  {
    private const string RT_TYPEDATES = @"((NOSIG)|((TEMPO|BECMG)(( " + RT_TYPEDATE + ")*)))";
    private const string RT_TYPEDATE = @"(FM|TL|AT)(\d{2})(\d{2})";

    protected override MetarTrendInfo _Decode(ref string source)
    {
      MetarTrendInfo ret = null;

      Match m = Regex.Match(source, RT_TYPEDATES);
      if (m.Success)
      {
        source = source.Substring(m.Groups[0].Length).TrimStart();
        ret = DecodeTrend(m.Groups, ref source);
      }
      else
        throw new DecodeException(Description, new ArgumentException("source"));

      return ret;
    }

    private MetarTrendInfo DecodeTrend(GroupCollection groups, ref string source)
    {
      MetarTrendInfo ret = new MetarTrendInfo();

      if (groups[2].Success)
        ret.Type = MetarTrendInfo.eType.NOSIG;
      else
      {
        ret.Type = (MetarTrendInfo.eType)Enum.Parse(typeof(MetarTrendInfo.eType), groups[4].Value, false);

        ret.Times = DecodeTrendDates(groups[5].Value);

        DecodeTrendValues(ref source, ret);
      }

      return ret;
    }

    private void DecodeTrendValues(ref string source, MetarTrendInfo ret)
    {
      TrendReport pom = new TrendReportDecoder().Decode(ref source);

      pom.CopyPropertiesTo(ret);
    }

    private TrendTimeInfo DecodeTrendDates(string source)
    {
      TrendTimeInfo ret = new TrendTimeInfo();

      Match m = Regex.Match(source, RT_TYPEDATE);
      while (m.Success)
      {
        ret.Add(DecodeTrendDate(m.Groups));

        m = m.NextMatch();
      }
      return ret;
    }

    private static TrendTime DecodeTrendDate(GroupCollection groups)
    {
      TrendTime ret = new TrendTime();

      ret.Type = (TrendTime.eType)Enum.Parse(typeof(TrendTime.eType), groups[1].Value, false);
      ret.Hour = int.Parse(groups[2].Value);
      ret.Minute = int.Parse(groups[3].Value);

      return ret;
    }

    public override string Description
    {
      get { return "METAR TREND"; }
    }
  }
}
