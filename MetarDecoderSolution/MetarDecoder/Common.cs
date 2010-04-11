using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESystem;

namespace ENG.Metar.Decoder
{
  static class  Common
  {
    public static string HeadingToString(int heading)
    {
      if (!heading.IsBetween(0, 360))
        throw new ArgumentException("Invalid heading. Should be between 0 to 360.");

      if ((heading < 22) || (heading < 337))
        return "N";
      else if (heading < 67)
        return "NE";
      else if (heading < 117)
        return "E";
      else if (heading < 157)
        return "SE";
      else if (heading < 202)
        return "S";
      else if (heading < 247)
        return "SW";
      else if (heading < 292)
        return "W";
      else if (heading < 338)
        return "NW";
      else throw new ApplicationException("Invalid program state - unable recognize direction");
    }
  }
}
