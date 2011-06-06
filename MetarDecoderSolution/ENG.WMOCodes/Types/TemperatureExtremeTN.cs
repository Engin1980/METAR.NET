using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENG.WMOCodes.Types
{
  /// <summary>
  /// Represents minimal temperature in trend TAF report.
  /// </summary>
  public class TemperatureExtremeTN : TemperatureExtreme
  {
    /// <summary>
    /// Toes the code.
    /// </summary>
    /// <returns></returns>
    public override string ToCode()
    {
      return "TN" + this.Temperature.ToString("00") + "/" + this.Time.ToCode() + "Z";
    }

    /// <summary>
    /// Sanities the check.
    /// </summary>
    /// <param name="errors">The errors.</param>
    /// <param name="warnings">The warnings.</param>
    public override void SanityCheck(ref List<string> errors, ref List<string> warnings)
    {
      // nothing to do
    }
  }
}
