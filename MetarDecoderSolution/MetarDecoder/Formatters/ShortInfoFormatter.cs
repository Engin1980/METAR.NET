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
  public class ShortInfoFormatter: InfoFormatter
  {

    public override string MetarFormat
    {
      get {
        string ret = "{0} - {2}:{3}Z: {4} {6} Temperature: {8}°C. {10}";

        return ret;
      }
    }

    public override string WindFormat
    {
      get
      {
        string ret =
           "[1|Wind calm.]" +
          "[!1|Wind [0|variable][!0|{2}] at {7} {5}.]";

        return ret;
      }
    }

    public override string VisibilityFormat
    {
      get { throw new NotImplementedException(); }
    }

    public override string RunwayVisibilityFormat
    {
      get { throw new NotImplementedException(); }
    }

    public override string CloudsFormat
    {
      get { throw new NotImplementedException(); }
    }

    public override string CloudFormat
    {
      get { throw new NotImplementedException(); }
    }

    public override string PressureFormat
    {
      get {
        return "Pressure {1} hPa.";
      }
    }

    public override string RunwayConditionsFormat
    {
      get { throw new NotImplementedException(); }
    }

    public override string RunwayConditionFormat
    {
      get { throw new NotImplementedException(); }
    }

    public override string WindShearsFormat
    {
      get { throw new NotImplementedException(); }
    }

    public override string WindShearFormat
    {
      get { throw new NotImplementedException(); }
    }

    public override string PhenomsFormat
    {
      get { return "[1|{2}.]"; }
    }

    public override string RePhenomsFormat
    {
      get { throw new NotImplementedException(); }
    }

    public override string PhenomFormat
    {
      get { return "{1},"; }
    }

    public override string PhenomItemFormat
    {
            get { return "{1} "; }
    }

    public override string TrendFormat
    {
      get { throw new NotImplementedException(); }
    }

    public override string TrendTimesFormat
    {
      get { throw new NotImplementedException(); }
    }

    public override string TrendTimeFormat
    {
      get { throw new NotImplementedException(); }
    }

    public override string eDirectionToString(Common.eDirection value, bool useLong)
    {
      return new LongInfoFormatter().eDirectionToString(value, useLong);
    }

    public override string RunwayVisibilityDeviceMeasureRestrictionToString(RunwayVisibility.eDeviceMeasurementRestriction? value)
    {
      return new LongInfoFormatter().RunwayVisibilityDeviceMeasureRestrictionToString(value);
    }

    public override string RunwayVisibilityTendencyToString(RunwayVisibility.eTendency? value)
    {
      return new LongInfoFormatter().RunwayVisibilityTendencyToString(value);
    }

    public override string CloudTypeToString(Cloud.eType value, bool useLong)
    {
      return new LongInfoFormatter().CloudTypeToString(value, useLong);
    }

    public override string PressureInfoUnitToString(PressureInfo.eUnit value, bool useLong)
    {
      return new LongInfoFormatter().PressureInfoUnitToString(value, useLong);
    }

    public override string TrendInfoTypeToString(TrendInfo.eType value, bool useLong)
    {
      return new LongInfoFormatter().TrendInfoTypeToString(value, useLong);
    }

    public override string RunwayConditionContaminationToString(RunwayCondition.eContamination? value)
    {
      return new LongInfoFormatter().RunwayConditionContaminationToString(value);
    }

    public override string RunwayConditionDepthToString(RunwayCondition.eDepth? value)
    {
      return new LongInfoFormatter().RunwayConditionDepthToString(value);
    }

    public override string RunwayConditionDepositToString(RunwayCondition.eDeposit? value)
    {
      return new LongInfoFormatter().RunwayConditionDepositToString(value);
    }

    public override string RunwayConditionFrictionToString(RunwayCondition.eFriction? value)
    {
      return new LongInfoFormatter().RunwayConditionFrictionToString(value);
    }

    public override string PhenomCollectionPhenomToString(ePhenomCollection.ePhenom value, bool useLong)
    {
      return new LongInfoFormatter().PhenomCollectionPhenomToString(value, useLong);
    }

    public override string eUnitToString(Common.eUnit value, bool useLong)
    {
      return new LongInfoFormatter().eUnitToString(value, useLong);
    }
  }
}
