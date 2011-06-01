#pragma warning disable 1591 // odstrani warning kvuli dokumentaci

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENG.Metar.Decoder.Formatters
{
  public class CzechLongInfoFormatter : InfoFormatter
  {

    public override string MetarFormat
    {
      get
      {
        return
        "METAR pro {0} vydán dne {1}, {2}:{3}Z. " +
        "{4} {5} {6} {7}" +
        "Teplota {8}°C. Rosný bod {9}°C. " +
        "{10} {11} {12}" +
        "{13}[14| Poznámky: {14}].";
      }
    }

    public override string WindFormat
    {
      get
      {
        return
          "[1|Bezvětří.]" +
          "[!1|Vítr [0|proměnlivý][!0|{2}] o rychlosti {5}{6}[7| v nárazech až {7}{6}] [9| proměnlivý od {9} do {10}].]";
      }
    }

    public override string VisibilityFormat
    {
      get
      {


        return
          "[0|Neomezená viditelnost.]" +
          "[!0|Viditelnost[5| maximálně] {1}  {3}[4| ({4})]. [8|{9}]]";
      }
    }

    public override string RunwayVisibilityFormat
    {
      get
      {

        string ret =
          "Runway {4} viditelnost [0| {0}] {1}[6| to {6}] {2}[5| {5}].";

        return ret;
      }
    }

    public override string CloudsFormat
    {
      get
      {
        string ret =
          "[0|Bez význačné oblačnosti.]" +
          "[1|Bez oblačnosti.]" +
          "[2|Vertikální viditelnost {2} metrů.]" +
          "[3|Oblačnost: {3}.]";

        return ret;
      }
    }

    public override string CloudFormat
    {
      get
      {

        string ret =
          " {3} ft {1}[4| cumulonimbus][5| towering cumulus]; ";

        return ret;
      }
    }

    public override string PressureFormat
    {
      get
      {

        string ret =
          "Tlak {2} {3}."
          ;

        return ret;
      }
    }

    public override string RunwayConditionsFormat
    {
      get
      {
        string ret =
          "[0|Letiště uzavřeno kvůli sněhu.]" +
          "[1|{2}]";

        return ret;
      }
    }

    public override string RunwayConditionFormat
    {
      get
      {
        string ret =
          "[0|Všechny ravneje jsou ][!0|Ravnej {3} je ]" +
          "[2|očištěna/y.]" +
         "[!2|" +
          "pokrytá[8| {9}][4| z {5}][6| na {7}][10|, {11}]. " +
          "]" +
          "[1|Tato informace může být zastaralá.]";

        return ret;


      }
    }

    public override string WindShearsFormat
    {
      get
      {

        string ret =
          "[0|" +
          "[1|Střih větru na všech ravnejích.]" +
          "[2|Střih větru na {3}.]" +
          "]";

        return ret;
      }
    }

    public override string WindShearFormat
    {
      get
      {
        string ret = "ranveji {0}";

        return ret;
      }
    }

    public override string PhenomsFormat
    {
      get
      {
        string ret =
          "[0|Počasí bez význačných jevů.][1|Počasí: {2}.]";

        return ret;
      }
    }

    public override string RePhenomsFormat
    {
      get
      {
        string ret =
          "[0|Dříve bez význačných jevů.][1|Předchozí počasí: {2}.]";

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
          "[0| Trend počasí: žádné význačné změny.]" +
          "[!0|" +
          "Trend počasí {2} {4}: [5|{5}] [6|{6}] [7|{7}] [8|{8}]" +
          "]";

        return ret;
      }
    }

    public override string TrendTimesFormat
    {
      get
      {
        string ret = "{0}";

        return ret;
      }
    }

    public override string TrendTimeFormat
    {
      get
      {
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
          ret = useLong ? "sever" : "S";
          break;
        case Common.eDirection.E:
          ret = useLong ? "východ" : "V";
          break;
        case Common.eDirection.NE:
          ret = useLong ? "severovýchod" : "SV";
          break;
        case Common.eDirection.NW:
          ret = useLong ? "severozápad" : "SZ";
          break;
        case Common.eDirection.S:
          ret = useLong ? "jih" : "J";
          break;
        case Common.eDirection.SE:
          ret = useLong ? "jihovýchod" : "JV";
          break;
        case Common.eDirection.SW:
          ret = useLong ? "jihozápad" : "JZ";
          break;
        case Common.eDirection.W:
          ret = useLong ? "západ" : "Z";
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
            ret = "maximálně";
            break;
          case RunwayVisibility.eDeviceMeasurementRestriction.P:
            ret = "minimálně";
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
            ret = "klesající";
            break;
          case RunwayVisibility.eTendency.N:
            ret = "stabilní";
            break;
          case RunwayVisibility.eTendency.U:
            ret = "vzrůstající";
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
          ret = useLong ? "oblačno až skoro zataženo" : "BKN";
          break;
        case ENG.Metar.Decoder.Cloud.eType.FEW:
          ret = useLong ? "skoro jasno" : "FEW";
          break;
        case ENG.Metar.Decoder.Cloud.eType.OVC:
          ret = useLong ? "zataženo" : "OVC";
          break;
        case ENG.Metar.Decoder.Cloud.eType.SCT:
          ret = useLong ? "polojasno" : "SCT";
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
          ret = useLong ? "hectopascalů" : "hPa";
          break;
        case PressureInfo.eUnit.mmHq:
          ret = useLong ? "mm of Hq" : "mmHq";
          break;
        default:
          throw new NotImplementedException();
      }
      return ret;
    }

    public override string TrendInfoTypeToString(TrendInfoForMetar.eType value, bool useLong)
    {
      string ret = null;

      switch (value)
      {
        case TrendInfoForMetar.eType.BECMG:
          ret = useLong ? "nastává" : "becmg";
          break;
        case TrendInfoForMetar.eType.NOSIG:
          ret = useLong ? "bez význačných změn" : "nosig";
          break;
        case TrendInfoForMetar.eType.TEMPO:
          ret = useLong ? "občasně" : "tempo";
          break;
        default:
          throw new NotImplementedException();
      }

      return ret;
    }

    public override string RunwayConditionContaminationToString(RunwayCondition.eContamination? value)
    {
      if (!value.HasValue)
        return "(kontaminace nehlášena)";
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
        return "(výška nánosu nehlášena)";
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
        return "(typ nánosu nehlášen)";
      else
      {
        string ret;
        switch (value)
        {
          case RunwayCondition.eDeposit.CleanDry:
            ret = "čistá suchá ranvej";
            break;
          case RunwayCondition.eDeposit.CompactSnow:
            ret = "pevný sníh";
            break;
          case RunwayCondition.eDeposit.DrySnow:
            ret = "suchý sníh";
            break;
          case RunwayCondition.eDeposit.FrozentRutsRidges:
            ret = "zmrzlé koleje";
            break;
          case RunwayCondition.eDeposit.Ice:
            ret = "led";
            break;
          case RunwayCondition.eDeposit.RimeOrFrost:
            ret = "jinovatka/námraza";
            break;
          case RunwayCondition.eDeposit.Slush:
            ret = "rozbředlý sníh";
            break;
          case RunwayCondition.eDeposit.WetDamp:
            ret = "morký/nečištěný";
            break;
          case RunwayCondition.eDeposit.WetOrWetPatches:
            ret = "mokrý/mokré skvrny";
            break;
          case RunwayCondition.eDeposit.WetSnow:
            ret = "mokrý sníh";
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
        return "(brzdný účinek nehlášen)";
      else
      {
        if ((int)value.Value < 91)
          return "koeficient přilnavosti 0." + ((int)value.Value).ToString("00");
        else
        {
          StringBuilder ret = new StringBuilder();
          ret.AppendSpaced("brzdný účinek ");
          switch ((int)value)
          {
            case 91:
              ret.Append("špatný");
              break;
            case 92:
              ret.Append("střední až špatný");
              break;
            case 93:
              ret.Append("střední");
              break;
            case 94:
              ret.Append("dobrý až střední");
              break;
            case 95:
              ret.Append("dobrý");
              break;
            case 96:
            case 97:
            case 98:
              ret.Append("(nepoužito)");
              break;
            case 99:
              ret.Append("nespolehlivý");
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
          ret = "shluky";
          break;
        case ePhenomCollection.ePhenom.BL:
          ret = "foukající";
          break;
        case ePhenomCollection.ePhenom.BR:
          ret = "mlha";
          break;
        case ePhenomCollection.ePhenom.DR:
          ret = "nízko letící";
          break;
        case ePhenomCollection.ePhenom.DS:
          ret = "prachová bouře";
          break;
        case ePhenomCollection.ePhenom.DU:
          ret = "prach";
          break;
        case ePhenomCollection.ePhenom.DZ:
          ret = "mrholení";
          break;
        case ePhenomCollection.ePhenom.FC:
          ret = "nálevkovitý mrak";
          break;
        case ePhenomCollection.ePhenom.FG:
          ret = "mlha";
          break;
        case ePhenomCollection.ePhenom.FU:
          ret = "kouř";
          break;
        case ePhenomCollection.ePhenom.FZ:
          ret = "mrznoucí";
          break;
        case ePhenomCollection.ePhenom.GR:
          ret = "krupobití";
          break;
        case ePhenomCollection.ePhenom.GS:
          ret = "sněhové kuličky";
          break;
        case ePhenomCollection.ePhenom.Heavy:
          ret = "silný";
          break;
        case ePhenomCollection.ePhenom.HZ:
          ret = "kouřmo";
          break;
        case ePhenomCollection.ePhenom.IC:
          ret = "ledové krystalky";
          break;
        case ePhenomCollection.ePhenom.Light:
          ret = "slabý";
          break;
        case ePhenomCollection.ePhenom.MI:
          ret = "mělký";
          break;
        case ePhenomCollection.ePhenom.PL:
          ret = "ledové kuličky";
          break;
        case ePhenomCollection.ePhenom.PO:
          ret = "prachové nebo sněhové víry";
          break;
        case ePhenomCollection.ePhenom.PR:
          ret = "částečný";
          break;
        case ePhenomCollection.ePhenom.RA:
          ret = "déšť";
          break;
        case ePhenomCollection.ePhenom.SA:
          ret = "písek";
          break;
        case ePhenomCollection.ePhenom.SG:
          ret = "sněhová zrna";
          break;
        case ePhenomCollection.ePhenom.SH:
          ret = "přeháňka";
          break;
        case ePhenomCollection.ePhenom.SN:
          ret = "sníh";
          break;
        case ePhenomCollection.ePhenom.SQ:
          ret = "hromobití";
          break;
        case ePhenomCollection.ePhenom.SS:
          ret = "písečná bouře";
          break;
        case ePhenomCollection.ePhenom.TS:
          ret = "bouřka";
          break;
        case ePhenomCollection.ePhenom.VA:
          ret = "vulkanický popel";
          break;
        case ePhenomCollection.ePhenom.VC:
          ret = "v blízkosti";
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
          ret = useLong ? "stop" : "ft";
          break;
        case Common.eDistanceUnit.km:
          ret = useLong ? "kilometrů" : "km";
          break;
        case Common.eDistanceUnit.m:
          ret = useLong ? "metrů" : "m";
          break;
        case Common.eDistanceUnit.mi:
          ret = useLong ? "mil" : "m";
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
          ret = useLong ? "km/h" : "km/h";
          break;
        case Common.eSpeedUnit.mps:
          ret = useLong ? "m/s" : "m/s";
          break;
        case Common.eSpeedUnit.kt:
          ret = useLong ? "uzlů" : "kt(s)";
          break;
        default:
          throw new NotImplementedException();
      }

      return ret;
    }
  }
}
