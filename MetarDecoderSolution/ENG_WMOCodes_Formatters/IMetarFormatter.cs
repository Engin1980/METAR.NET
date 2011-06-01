using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENG.WMOCodes.Formatters
{
  public interface IMetarFormatter
  {
    string ToString(ENG.WMOCodes.Codes.Metar metar);
  }
}
