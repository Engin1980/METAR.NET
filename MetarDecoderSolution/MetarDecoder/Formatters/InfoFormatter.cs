using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;

namespace ENG.Metar.Decoder.Formatters
{
  /// <summary>
  /// Abstract class for formatters used to create info-string from metar.
  /// </summary>
  public abstract class InfoFormatter
  {

    public enum eMetarFormat
    {
      ICAO = 0,
      DateDay = 1,
      DateHour = 2,
      DateMinute = 3,
      Wind = 4,
      VisibilityWithRunwayVisibility = 5,
      Phenomens = 6,
      Clouds = 7,
      Temperature = 8,
      DewPoint = 9,
      PressureInfo = 10,
      WindShearOrNull = 11,
      RunwayConditionsOrNull = 12,
      TrendOrNull = 13,
      RemarkOrNull = 14
    }

    public enum eWindFormat
    {
      Direction = 0,
      DirectionAsCardinalPoint = 1,
      WindSpeed = 2,
      WindSpeedUnit = 3,
      WindGustSpeedOrNull = 4,
      MaximumWindSpeed = 5,
      VaryingWindFromOrNull = 6,
      VaryingWindToOrNull = 7,
      IsCalm = 8
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// This is also used for trend-visibility
    /// </remarks>
    public enum eVisibilityFormat
    {
      IsClear = 0,
      Distance = 1,
      DistanceUnit = 2,
      DistanceUnitLongString = 3,
      DistanceDirectionOrNullNotTrend = 4,
      NotUsed5 = 5,
      IsMinimumMeasurable = 6,
      OtherDistanceOrNullNotTrend = 7,
      OtherDistanceDirectionOrNullNotTrend = 8,
      IsRunwayVisibilityPresentNotTrend = 9,
      RunwayVisibilityFormatNotTrend = 10
    }

    public enum eRunwayVisibilityFormat
    {
      DeviceMeasureRestrictionOrNull = 0,
      Distance = 1,
      DistanceUnit = 2,
      DistanceUnitLongString = 3,
      RunwayDesignator = 4,
      TendencyAsStringOrNull = 5,
      VariableVisibilityDistanceOrNull = 6
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Also used for re-phenoms
    /// </remarks>
    public enum ePhenomsFormat
    {
      IsNSW = 0,
      ArePhenomsPresent = 1,
      PhenomFormat = 2
    }

    public enum ePhenomFormat
    {
      ArePhenomsPresent = 0,
      PhenomItemFormat = 1
    }
    public enum ePhenomItemFormat
    {
      PhenomItemAbbreviation = 0,
      PhenomItemAsString = 1
    }

    public enum eCloudFormat
    {
      Type = 0,
      TypeAsLongString = 1,
      AltitudeInHundreds = 2,
      Altitude = 3,
      IsCB = 4,
      IsTCU = 5
    }

    public enum ePressureFormat
    {
      ValueInMMHQ = 0,
      ValueInHPA = 1,
      ValueInCurrentUnit = 2,
      CurrentUnit = 3,
      CurrentUnitLongString = 4
    }

    public enum eRunwayConditionsFormat
    {
      IsSNOCLO = 0,
      AreRunwayConditionsPresent = 1,
      RunwayConditionFormat
    }

    public enum eRunwayConditionFormat
    {
      IsForAllRunways = 0,
      IsObsolete = 1,
      IsCleared = 2,
      RunwayDesignator = 3,
      DepositNumOrNull = 4,
      DepositStringNeverNull = 5,
      ContaminationNumOrNull = 6,
      ContaminationStringNeverNull = 7,
      DepthNumOrNull = 8,
      DepthStringNeverNull = 9,
      FrictionNumOrNull = 10,
      FrictionNumNeverNull = 11
    }

    public enum eWindShearsFormat
    {
      IsNonEmpty = 0,
      IsForAllRunways = 1,
      IsSomeRunwaysPresent = 2,
      WindShearFormat = 3
    }

    public enum eWindShearFormat
    {
      RunwayDesignator = 0
    }

    public enum eTrendFormat
    {
      IsNOSIG = 0,
      TrendType = 1,
      TrendTypeLongString = 2,
      AreTrendTimesPresent = 3,
      TrendTimesInfoFormat = 4,
      WindFormat = 5,
      VisibilityFormat = 6,
      PhenomsFormat = 7,
      CloudsFormat = 8
    }
    public enum eTrendTimesFormat
    {
      TrendTimes = 0
    }

    public enum eTrendTimeFormat
    {
      TimeType = 0,
      TimeTypeLongString = 1,
      Hour = 2,
      Minute = 3
    }

    public abstract string MetarFormat { get; }

    /// <summary>
    /// Gets the definition of format wind
    /// </summary>
    /// <value></value>
    public abstract string WindFormat { get; }

    /// <summary>
    /// Gets the definition of format for visibility (both, standard and trend).
    /// </summary>
    /// <value></value>
    public abstract string VisibilityFormat { get; }
    /// <summary>
    /// Gets the definition of format for runway visibility.
    /// </summary>
    /// <value></value>
    public abstract string RunwayVisibilityFormat { get; }

    /// <summary>
    /// Gets the definition of format for set of clouds.
    /// </summary>
    /// <value></value>
    public abstract string CloudsFormat { get; }
    /// <summary>
    /// Gets the definition of format for one cloud.
    /// </summary>
    /// <value></value>
    public abstract string CloudFormat { get; }

    /// <summary>
    /// Gets the definition of format for pressure.
    /// </summary>
    /// <value></value>
    public abstract string PressureFormat { get; }

    /// <summary>
    /// Gets the definition of format for set of runway conditions.
    /// </summary>
    /// <value></value>
    public abstract string RunwayConditionsFormat { get; }
    /// <summary>
    /// Gets the definition of format for one runway condition.
    /// </summary>
    /// <value></value>
    public abstract string RunwayConditionFormat { get; }

    /// <summary>
    /// Gets the definition of format for set of wind-shear info.
    /// </summary>
    /// <value></value>
    public abstract string WindShearsFormat { get; }
    /// <summary>
    /// Gets the definition of format for one wind-shear info.
    /// </summary>
    /// <value></value>
    public abstract string WindShearFormat { get; }

    /// <summary>
    /// Gets the definition of format for set of blocks of phenomens. (e.g. RABR SN)
    /// </summary>
    /// <value></value>
    public abstract string PhenomsFormat { get; }
    /// <summary>
    /// Gets the definition of format for set of blocks of re-phenomens.  (e.g. RERABR)
    /// </summary>
    /// <value></value>
    public abstract string RePhenomsFormat { get; }
    /// <summary>
    /// Gets the definition of format for one block of phenomens.  (e.g. RABR)
    /// </summary>
    /// <value></value>
    public abstract string PhenomFormat { get; }
    /// <summary>
    /// Gets the definition of format for one phenomen.  (e.g. RA)
    /// </summary>
    /// <value></value>
    public abstract string PhenomItemFormat { get; }

    /// <summary>
    /// Gets the definition of format for trend info.
    /// </summary>
    /// <value></value>
    public abstract string TrendFormat { get; }
    /// <summary>
    /// Gets the definition of format for set of times in trend info.
    /// </summary>
    /// <value></value>
    public abstract string TrendTimesFormat { get; }
    /// <summary>
    /// Gets the definition of format for one time information in trend info.
    /// </summary>
    /// <value></value>
    public abstract string TrendTimeFormat { get; }

    /// <summary>
    /// Converts a format-string into info-string. Parameters required, used in same way as string.Format.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public string Format(string value, params object[] values)
    {
      value = ResolveConditionalRegex(value, values);

      string ret =
        string.Format(value, values);

      return ret;
    }

    /// <summary>
    /// </summary>
    /// <param name="conditionalRegex"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    private static string ResolveConditionalRegex(string conditionalRegex, object[] @values)
    {
      StringBuilder ret = new StringBuilder();

      string rgx = @"^((\!?(\d+)(^|))?[^\[\]]*)(((?'Open'\[)[^\[\]]*)+((?'Close-Open'\])([^\[\]]*))+)*(?(Open)(?!))$";

      Match m = Regex.Match(conditionalRegex, rgx);

      string beg;
      string end;
      string inn;

      string retBeg = "";
      string retEnd = "";
      string retInn = "";

      end = m.Groups[8].Value;
      inn = (m.Groups[10].Success) ? m.Groups[10].Value : "";
      beg = conditionalRegex.Substring(0, conditionalRegex.Length - (inn.Length > 0 ? inn.Length + 2 : 0) - end.Length);

      retEnd = end;
      if (!string.IsNullOrEmpty(inn))
        retInn = EvaluateConditionalRegex(inn, @values);
      if (m.Groups[10].Success)
        retBeg = ResolveConditionalRegex(beg, @values);
      else
        retBeg = beg;

      ret.Append(retBeg);
      ret.Append(retInn);
      ret.Append(retEnd);

      return ret.ToString();
    }

    /// <summary>
    /// </summary>
    /// <param name="conditionalRegex"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    private static string EvaluateConditionalRegex(string conditionalRegex, object[] @values)
    {
      string ret;
      string rgx = @"(\!)?(\d+)\|(.*)";

      Match m = Regex.Match(conditionalRegex, rgx);
      if (m.Success)
      {
        bool condRes = IsConditionTrue(values[m.Groups[2].GetIntValue()]);
        if (condRes != m.Groups[1].Success)
          ret = ResolveConditionalRegex(m.Groups[3].Value, values);
        else
          ret = "";
      }
      else
        ret = ResolveConditionalRegex(conditionalRegex, values);

      return ret;
    }

    /// <summary>
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    private static bool IsConditionTrue(object obj)
    {
      if (obj == null)
        return false;

      if (obj is bool || obj is bool?)
        return ((bool)obj); // bool je true ==> return true
      else if (obj is int?)
        return (((int?)obj).HasValue); // int != 0 ==> return true
      else if (obj is NonNegInt?)
        return (((NonNegInt?)obj).HasValue); // dtto
      else if (obj is ICollection)
        return (((ICollection)obj).Count != 0); // Coll.count != 0 ==> return true
      else
        return true; // neni null ==> true;


      //if (obj is bool || obj is bool?)
      //  return ((bool)obj); // bool je true ==> return true
      //else if (obj is int || obj is int?)
      //  return (((int)obj) != 0); // int != 0 ==> return true
      //else if (obj is NonNegInt || obj is NonNegInt?)
      //  return (((NonNegInt)obj) != 0); // dtto
      //else if (obj is ICollection)
      //  return (((ICollection)obj).Count != 0); // Coll.count != 0 ==> return true
      //else
      //  return true; // neni null ==> true;
    }
  }
}
