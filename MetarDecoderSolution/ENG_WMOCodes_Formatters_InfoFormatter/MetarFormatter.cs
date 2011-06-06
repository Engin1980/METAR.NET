using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENG.WMOCodes.Formatters.InfoFormatter
{
  public partial class MetarFormatter : ENG.WMOCodes.Formatters.IMetarFormatter
  {
    #region IMetarFormatter Members

    public string ToString(Codes.Metar metar)
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}
