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
          "[8|Wind calm.]" +
          "[!8|Wind {0} at {2}{3}[4| gusting to {4}{3}] [6| varying between {6} and {7}].]";
      }
    }
    
    public override string VisibilityFormat
    {
      get {

        //VISIBILITY
        //0 - isClear
        //1 - distance
        //2 - distance unit
        //3 - distance unit long
        //4 - distance direction (if any), or null
        //5 - not used
        //6 - true if it is minimum measurable distance, or false
        //7 - other distance if used, or null
        //8 - other distance direction if other distance used, or null
        //9 - true if runwayVisibility definitions is present, false otherwise
        //10 - RUNWAY-VISIBILITY
        // 

        return
          "[0|Visibility unlimited.]" +
          "[!0|Visibility {1}[6| at most] {3}[4| ({4})]. [9|{10}]]";
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
        string ret = "runway {0}";

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
  }
}
