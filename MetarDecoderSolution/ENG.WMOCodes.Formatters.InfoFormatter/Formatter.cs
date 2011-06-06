using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESystem.Extensions;
using R = ENG.WMOCodes.Formatters.InfoFormatter.Properties.Resources;
using ENG.WMOCodes.Types;

namespace ENG.WMOCodes.Formatters.InfoFormatter
{
  class Formatter
  {

    internal static StringBuilder ToString(Codes.Metar metar)
    {
      StringBuilder ret = new StringBuilder();

      ret.Append(R.METAR);
      ret.Append(R.Space);      

      ret.Append(R.For);
      ret.Append(R.Space);

      ret.Append(metar.ICAO);
      ret.Append(R.Space);

      ret.Append(R.IssuedAtDay);
      ret.Append(R.Space);

      ret.Append(Get(metar.Date));
      ret.Append(R.Dot);

      ret.Append(Get(metar.Wind));
      ret.Append(R.Dot);

      ret.Append(Get(metar.Visibility));
      ret.Append(R.Dot);

      ret.Append(Get(metar.Phenomens));
      ret.Append(R.Dot);

      ret.Append(Get(metar.Clouds));
      ret.Append(R.Dot);

      ret.Append(R.Temperature + R.Colon + R.Space + metar.Temperature.ToString() + R.Dot);
      ret.Append(R.DewPoint + R.Colon + R.Space + metar.DewPoint.ToString() + R.Dot);

      ret.Append(Get(metar.Pressure));
      ret.Append(R.Dot);

      ret.Append(Get(metar.WindShears));
      ret.Append(R.Dot);

      ret.Append(Get(metar.RunwayConditions));
      ret.Append(R.Dot);

      /*
      Weather trend temporally at 13:00Z : Visibility 2000 meters. Weather: mist. Sky clear. Remark: OA2.
      */

      return ret;
    }

    #region Runway condition


    private static string Get(RunwayConditionInfo runwayConditionInfo)
    {
      StringBuilder ret = new StringBuilder();

      if (runwayConditionInfo.IsSNOCLO == false && runwayConditionInfo.Count == 0)
      { ; }
      else if (runwayConditionInfo.IsSNOCLO)
        ret.Append(R.SNOCLO);
      else
        foreach (var fItem in runwayConditionInfo)
          ret.Append(Get(fItem) + R.Semicolon);

      ret.Append(R.Dot);

      return ret.ToString();
    }

    private static string Get(RunwayCondition fItem)
    {
      StringBuilder ret = new StringBuilder();

      if (fItem.IsForAllRunways)
        ret.Append(R.AllRunways + R.Colon +  R.Space);
      else
        ret.Append(R.Runway + R.Space + fItem.Runway + R.Colon+ R.Space);

      if (fItem.IsCleared)
        ret.Append(R.RunwayIsCleared);
      else
      {

      if (fItem.Contamination.HasValue)
        ret.Append(R.CoveredBy + Get(fItem.Contamination.Value) + R.Comma);

      if (fItem.Depth.HasValue)
        ret.Append(Get(fItem.Depth.Value) + R.Comma);

      if (fItem.Deposit.HasValue)
        ret.Append(Get(fItem.Deposit.Value) + R.Comma);

      if (fItem.Friction.HasValue)
        ret.Append(R.BrakingAction + R.Space + Get(fItem.Friction.Value));
      }

      ret.Append(R.Dot);

      return ret.ToString();
    }

    private static string Get(RunwayCondition.eFriction eFriction)
    {
      string ret;
      if (eFriction < RunwayCondition.eFriction.BrakingActionPoor)
        ret = eFriction.ToString().Substring(10) + "%";
      else if (eFriction.ToString().StartsWith("Reserved"))
        ret = R.Unknown;
      else
        ret = R.ResourceManager.GetString(eFriction.ToString(), R.Culture);

      return ret;
    }

    private static string Get(RunwayCondition.eDeposit eDeposit)
    {
      string ret =
        R.ResourceManager.GetString(eDeposit.ToString(), R.Culture);
      return ret;
    }

    private static string Get(RunwayCondition.eDepth eDepth)
    {
      string ret;
      switch (eDepth)
      {
        case RunwayCondition.eDepth._40cmOrMore:
          ret = R.MoreThan40CM;
          break;
        case RunwayCondition.eDepth.lessThan1mm:
          ret = R.LessThan1MM;
          break;
        case RunwayCondition.eDepth.NotReportedOrClosed:
          ret = R.NotReportedOrClosed;
          break;
        case RunwayCondition.eDepth.Reserved:
          ret = R.Reserved;
          break;
        default:
          ret = eDepth.ToString().Substring(1);
          break;
      }

      return ret;
    }

    private static string Get(RunwayCondition.eContamination eContamination)
    {
      string ret = 
        R.ResourceManager.GetString(eContamination.ToString(), R.Culture);
      return ret;
    }
    #endregion Runway condition

    #region Windsheas

    private static string Get(WindShearInfo windShearInfo)
    {
      if (windShearInfo.IsAllRunways == false && windShearInfo.Count == 0)
        return "";

      if (windShearInfo.IsAllRunways)
        return R.WindshearAllRunways;
      else
      {
        StringBuilder ret = new StringBuilder();
        ret.Append(R.WindshearAt + R.Space);
        foreach (var fItem in windShearInfo)
          ret.Append(fItem.Runway + R.Comma);

        return ret.ToString();
      }
    }
    #endregion Windsheas

    #region Pressure

    private static string Get(PressureInfo pressureInfo)
    {
      StringBuilder ret = new StringBuilder();

      ret.Append(R.Pressure + R.Colon + R.Space);

      if (pressureInfo.Unit == PressureInfo.eUnit.hPa)
        ret.Append(pressureInfo.QNH + R.hPa);
      else
        ret.Append(pressureInfo.mmHq + R.mmHq);

      ret.Append(R.Dot);

      return ret.ToString();
    }

    #endregion Pressure

    #region Clouds

    private static string Get(CloudInfoWithNCD cloudInfoWithNCD)
    {
      StringBuilder ret = new StringBuilder();

      if (cloudInfoWithNCD.IsNCD)
        ret.Append(R.NoCloudsDetected);
      else
        ret = new StringBuilder( Get(cloudInfoWithNCD as CloudInfo));

      ret.Append(R.Dot);

      return ret.ToString();
    }

    private static string Get(CloudInfo cloudInfo)
    {
      StringBuilder ret = new StringBuilder();

      if (cloudInfo.IsNSC)
        ret.Append(R.NoSignificantCloud);
      else if (cloudInfo.IsSKC)
        ret.Append(R.SkyClear);
      else if (cloudInfo.IsVerticalVisibility)
      {
        if (cloudInfo.VVDistance.HasValue)
          ret.Append(R.VerticalVisibility + R.Space + cloudInfo.VVDistance.Value);
        else
          ret.Append(R.VerticalVisibility + R.Space + R.Unknown);
      }
      else
        foreach (var fItem in cloudInfo)
          ret.Append(Get(fItem) + R.Comma);

      ret.Append(R.Dot);

      return ret.ToString();
    }

    private static string Get(Cloud fItem)
    {
      StringBuilder ret = new StringBuilder();

      ret.Append(Get(fItem.Type));
      ret.Append(R.Space);
      ret.Append(fItem.GetAltitudeIn(Common.eDistanceUnit.ft) + R.Ft + R.Space);
      if (fItem.IsCB)
        ret.Append(R.CB);
      if (fItem.IsTCU)
        ret.Append(R.TCU);

      return ret.ToString();
    }

    private static string Get(Cloud.eType eType)
    {
      return
        R.ResourceManager.GetString(eType.ToString(), R.Culture);
    }

    #endregion Clouds

    #region Phenomens

    private static string Get(PhenomInfo phenomInfo)
    {
      if (phenomInfo.Count == 0)
        return "";

      StringBuilder ret = new StringBuilder();

      ret.Append(R.Phenomens);

      foreach (var fItem in phenomInfo)
        ret.Append(Get(fItem) + R.Semicolon + R.Space);

      ret.Append(R.Dot);

      return ret.ToString();

    }

    private static string Get(ePhenomCollection phenomList)
    {
      StringBuilder ret = new StringBuilder();

      foreach (var fItem in phenomList)
        ret.Append(Get(fItem) + R.Space);

      return ret.ToString();
    }

    private static string Get(ePhenomCollection.ePhenom fItem)
    {
      string ret = 
        R.ResourceManager.GetString(fItem.ToString(), R.Culture);
      return ret;
    }

    #endregion Phenomens

    #region Visibility

    private static string Get(VisibilityForMetar visibilityForMetar)
    {
      StringBuilder ret = new StringBuilder();

      if (visibilityForMetar.IsClear)
        ret.Append ( R.VisibilityClear);
      else
      {
        
        ret.Append(R.Visibility + R.Space + Get(visibilityForMetar.Distance.Value) + R.Space);

        if (visibilityForMetar.DirectionSpecification != null)
          ret.Append (R.VisibilityFrom + Get(visibilityForMetar.DirectionSpecification.Value) + R.Space);

        if (visibilityForMetar.OtherDistance.HasValue)
          ret.Append(R.OpeningBracket + Get(visibilityForMetar.OtherDistance.Value) + R.ClosingBracket + R.Space);

        if (visibilityForMetar.Runways.Count > 0)
        {
          string pom = Get(visibilityForMetar.Runways);
          ret.Append(R.Dot);
          ret.Append(pom);
        }
      }

        ret.Append(R.Dot);

        return ret.ToString();
    }

    private static string Get(List<RunwayVisibility> list)
    {
      StringBuilder ret = new StringBuilder();

      ret.Append(R.RunwaysVisibilities + R.Colon);
      foreach (var fItem in list)
        ret.Append(Get(fItem) + R.Semicolon);

      return ret.ToString();
    }

    private static string Get(RunwayVisibility fItem)
    {
      StringBuilder ret = new StringBuilder (
        R.Runway + R.Space + R.Colon + R.Space + fItem.Distance + Get(fItem.Distance));

      if (fItem.Tendency.HasValue && fItem.Tendency.Value != RunwayVisibility.eTendency.N)
        ret.Append(R.Space + R.ResourceManager.GetString("RunwayVisibilityTendency_" + fItem.Tendency.Value.ToString()));

      if (fItem.VariableVisibility.HasValue)
        ret.Append(R.RunwayVisibilityVaryingTo + R.Space + Get(fItem.VariableVisibility.Value));

      ret.Append(R.Dot);

      return ret.ToString();
    }

    private static object Get(Types.Basic.NonNegInt nonNegInt)
    {
      return nonNegInt.ToString();
    }

    private static string Get(Common.eDirection eDirection)
    {
      string ret =
        R.ResourceManager.GetString(eDirection.ToString(), R.Culture);
      return ret;
    }

    private static string Get(Types.Basic.Racional racional)
    {
      StringBuilder ret = new StringBuilder();

      if (racional.Denominator == 1)
        ret.Append(racional.Value);
      else
        ret.Append(racional.Numerator + "/" + racional.Denominator);

      return ret.ToString();

      return ret.ToString();
    }

    #endregion Visibility

    #region Wind

    private static string Get(Types.WindWithVariability windWithVariability)
    {
      string ret = Get(windWithVariability as Wind);
      if (windWithVariability.IsVarying)
        ret += R.WindIsVaryingBetween + R.Space + windWithVariability.Variability.FromDirection.ToString("000") + R.Space +
          R.To + R.Space + windWithVariability.Variability.ToDirection.ToString("000");

      ret += R.Dot;

      return ret;
    }

    private static string Get(Types.Common.eSpeedUnit eSpeedUnit)
    {
      switch (eSpeedUnit)
      {
        case Types.Common.eSpeedUnit.kph:
          return R.KPH;
        case Types.Common.eSpeedUnit.kt:
          return R.KT;
        case Types.Common.eSpeedUnit.miph:
          return R.MIPH;
        case Types.Common.eSpeedUnit.mps:
          return R.MPS;
        default:
          return "???";
      }
    }

    private static string Get(Wind w)
    {
      StringBuilder ret = new StringBuilder();

      if (w.IsCalm)
        ret.AppendPreSpaced(R.WindCalm);
      else
      {
        ret.Append(R.Wind);

        ret.Append(R.Space);

        if (w.IsVariable)
          ret.AppendPreSpaced(R.WindVariable);
        else
          ret.Append(R.From + R.Space + w.Direction.Value.ToString("000"));

        ret.Append(R.Space);

        ret.Append(R.WindSpeedAt);

        ret.Append(R.Space);

        ret.Append(w.Speed.ToString());

        ret.Append(Get(w.Unit));

        if (w.GustSpeed.HasValue)
          ret.Append(R.WindGustingTo + R.Space + w.GustSpeed.Value.ToString() + ret.Append(Get(w.Unit)));

        ret.Append(R.Space);

      }

      ret.Append(R.Dot);

      return ret.ToString();
    }

    #endregion Wind

    private static string Get(Types.DateTimeTypes.DayHourMinute dayHourMinute)
    {
      // 1, 21:50Z
      string ret =
        dayHourMinute.Day + R.Comma + dayHourMinute.Hour.ToString("0") + R.Colon + dayHourMinute.Minute.ToString("00") + R.Zulu;

      return ret;
    }


  }
}
