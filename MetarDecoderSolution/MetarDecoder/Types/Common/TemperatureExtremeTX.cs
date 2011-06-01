using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENG.Metar.Decoder.Types.TAF
{
  public class TemperatureExtremeTX : TemperatureExtreme
  {
    public override string ToCode()
    {
      return "TX" + this.Temperature.ToString("00") + "/" + this.Time.ToCode() + "Z";
    }

    public override void SanityCheck(ref List<string> errors, ref List<string> warnings)
    {
      // nothing to do
    }
  }
}
