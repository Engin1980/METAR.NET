using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESystem.Extensions;

namespace ENG.Metar.Decoder
{
  static class  Common
  {

    public static string HeadingToString(int heading)
    {
      if (!heading.IsBetween(0, 360))
        throw new ArgumentException("Invalid heading. Should be between 0 to 360.");

      if ((heading < 22) || (heading < 337))
        return "N";
      else if (heading < 67)
        return "NE";
      else if (heading < 117)
        return "E";
      else if (heading < 157)
        return "SE";
      else if (heading < 202)
        return "S";
      else if (heading < 247)
        return "SW";
      else if (heading < 292)
        return "W";
      else if (heading < 338)
        return "NW";
      else throw new ApplicationException("Invalid program state - unable recognize direction");
    }

    internal static string DeviceMeasureRestrictionToString(RunwayVisibility.eDeviceMeasurementRestriction? restriction)
    {
      string ret = "";
      if (restriction.HasValue)
        switch (restriction.Value)
        {
          case RunwayVisibility.eDeviceMeasurementRestriction.M:
            ret = "at most";
            break;
          case RunwayVisibility.eDeviceMeasurementRestriction.P:
            ret = "at least";
            break;
          default:
            throw new NotImplementedException();
        }
      else
        ret = null;

      return ret;
    }

    internal static string TendencyToString(RunwayVisibility.eTendency? tendency)
    {
      string ret;
      if (tendency.HasValue)
        switch (tendency.Value)
        {
          case RunwayVisibility.eTendency.D:
            return "decreasing";
            break;
          case RunwayVisibility.eTendency.N:
            return "stable";
            break;
          case RunwayVisibility.eTendency.U:
            return "increasing";
            break;
          default:
            throw new NotImplementedException();
        }
      else
        ret = null;

      return ret;
    }

    internal static object TypeToString(Cloud.eType type)
    {
      string ret;

      switch (type)
      {
        case ENG.Metar.Decoder.Cloud.eType.BKN:
          ret = "broken";
          break;
        case ENG.Metar.Decoder.Cloud.eType.FEW:
          ret = "few";
          break;
        case ENG.Metar.Decoder.Cloud.eType.OVC:
          ret = "overcast";
          break;
        case ENG.Metar.Decoder.Cloud.eType.SCT:
          ret = "scattered";
          break;
        default:
          throw new NotImplementedException();
      }

      return ret;
    }

    internal static string PressureUnitToString(PressureInfo.eUnit unit)
    {
      if (unit == PressureInfo.eUnit.hPa)
        return "hectopascals";
      else if (unit == PressureInfo.eUnit.mmHq)
        return "mm of Hq";
      else
        throw new NotImplementedException();
    }

    internal static object TypeToString(TrendInfo.eType eType)
    {
      switch (eType)
      {
        case TrendInfo.eType.BECMG:
          return "becoming";
          break;
        case TrendInfo.eType.NOSIG:
          return "no significant change.";
          break;
        case TrendInfo.eType.TEMPO:
          return "temporally";
          break;
        default:
          throw new NotImplementedException();
      }
    }
  }
}
