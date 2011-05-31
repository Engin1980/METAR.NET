﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.Metar.Decoder.Types.TAF;
using ENG.Metar.Decoder.Types.Common;
using System.Text.RegularExpressions;
using ENG.Metar.Decoder.Decoders.Base;
using ENG.Metar.Decoder.Decoders.TAF;

namespace ENG.Metar.Decoder.Decoders
{
  internal class TafSubReportDecoder : CustomDecoder<TafSubReport>
  {
    private class Info
    {
      public eType Type;
      public DayTimeFlag DayTimeFlag;
      public DayHourDayHourFlag FromToFlag;
    }

    private enum eType
    {
      None,
      Tempo,
      Tempo30,
      Tempo40,
      Becmg,
      Fm,
      Prob30,
      Prob40
    }

    public override string Description
    {
      get { return "Trend/Tempo/Becoming data"; }
    }

    private const int NUMBER_OF_GROUPS = 7;
    private string regexPattern
    {
      get { return "(^TEMPO)|(^PROB30 TEMPO)|(^PROB40 TEMPO)|(^BECMG)|(^FM)|(^PROB40)|(^PROB30)"; }
    }
    private eType[] regexTypes
    {
      get
      {
        return new eType[] { eType.None, eType.Tempo, eType.Tempo30, eType.Tempo40, eType.Becmg, eType.Fm, eType.Prob40, eType.Prob40 };
      }
    }

    protected override TafSubReport _Decode(ref string source)
    {
      Info info = new Info();

      bool found = DecodePrefix(ref source, info);

      TrendReport tr =
        new TrendReportDecoder().Decode(ref source);

      TafSubReport ret = CreateTafSubReport(info, tr);

      return ret;
    }

    private TafSubReport CreateTafSubReport(Info info, TrendReport tr)
    {
      TafSubReport ret = new TafSubReport();
      tr.CopyPropertiesTo(ret);

      switch (info.Type)
      {
        case eType.Becmg:
          ret.Interval =
            new ENG.Metar.Decoder.Types.TAF.Intervals.FromToInterval(
              Types.TAF.Intervals.FromToInterval.eType.BECMG, info.FromToFlag);
          break;
        case eType.Fm:
          ret.Interval =
            new ENG.Metar.Decoder.Types.TAF.Intervals.FMInterval(info.DayTimeFlag);
          break;
        case eType.None:
          throw new NotSupportedException("At this place the type must be something different from \"None\"");
        case eType.Prob30:
          ret.Interval =
            new ENG.Metar.Decoder.Types.TAF.Intervals.FromToInterval(
              Types.TAF.Intervals.FromToInterval.eType.PROB30, info.FromToFlag);
          break;
        case eType.Prob40:
          ret.Interval =
            new ENG.Metar.Decoder.Types.TAF.Intervals.FromToInterval(
              Types.TAF.Intervals.FromToInterval.eType.PROB40, info.FromToFlag);
          break;
        case eType.Tempo:
          ret.Interval =
            new ENG.Metar.Decoder.Types.TAF.Intervals.FromToInterval(
              Types.TAF.Intervals.FromToInterval.eType.TEMPO, info.FromToFlag);
          break;
        case eType.Tempo30:
          ret.Interval =
            new ENG.Metar.Decoder.Types.TAF.Intervals.FromToInterval(
              Types.TAF.Intervals.FromToInterval.eType.TEMPO_PROB30, info.FromToFlag);
          break;
        case eType.Tempo40:
          ret.Interval =
            new ENG.Metar.Decoder.Types.TAF.Intervals.FromToInterval(
              Types.TAF.Intervals.FromToInterval.eType.TEMPO_PROB40, info.FromToFlag);
          break;
        default:
          throw new NotImplementedException();
      }

      return ret;
    }

    private bool DecodePrefix(ref string source, Info info)
    {
      bool ret;
      var match = Regex.Match(source, regexPattern);
      if (match.Success)
      {
        info.Type = GetTypeOfReport(match.Groups);
        source = CutMatch(source, match.Groups[(int)info.Type].Value);
        if (info.Type == eType.Fm)
          info.DayTimeFlag = GetDayTimeFlag(ref source);
        else
          info.FromToFlag = GetFromToFlag(ref source);
        
        ret = true;
      }
      else
        ret = false;

      return ret;
    }

    private DayHourDayHourFlag GetFromToFlag(ref string source)
    {
      DayHourDayHourFlag ret =
        new DayHourDayHourFlagDecoder().Decode(ref source);

      return ret;
    }

    private DayTimeFlag GetDayTimeFlag(ref string source)
    {
      DayTimeFlag ret = new DayTimeFlag();
      string ds = source.Substring(0, 2);
      string hs = source.Substring(2, 2);
      string ms = source.Substring(4, 2);
      ret.Day = int.Parse(ds);
      ret.Hour = int.Parse(hs);
      ret.Minute = int.Parse(ms);
      source = source.Substring(6).TrimStart();
      return ret;
    }

    private eType GetTypeOfReport(GroupCollection groups)
    {
      eType ret = eType.None;
      for (int i = 1; i < NUMBER_OF_GROUPS; i++)
      {
        if (groups[i].Success)
        {
          ret = regexTypes[i];
          break;
        }
      }
      return ret;
    }

    private string CutMatch(string source, string match)
    {
      return source.Substring(match.Length).TrimStart();
    }

  }
}