#pragma warning disable 1591 // odstrani warning kvuli dokumentaci

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENG.Metar.Decoder.Formatters
{
  /// <summary>
  /// Represents formatter to convert metar into long information string.
  /// </summary>
  /// <seealso cref="T:ENG.Metar.Decoder.InfoFormatter"/>
  public class LongInfoFormatter : InfoFormatter
  {

    public override string MetarFormat
    {
      get {
        return
        "METAR for {0} issued at day {1}, {2}:{3}Z. " +
        "{4} {5} {6} {7}" +
        "Temperature {8}°C. Dew point {9}°C. " +
        "{10} {11} {12}"+
        "{13}[14| Remark: {14}].";
      }
    }

    public override string WindFormat
    {
      get {
        return 
          "[1|Wind calm.]" +
          "[!1|Wind [0|variable][!0|{2}] at {5}{6}[7| gusting to {7}{6}] [9| varying between {9} and {10}].]";
      }
    }
    
    public override string VisibilityFormat
    {
      get {


        return
          "[0|Visibility unlimited.]" +
          "[!0|Visibility[5| at most] {1}  {3}[4| ({4})]. [8|{9}]]";
      }
    }

    public override string RunwayVisibilityFormat
    {
      get {

        /*
 * 
 * RUNWAY-VISIBILITY
 * 0 - device measure restrition string, or null
 * 1 - distance
 * 2 - distance unit (short)
 * 3 - distance unit (long)
 * 4 - runway designator
 * 5 - tendency as string, or null
 * 6 - variable visibility distance, or null
   
 */

        string ret =
          "Runway {4} visibility [0| {0}] {1}[6| to {6}] {2}[5| {5}].";

        return ret;
      }
    }

    public override string CloudsFormat
    {
      get
      {
        /* CLOUDS INFO
 * 0 - true if NSC, or false
 * 1 - true if SKC, or false
 * 2 - distance if vertical visibility, or null
 * 3 - (iter) CLOUD info, or null if empty
 * */

        string ret =
          "[0|No significant clouds.]" +
          "[1|Sky clear.]" +
          "[2|Vertical visibility {2} meters.]" +
          "[3|Clouds: {3}.]";

        return ret;
      }
    }

    public override string CloudFormat
    {
      get {
        /* CLOUD
 * 0 - type (short)
 * 1 - type (long)
 * 2 - altitude in hundreds formatted to 000
 * 3 - altitude in number
 * 4 - true if CB
 * 5 - true if TCU
 * */

        string ret =
          " {3} ft {1}[4| cumulonimbus][5| towering cumulus]; ";

        return ret;
      }
    }

    public override string PressureFormat
    {
      get { 

          /* PRESSURE
   * 0 - value in mmHq
   * 1 - value in hPa
   * 2 - value in current unit
   * 3 - current unit (short)
   * 4 - current unit (long)
   * */

        string ret =
          "Current pressure {2} {3}."
          ;

        return ret;
      }
    }

    public override string RunwayConditionsFormat
    {
      get {
        string ret =
          "[0|Airport is closed due to snow.]" +
          "[1|{2}]";

        return ret;
      }
    }

    public override string RunwayConditionFormat
    {
      get {
        /* RUNWAY CONDITION INFO
   * 0 - true if is for all runways
   * 1 - true if info is obsolete
   * 2 - true if runway is cleared
   * 3 - runway designator
   * 4 - deposit int value, or null
   * 5 - deposit description (never null)
   * 6 - contamination int value, or null
   * 7 - contamination string (never null)
   * 8 - depth int value, or null
   * 9 - depth string (never null)
   * 10 - friction int value, or null
   * 11 - friction string (never null)
   * */

        string ret =
          "[0|All runways are ][!0|Runway {3} is ]" +
          "[2|cleared.]" +
         "[!2|" +
          "covered[8| by {9}][4| of {5}][6| over {7}][10|, {11}]. " +
          "]" +
          "[1|This information can be obsolete.]";

        return ret;
        
      
      }
    }

    public override string WindShearsFormat
    {
      get {

        string ret =
          "[0|" + 
          "[1|Windshear reported at all runways.]" +
          "[2|Windshear reported at {3}.]" +
          "]";

        return ret;
      }
    }

    public override string WindShearFormat
    {
      get {
        string ret = "runway {0}, ";

        return ret;
      }
    }

    public override string PhenomsFormat
    {
      get {
        string ret =
          "[0|No significat weather.][1|Weather: {2}.]";

        return ret;
      }
    }

    public override string RePhenomsFormat
    {
      get
      {
        string ret =
          "[0|No previous significant weather.][1|Previous weather: {2}.]";

        return ret;
      }
    }

    public override string PhenomFormat
    {
      get
      {
        string ret =
          "[0|{1}; ]";

        return ret;
      }
    }

    public override string PhenomItemFormat
    {
      get
      {
        string ret =
          "{1} ";
        return ret;
      }
    }

    public override string TrendFormat
    {
      get
      {
        string ret =
          "[0| No significant change expected.]" +
          "[!0|" +
          "Weather trend {2} {4}: [5|{5}] [6|{6}] [7|{7}] [8|{8}]" +
          "]";

        return ret;
      }
    }   

    public override string TrendTimesFormat
    {
      get {
        string ret = "{0}";

        return ret;
      }
    }

    public override string TrendTimeFormat
    {
      get {
        string ret =
          "{1} {2}:{3}Z ";

        return ret;
      }
    }

    public override string eDirectionToString(Common.eDirection value, bool useLong)
    {
      string ret = null;

      switch (value)
      {
        case Common.eDirection.N:
          ret = useLong ? "north" : "N";
          break;
        case Common.eDirection.E:
          ret = useLong ? "east" : "E";
          break;
        case Common.eDirection.NE:
          ret = useLong ? "northeast" : "NE";
          break;
        case Common.eDirection.NW:
          ret = useLong ? "northwest" : "NW";
          break;
        case Common.eDirection.S:
          ret = useLong ? "south" : "S";
          break;
        case Common.eDirection.SE:
          ret = useLong ? "southeast" : "SE";
          break;
        case Common.eDirection.SW:
          ret = useLong ? "southwest" : "SW";
          break;
        case Common.eDirection.W:
          ret = useLong ? "west" : "W";
          break;
        default:
          throw new NotImplementedException();
      }

      return ret;
    }

    public override string RunwayVisibilityDeviceMeasureRestrictionToString(RunwayVisibility.eDeviceMeasurementRestriction? value)
    {
           string ret = "";
      if (value.HasValue)
        switch (value)
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

    public override string RunwayVisibilityTendencyToString(RunwayVisibility.eTendency? value)
    {
      string ret;
      if (value.HasValue)
        switch (value.Value)
        {
          case RunwayVisibility.eTendency.D:
            ret = "decreasing";
            break;
          case RunwayVisibility.eTendency.N:
            ret = "stable";
            break;
          case RunwayVisibility.eTendency.U:
            ret = "increasing";
            break;
          default:
            throw new NotImplementedException();
        }
      else
        ret = null;

      return ret;
    }

    public override string CloudTypeToString(Cloud.eType type, bool useLong)
    {
      string ret;

      switch (type)
      {
        case ENG.Metar.Decoder.Cloud.eType.BKN:
          ret = useLong ? "broken" : "BKN";
          break;
        case ENG.Metar.Decoder.Cloud.eType.FEW:
          ret = useLong ? "few" : "FEW";
          break;
        case ENG.Metar.Decoder.Cloud.eType.OVC:
          ret = useLong ? "overcast" : "OVC";
          break;
        case ENG.Metar.Decoder.Cloud.eType.SCT:
          ret = useLong ? "scattered" : "SCT";
          break;
        default:
          throw new NotImplementedException();
      }

      return ret;
    }

    public override string PressureInfoUnitToString(PressureInfo.eUnit value, bool useLong)
    {
      string ret = null;

      switch (value)
      {
        case PressureInfo.eUnit.hPa:
          ret = useLong ? "hectopascals" : "hPa";
          break;
        case PressureInfo.eUnit.mmHq:
          ret = useLong ? "mm of Hq" : "mmHq";
          break;
        default:
          throw new NotImplementedException();
      }
      return ret;
    }

    public override string TrendInfoTypeToString(TrendInfo.eType value, bool useLong)
    {
      string ret = null;

      switch (value)
      {
        case TrendInfo.eType.BECMG:
          ret = useLong ? "becoming" : "becmg";
          break;
        case TrendInfo.eType.NOSIG:
          ret = useLong ?"no significant change" : "nosig";
          break;
        case TrendInfo.eType.TEMPO:
          ret = useLong ?"temporally" : "tempo";
          break;
        default:
          throw new NotImplementedException();
      }

      return ret;
    }

    public override string RunwayConditionContaminationToString(RunwayCondition.eContamination? value)
    {
      if (!value.HasValue)
        return "Contamination not reported";
      else
      {
        string ret;

        switch (value)
        {
          case RunwayCondition.eContamination.LessThan10Percent:
            ret = "0-10%";
            break;
          case RunwayCondition.eContamination.LessThan25Percent:
            ret = "10-25%";
            break;
          case RunwayCondition.eContamination.LessThan50Percent:
            ret = "25-50%";
            break;
          case RunwayCondition.eContamination.MoreThan50Percent:
            ret = "50-100%";
            break;
          default:
            ret = "N/A";
            break;
        }

        return ret;
      }
    }

    public override string RunwayConditionDepthToString(RunwayCondition.eDepth? value)
    {
      if (!value.HasValue)
        return "(depth not reported)";
      else
      {
        if ((int)value < 91)
          return ((int)value).ToString() + "mm";
        else
          return (((int)value - 90) * 5) + "cm";
      }
    }

    public override string RunwayConditionDepositToString(RunwayCondition.eDeposit? value)
    {
      if (!value.HasValue)
        return "unreported deposit";
      else
      {
        string ret;
        switch (value)
        {
          case RunwayCondition.eDeposit.CleanDry:
            ret = "clean dry runway";
            break;
          case RunwayCondition.eDeposit.CompactSnow:
            ret = "compact snow";
            break;
          case RunwayCondition.eDeposit.DrySnow:
            ret = "dry snow";
            break;
          case RunwayCondition.eDeposit.FrozentRutsRidges:
            ret = "frozen ruts/ridges";
            break;
          case RunwayCondition.eDeposit.Ice:
            ret = "ice";
            break;
          case RunwayCondition.eDeposit.RimeOrFrost:
            ret = "rime/frost";
            break;
          case RunwayCondition.eDeposit.Slush:
            ret = "slush";
            break;
          case RunwayCondition.eDeposit.WetDamp:
            ret = "wet/damp";
            break;
          case RunwayCondition.eDeposit.WetOrWetPatches:
            ret = "wet/wet patches";
            break;
          case RunwayCondition.eDeposit.WetSnow:
            ret = "wet snow";
            break;
          default:
            throw new NotSupportedException();
        }

        return ret;
      }
    }

    public override string RunwayConditionFrictionToString(RunwayCondition.eFriction? value)
    {
      if (!value.HasValue)
        return "braking action not reported";
      else
      {
        if ((int)value.Value < 91)
          return "friction coefficient 0." + ((int) value.Value).ToString("00");
        else
        {
          StringBuilder ret = new StringBuilder();
          ret.AppendSpaced("braking action ");
          switch ((int)value)
          {
            case 91:
              ret.Append("poor");
              break;
            case 92:
              ret.Append("medium-poor");
              break;
            case 93:
              ret.Append("medium");
              break;
            case 94:
              ret.Append("medium-good");
              break;
            case 95:
              ret.Append("good");
              break;
            case 96:
            case 97:
            case 98:
              ret.Append("(reserved value)");
              break;
            case 99:
              ret.Append("unreliable");
              break;
            default:
              throw new NotSupportedException();
          }

          return ret.ToString();
        }
      }
    }

    public override string PhenomCollectionPhenomToString(ePhenomCollection.ePhenom value, bool useLong)
    {
      if (!useLong)
        return value.ToString();

      string ret = null;

      switch (value)
      {
        case ePhenomCollection.ePhenom.BC:
          ret = "patches";
          break;
        case ePhenomCollection.ePhenom.BL:
          ret = "blowing";
          break;
        case ePhenomCollection.ePhenom.BR:
          ret = "mist";
          break;
        case ePhenomCollection.ePhenom.DR:
          ret = "low drifting";
          break;
        case ePhenomCollection.ePhenom.DS:
          ret = "dust storm";
          break;
        case ePhenomCollection.ePhenom.DU:
          ret = "dust";
          break;
        case ePhenomCollection.ePhenom.DZ:
          ret = "drizzle";
          break;
        case ePhenomCollection.ePhenom.FC:
          ret = "funnel cloud";
          break;
        case ePhenomCollection.ePhenom.FG:
          ret = "fog";
          break;
        case ePhenomCollection.ePhenom.FU:
          ret = "smoke";
          break;
        case ePhenomCollection.ePhenom.FZ:
          ret = "freezing";
          break;
        case ePhenomCollection.ePhenom.GR:
          ret = "hail";
          break;
        case ePhenomCollection.ePhenom.GS:
          ret = "snow pellets";
          break;
        case ePhenomCollection.ePhenom.Heavy:
          ret = "heavy";
          break;
        case ePhenomCollection.ePhenom.HZ:
          ret = "haze";
          break;
        case ePhenomCollection.ePhenom.IC:
          ret = "ice crystals";
          break;
        case ePhenomCollection.ePhenom.Light:
          ret = "light";
          break;
        case ePhenomCollection.ePhenom.MI:
          ret = "shallow";
          break;
        case ePhenomCollection.ePhenom.PL:
          ret = "ice pellets";
          break;
        case ePhenomCollection.ePhenom.PO:
          ret = "dust or sand whirls";
          break;
        case ePhenomCollection.ePhenom.PR:
          ret = "partial";
          break;
        case ePhenomCollection.ePhenom.RA:
          ret = "rain";
          break;
        case ePhenomCollection.ePhenom.SA:
          ret = "sand";
          break;
        case ePhenomCollection.ePhenom.SG:
          ret = "snow grains";
          break;
        case ePhenomCollection.ePhenom.SH:
          ret = "shower";
          break;
        case ePhenomCollection.ePhenom.SN:
          ret = "snow";
          break;
        case ePhenomCollection.ePhenom.SQ:
          ret = "squalls";
          break;
        case ePhenomCollection.ePhenom.SS:
          ret = "sand storm";
          break;
        case ePhenomCollection.ePhenom.TS:
          ret = "thunderstorm";
          break;
        case ePhenomCollection.ePhenom.VA:
          ret = "volcanic ash";
          break;
        case ePhenomCollection.ePhenom.VC:
          ret = "in vicinity";
          break;
        default:
          throw new NotImplementedException();
      }

      return ret;
    }

    public override string eDistanceUnitToString(Common.eDistanceUnit value, bool useLong)
    {
      string ret = "";

      switch (value)
      {
        case Common.eDistanceUnit.ft:
          ret = useLong ? "feet" : "ft";
          break;
        case Common.eDistanceUnit.km:
          ret = useLong ? "kilometer(s)" : "km";
          break;
        case Common.eDistanceUnit.m:
          ret = useLong ? "meter(s)" : "m";
          break;
        case Common.eDistanceUnit.mi:
          ret = useLong ? "mile(s)" : "m";
          break;
        default:
          throw new NotImplementedException();
      }

      return ret;
    }

    public override string eSpeedUnitToString(Common.eSpeedUnit value, bool useLong)
    {
      string ret = "";

      switch (value)
      {
        case Common.eSpeedUnit.kph:
          ret = useLong ? "kph" : "kph";
          break;
        case Common.eSpeedUnit.mps:
          ret = useLong ? "mps" : "mps";
          break;
        case Common.eSpeedUnit.kt:
          ret = useLong ? "knot(s)" : "kt(s)";
          break;
        default:
          throw new NotImplementedException();
      }

      return ret;
    }
  }
}
