using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ENG.Metar.Decoder
{
  public abstract class InfoFormatter
  {
    /*
     * METAR:
     * 0 - icao
     * 1 - date-day
     * 2 - date-hour
     * 3 - date-minute
     * 4 - temperature
     * 5 - dew-point
     * 6 - wind
     * 
     * WIND:
     * 0 - direction
     * 1 - direction as N/NE/...
     * 2 - wind speed
     * 3 - wind speed unit
     * 4 - wind-gust-speed, if none, wind speed is used
     * 5 - WIND-VARIES
     *
     * WIND-VARIES:
     * 0 - from heading
     * 1 - from heading direction (N, NE, ...)
     * 2 - to heading
     * 3 - to heading direction (N, NE, ...)
     * 
     * VISIBILITY
     * 0 - distance
     * 1 - distance unit
     * 2 - distance direction (if any)
     * 3 - NOT SUPPORTED is minimum distance
     * 4 - other distance (if used)
     * 5 - other distance direciton (if other distance used)
     * 
     * RUNWAYS-VISIBILITY
     *  0 (iter) - RUNWAY-VISIBILITY
     * 
     * RUNWAY-VISIBILITY
     * 
     * */

    public static const string COND_START = "^";
    public static const string COND_END = "|";

    public abstract string MetarFormat { get; }

    public abstract string WindFormat { get; }
    public abstract string WindGustingFormat { get;  }
    public abstract string WindVaryingFormat { get; }

    public abstract string VisibilityFormat { get;  }
    public abstract string VisibilityWithOtherDistanceFormat { get; }
    public abstract string VisibilityClearFormat { get; }

    public abstract string RunwaysVisibilityFormat { get; }
    public abstract string RuwnayVisibilityFormat { get; }

    public string Normalize(string value)
    {
      value = cycleRem(value, "  ", " ");

      value = cycleRem(value, " .", ".");
      value = cycleRem(value, " ,", ",");

      value = cycleRem(value, ",.", ".");
      value = cycleRem(value, ";.", ".");

      value = cycleRem(value, ",,", ",");
      value = cycleRem(value, "..", ".");
      value = cycleRem(value, ";;", ";");

      return value;
    }
    private string cycleRem(string value,string oldString, string newString)
    {
      while (value.Contains(oldString))
        value = value.Replace(oldString, newString);

      return value;
    }

    private static void DecodeConditionalRegex(string conditionalRegex, out string clearRegex, out Dictionary<int, string> conditions)
    {
      string rgx = @"({\d+)(:(\d))?(})";
      conditions = new Dictionary<int, string>();
      int i = 0;

      Match m = Regex.Match(conditionalRegex, rgx);
      while (m.Success)
      {
        if (m.Groups[2].Success)
          conditions.Add(i, m.Groups[3].Value);

        i++;
        m = m.NextMatch();
      }

      clearRegex = Regex.Replace(conditionalRegex, rgx, "$1$4");
    }
    private static void ApplyRegexReplaces(string regex, object [] replaces, Dictionary<int, string> conditions)
    {
      string rgx = "{(.+)}";
      int index = 0;

      Match m = Regex.Match(regex, rgx);

      while (m.Success)
      {
        if ((
          (conditions.ContainsKey(index))
          &&
          (IsConditionTrue (object[index])))
        {
        }
        else
        {
        }

        m = m.NextMatch();
      }
    }
  }
}
