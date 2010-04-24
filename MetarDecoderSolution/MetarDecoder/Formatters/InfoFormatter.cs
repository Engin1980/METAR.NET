using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using ESystem.Extensions;

namespace ENG.Metar.Decoder.Formatters
{
  /// <summary>
  /// Abstract class for formatters used to create info-string from metar.
  /// </summary>
  public abstract class InfoFormatter
  {

    #region Enums

    /// <summary>
    /// Metar info values for info-strings. Source is metar.<see cref="ENG.Metar.Decoder.Metar"/>
    /// </summary>
    public enum eMetarFormat
    {
      /// <summary>
      /// Icao code, 4 chars
      /// </summary>
      ICAO = 0,
      /// <summary>
      /// Number of day of metar
      /// </summary>
      DateDay = 1,
      /// <summary>
      /// Hour of metar
      /// </summary>
      DateHour = 2,
      /// <summary>
      /// Minute of metar, allways formatted as two numbers.
      /// </summary>
      DateMinute = 3,
      /// <summary>
      /// Wind format. <see cref="eWindFormat"/>
      /// </summary>
      WindFormat = 4,
      /// <summary>
      /// Visibility format including runway visibility. <see cref="eVisibilityFormat"/>
      /// </summary>
      VisibilityFormat = 5,
      /// <summary>
      /// Phenomens format. <see cref="ePhenomsFormat"/>
      /// </summary>
      PhenomsFormat = 6,
      /// <summary>
      /// Clouds format. <see cref="eCloudsFormat"/>
      /// </summary>
      CloudsFormat = 7,
      /// <summary>
      /// Temperature value.
      /// </summary>
      Temperature = 8,
      /// <summary>
      /// Dewpoint value.
      /// </summary>
      DewPoint = 9,
      /// <summary>
      /// Pressure format. <see cref="ePressureFormat"/>
      /// </summary>
      PressureFormat = 10,
      /// <summary>
      /// Windshear format, or null if not used. <see cref="eWindShearsFormat"/>
      /// </summary>
      WindShearFormatOrNull = 11,
      /// <summary>
      /// RunwayConditionsFormat, or null if not used. <see cref="eRunwayConditionsFormat"/>
      /// </summary>
      RunwayConditionsFormatOrNull = 12,
      /// <summary>
      /// Trend format, or null if not used. <see cref="eTrendFormat"/>
      /// </summary>
      TrendFormatOrNull = 13,
      /// <summary>
      /// Remark string, or null if not present.
      /// </summary>
      RemarkOrNull = 14
    }

    /// <summary>
    /// Wind info values for info-strings. <see cref="ENG.Metar.Decoder.Wind"/>
    /// </summary>
    public enum eWindFormat
    {
      /// <summary>
      /// True if wind is variable (VRB).
      /// </summary>
      IsVariable = 0,
      /// <summary>
      /// True if wind is calm.
      /// </summary>
      IsCalm = 1,
      /// <summary>
      /// Wind direction, as single value (not formatted to "000"), or null if wind is variable.
      /// </summary>
      DirectionOrNull = 2,
      /// <summary>
      /// Gets direction as one of values N/NE/E/SE/S/SW/W/NW. Null if wind is variable.
      /// </summary>
      DirectionAsCardinalPointShortStringOrNull = 3,
      /// <summary>
      /// Gets direction as one of values north, east, .... Null if wind is variable.
      /// </summary>
      DirectionAsCardinalPointLongStringOrNull = 4,
      /// <summary>
      /// Speed of wind.
      /// </summary>
      WindSpeed = 5,
      /// <summary>
      /// Unit of wind-speed.
      /// </summary>
      WindSpeedUnit = 6,
      /// <summary>
      /// Wind gust speed, or null if no gusts.
      /// </summary>
      WindGustSpeedOrNull = 7,
      /// <summary>
      /// Maximum of values WindSpeed and WindGustSpeed.
      /// </summary>
      MaximumWindSpeed = 8,
      /// <summary>
      /// If varying wind, first value "from-where". Null if not used.
      /// </summary>
      VaryingWindFromOrNull = 9,
      /// <summary>
      /// If varying wind, second value "to-where". Null if not used.
      /// </summary>
      VaryingWindToOrNull = 10,
    }

    /// <summary>
    /// Visibility values for info-strings.  <see cref="ENG.Metar.Decoder.Visibility"/>
    /// </summary>
    /// <remarks>
    /// This is also used for trend-visibility
    /// </remarks>
    public enum eVisibilityFormat
    {
      /// <summary>
      /// True if visibility is ok (CAVOK or SKC).
      /// </summary>
      IsClear = 0,
      /// <summary>
      /// Visibility distance, or null if is clear.
      /// </summary>
      DistanceOrNull = 1,
      /// <summary>
      /// Unit of visibility in short (m/sm)
      /// </summary>
      DistanceUnit = 2,
      /// <summary>
      /// Units of visibility as long string (meters/miles)
      /// </summary>
      DistanceUnitLongString = 3,
      /// <summary>
      /// Visibility distance direction, or null if not used. This value is not used at trend part of metar.
      /// </summary>
      DistanceDirectionOrNullNotTrend = 4,
      /// <summary>
      /// True if value is minimum of measurable value of measuring device.
      /// </summary>
      IsMinimumMeasurable = 5,
      /// <summary>
      /// Second direction measured visibility if present, null otherwise.  This value is not used at trend part of metar.
      /// </summary>
      OtherDistanceOrNullNotTrend = 6,
      /// <summary>
      /// Second direction of second visibility, if present, null otherwise.  This value is not used at trend part of metar.
      /// </summary>
      OtherDistanceDirectionOrNullNotTrend = 7,
      /// <summary>
      /// Returns true if there are any specifications of runway visibility, null otherwise.  This value is not used at trend part of metar.
      /// </summary>
      IsRunwayVisibilityPresentNotTrend = 8,
      /// <summary>
      /// Runways visibility specification if exists. <see cref="eRunwayVisibilityFormat"/>. Null if not present.  This value is not used at trend part of metar.
      /// </summary>
      RunwayVisibilityFormatNotTrend = 9
    }

    /// <summary>
    /// Runway visibility for info-strings. <see cref="ENG.Metar.Decoder.RunwayVisibility"/>
    /// </summary>
    public enum eRunwayVisibilityFormat
    {
      /// <summary>
      /// Device measure restriction (at least/at most). Or null if not used.
      /// </summary>
      DeviceMeasureRestrictionOrNull = 0,
      /// <summary>
      /// Visibility distance.
      /// </summary>
      Distance = 1,
      /// <summary>
      /// Visibility distance unit as short string (m/ft).
      /// </summary>
      DistanceUnit = 2,
      /// <summary>
      /// Visibility distance unit as long string (meters, feet).
      /// </summary>
      DistanceUnitLongString = 3,
      /// <summary>
      /// Runway designator.
      /// </summary>
      RunwayDesignator = 4,
      /// <summary>
      /// Visibility tendency as string (increasing/decreasing/stable), null if not present.
      /// </summary>
      TendencyAsStringOrNull = 5,
      /// <summary>
      /// Visibility varies value, or null if not used.
      /// </summary>
      VariableVisibilityDistanceOrNull = 6
    }

    /// <summary>
    /// Phenoms values for info-strings.  <see cref="ENG.Metar.Decoder.PhenomInfo"/>
    /// </summary>
    /// <remarks>
    /// This represents the whole phenomens block, like "+RABR -SNTS".
    /// Also used for re-phenoms.
    /// </remarks>
    public enum ePhenomsFormat
    {
      /// <summary>
      /// True if there is no significant weather.
      /// </summary>
      IsNSW = 0,
      /// <summary>
      /// True if there are any phenoms present.
      /// </summary>
      ArePhenomsPresent = 1,
      /// <summary>
      /// Phenom format. <see cref="ePhenomFormat"/>
      /// </summary>
      PhenomFormat = 2
    }

    /// <summary>
    /// Phenoms block values for info-string. <see cref="ENG.Metar.Decoder.ePhenomCollection"/>
    /// </summary>
    /// <remarks>
    /// This represents the one phenomens block, like "+RABR" or "-SNTS".
    /// </remarks>
    public enum ePhenomFormat
    {
      /// <summary>
      /// True if there are any phenoms items.
      /// </summary>
      ArePhenomsPresent = 0,
      /// <summary>
      /// PhenomItemFormat. <see cref="ePhenomItemFormat"/>
      /// </summary>
      PhenomItemFormat = 1
    }

    /// <summary>
    /// Phenom item for info-string. <see cref="ENG.Metar.Decoder.ePhenomCollection"/>
    /// </summary>
    /// <remarks>
    /// This represents the one phenomen, like "+", "RA", "BR", "-", "SN" or "TS".
    /// </remarks>
    public enum ePhenomItemFormat
    {
      /// <summary>
      /// Phenom item as short string, e.g. SN
      /// </summary>
      PhenomItemAbbreviation = 0,
      /// <summary>
      /// Phenom item as string text, e.g. "snow".
      /// </summary>
      PhenomItemAsString = 1
    }

    /// <summary>
    /// Clouds values for info-string. <see cref="ENG.Metar.Decoder.CloudInfo"/>
    /// </summary>
    public enum eCloudsFormat
    {
      /// <summary>
      /// True if no significant clouds.
      /// </summary>
      IsNSC = 0,
      /// <summary>
      /// True if sky is clear.
      /// </summary>
      IsSKC = 1,
      /// <summary>
      /// Value of vertical visibility if used, null otherwise.
      /// </summary>
      VerticalVisibilityDistanceOrNull = 2,
      /// <summary>
      /// CloudFormat if used, null otherwise. <see cref="eCloudFormat"/>
      /// </summary>
      CloudFormatOrNull = 3
    }

    /// <summary>
    /// Cloud values for info-string. <see cref="ENG.Metar.Decoder.Cloud"/>
    /// </summary>
    public enum eCloudFormat
    {
      /// <summary>
      /// Cloud type as short string (FEW, OVC, ...)
      /// </summary>
      Type = 0,
      /// <summary>
      /// Cloud type as long string (few, overcast, ...)
      /// </summary>
      TypeAsLongString = 1,
      /// <summary>
      /// Altitude in hundreds feet, e.g. 020. Formatted to "000".
      /// </summary>
      AltitudeInHundreds = 2,
      /// <summary>
      /// Altitude in feet, e.g. 8000.
      /// </summary>
      Altitude = 3,
      /// <summary>
      /// True if CB.
      /// </summary>
      IsCB = 4,
      /// <summary>
      /// True if cloud is towering cumulus.
      /// </summary>
      IsTCU = 5
    }

    /// <summary>
    /// Pressure values for info-string. <see cref="ENG.Metar.Decoder.PressureInfo"/>
    /// </summary>
    public enum ePressureFormat
    {
      /// <summary>
      /// Pressure value in mmHq.
      /// </summary>
      ValueInMMHQ = 0,
      /// <summary>
      /// Pressure value in hPa.
      /// </summary>
      ValueInHPA = 1,
      /// <summary>
      /// Pressure value in current pressure unit (as loaded from metar os set by programmer).
      /// </summary>
      ValueInCurrentUnit = 2,
      /// <summary>
      /// Current unit as short string (hPa, ...).
      /// </summary>
      CurrentUnit = 3,
      /// <summary>
      /// Current unit as long string (hectopascals, ...)
      /// </summary>
      CurrentUnitLongString = 4
    }

    /// <summary>
    /// Runway conditions format values for info-string. <see cref="ENG.Metar.Decoder.RunwayConditionInfo"/>
    /// </summary>
    public enum eRunwayConditionsFormat
    {
      /// <summary>
      /// True if airport is closed due to snow. (SNOCLO).
      /// </summary>
      IsSNOCLO = 0,
      /// <summary>
      /// True if there are some condition info about runways.
      /// </summary>
      AreRunwayConditionsPresent = 1,
      /// <summary>
      /// RunwayConditionFormat or null if empty. <see cref="eRunwayConditionFormat"/>
      /// </summary>
      RunwayConditionFormatOrNull = 2
    }

    /// <summary>
    /// Runway condition format values for info-string. <see cref="ENG.Metar.Decoder.RunwayCondition"/>
    /// </summary>
    public enum eRunwayConditionFormat
    {
      /// <summary>
      /// True if this set is for all runways
      /// </summary>
      IsForAllRunways = 0,
      /// <summary>
      /// True if information is obsolete (from prevous metar).
      /// </summary>
      IsObsolete = 1,
      /// <summary>
      /// True if runway is cleared.
      /// </summary>
      IsCleared = 2,
      /// <summary>
      /// Runway designator.
      /// </summary>
      RunwayDesignator = 3,
      /// <summary>
      /// Deposit value as represented in metar, or null if empty (//).
      /// </summary>
      DepositNumOrNull = 4,
      /// <summary>
      /// Deposit value as string, special value if empty.
      /// </summary>
      DepositStringNeverNull = 5,
      /// <summary>
      /// Contamination value as represented in metar, or null if empty (//).
      /// </summary>
      ContaminationNumOrNull = 6,
      /// <summary>
      /// Contamination  value as string, special value if empty.
      /// </summary>
      ContaminationStringNeverNull = 7,
      /// <summary>
      /// Depth value as represented in metar, or null if empty (//).
      /// </summary>
      DepthNumOrNull = 8,
      /// <summary>
      /// Depth  value as string, special value if empty.
      /// </summary>
      DepthStringNeverNull = 9,
      /// <summary>
      /// Friction/braking action value as represented in metar, or null if empty (//).
      /// </summary>
      FrictionNumOrNull = 10,
      /// <summary>
      /// Friction/braking action value as string, special value if empty.
      /// </summary>
      FrictionNumNeverNull = 11
    }

    /// <summary>
    /// Windshears values for info-string. <see cref="ENG.Metar.Decoder.WindShearInfo"/>
    /// </summary>
    public enum eWindShearsFormat
    {
      /// <summary>
      /// True if windshear info is not empty.
      /// </summary>
      IsNonEmpty = 0,
      /// <summary>
      /// True if windshear warning is for all runways.
      /// </summary>
      IsForAllRunways = 1,
      /// <summary>
      /// True if some runway WS warning is present.
      /// </summary>
      IsSomeRunwaysPresent = 2,
      /// <summary>
      /// WindShearFormat or null if no WS runway warning is present. <see cref="eWindShearFormat"/>
      /// </summary>
      WindShearFormatOrNull = 3
    }

    /// <summary>
    /// Windshear value for info-string. <see cref="ENG.Metar.Decoder.WindShear"/>
    /// </summary>
    public enum eWindShearFormat
    {
      /// <summary>
      /// Runway designator.
      /// </summary>
      RunwayDesignator = 0
    }

    /// <summary>
    /// Trend values for info-string. <see cref="ENG.Metar.Decoder.TrendInfo"/>
    /// </summary>
    public enum eTrendFormat
    {
      /// <summary>
      /// True if trend is no-significatn-change (NOSIG).
      /// </summary>
      IsNOSIG = 0,
      /// <summary>
      /// Trend type as short string (NOSIG/BECMG, ...)
      /// </summary>
      TrendType = 1,
      /// <summary>
      /// Trend type as long string (no significant change, ... )
      /// </summary>
      TrendTypeLongString = 2,
      /// <summary>
      /// True if trend times are present.
      /// </summary>
      AreTrendTimesPresent = 3,
      /// <summary>
      /// Trend times format, or null if not present. <see cref="eTrendTimesFormat"/>
      /// </summary>
      TrendTimesFormatOrNull = 4,
      /// <summary>
      /// Trend wind format, or null if not present. <see cref="eWindFormat"/>
      /// </summary>
      WindFormatOrNull = 5,
      /// <summary>
      /// Trend visibility format, or null if not present. <see cref="eVisibilityFormat"/>
      /// </summary>
      VisibilityFormatOrNull = 6,
      /// <summary>
      /// Trend phenomens format, or null if not present. <see cref="ePhenomsFormat"/>
      /// </summary>
      PhenomsFormatOrNull = 7,
      /// <summary>
      /// Cloud info format, or null if not present. <see cref="eCloudFormat"/>
      /// </summary>
      CloudsFormatOrNull = 8
    }

    /// <summary>
    /// Trend times values for info-string. <see cref="ENG.Metar.Decoder.TrendTimeInfo"/>
    /// </summary>
    public enum eTrendTimesFormat
    {
      /// <summary>
      /// Trend time format or null if empty or not present.  <see cref="eTrendTimeFormat"/>
      /// </summary>
      TrendTimeFormatOrNull = 0
    }

    /// <summary>
    /// Trend time values for info-string <see cref="ENG.Metar.Decoder.TrendTime"/>
    /// </summary>
    public enum eTrendTimeFormat
    {
      /// <summary>
      /// Trend time type as short string (AT, FM, ...)
      /// </summary>
      TimeType = 0,
      /// <summary>
      /// Trend time type as long string (at, from, ...)
      /// </summary>
      TimeTypeLongString = 1,
      /// <summary>
      /// Trend time hour.
      /// </summary>
      Hour = 2,
      /// <summary>
      /// Trend time minute, formatted to "00".
      /// </summary>
      Minute = 3
    }

    #endregion Enums

    #region Format properties

    /// <summary>
    /// Gets the metar format string.
    /// </summary>
    /// <value></value>
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

    #endregion Format properties

    #region ToString methods

    /// <summary>
    /// Converts enum to string.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="useLong"></param>
    /// <returns></returns>
    public abstract string eDirectionToString  (Common.eDirection value, bool useLong);

    /// <summary>
    /// Converts enum to string.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public abstract string RunwayVisibilityDeviceMeasureRestrictionToString(RunwayVisibility.eDeviceMeasurementRestriction? value);

    /// <summary>
    /// Converts enum to string.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public abstract string RunwayVisibilityTendencyToString(RunwayVisibility.eTendency? value);

    /// <summary>
    /// Converts enum to string.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="useLong"></param>
    /// <returns></returns>
    public abstract string CloudTypeToString(Cloud.eType value, bool useLong);

    /// <summary>
    /// Converts enum to string.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="useLong"></param>
    /// <returns></returns>
    public abstract string PressureInfoUnitToString(PressureInfo.eUnit value, bool useLong);

    /// <summary>
    /// Converts enum to string.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="useLong"></param>
    /// <returns></returns>
    public abstract string TrendInfoTypeToString(TrendInfo.eType value, bool useLong);

    /// <summary>
    /// Converts enum to string.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public abstract string RunwayConditionContaminationToString(RunwayCondition.eContamination? value);

    /// <summary>
    /// Converts enum to string.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public abstract string RunwayConditionDepthToString(RunwayCondition.eDepth? value);

    /// <summary>
    /// Converts enum to string.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public abstract string RunwayConditionDepositToString(RunwayCondition.eDeposit? value);

    /// <summary>
    /// Converts enum to string.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public abstract string RunwayConditionFrictionToString(RunwayCondition.eFriction? value);

    /// <summary>
    /// Converts enum to string.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="useLong"></param>
    /// <returns></returns>
    public abstract string PhenomCollectionPhenomToString(ePhenomCollection.ePhenom value, bool useLong);

    /// <summary>
    /// Converts enum to string.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="useLong"></param>
    /// <returns></returns>
    public abstract string eUnitToString(Common.eUnit value, bool useLong);


    #endregion ToString methods


    #region Methods

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

    /// <summary>
    /// Cleans out assembled info string from ugly phenomens, like double dots .. at end of sentences, etc.
    /// See parameters.
    /// </summary>
    /// <param name="info">Text to clear.</param>
    /// <param name="addSpacesAfterPunctuation">True to removes spaces before . , ;</param>
    /// <param name="removeDoubleSpaces">True to replaces double spaces by single space</param>
    /// <param name="updateCasing">True to set upper case after dot (.).</param>
    /// <param name="updatePunctuation">True to remove first char from combinations like ,. ,; ;. and doubles, like ,, .. ;; </param>
    /// <returns></returns>
    public static string CleanOutInfo(string info,
     bool removeDoubleSpaces, bool updatePunctuation,
     bool addSpacesAfterPunctuation, bool updateCasing)
    {
      StringBuilder ret = new StringBuilder(info);

      if (removeDoubleSpaces)
        while (ret.ToString().Contains("  "))
          ret.Replace("  ", " ");

      if (updatePunctuation)
      {
        // mezery pred znaky
        ret.Replace(" .", ".");
        ret.Replace(" ,", ",");
        ret.Replace(" ;", ";");

        // double-znaky
        ret.Replace(",,", ",");
        ret.Replace("..", ".");
        ret.Replace(";;", ";");

        // double-kombinace
        ret.Replace(",.", ".");
        ret.Replace(";.", ".");
        ret.Replace(",;", ";");
      }

      if (addSpacesAfterPunctuation)
      {
        for (int i = 0; i < ret.Length - 1; i++)
        {
          if (ret[i].IsIn(',', '.', ';'))
            if (ret[i + 1] != ' ' && !Char.IsDigit(ret[i + 1]))
              ret.Insert(i + 1, ' ');
        }
      }

      if (updateCasing)
      {
        int i = 0;
        bool nextUpper = false;
        while (i < ret.Length - 2)
        {
          if (nextUpper)
            if (ret[i].IsNotIn(',', '.', ';', ' '))
            {
              ret[i] = Char.ToUpper(ret[i]);
              nextUpper = false;
            }

          if (ret[i] == '.')
            nextUpper = true;
          i++;
        }
      }

      return ret.ToString();
    }

    #endregion Methods

  }
}
