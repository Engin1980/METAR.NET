using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace ENG.WMOCodes.Formatters
{
  /// <summary>
  /// This interface defines method to implement class converting metar report to some different string.
  /// </summary>
  public abstract class MetarFormatter : ENG.WMOCodes.Formatters.IMetarFormatter
  {
    #region IMetarFormatter Members

    /// <summary>
    /// Converts metar report into information string.
    /// </summary>
    /// <param name="metar">The metar.</param>
    /// <returns>
    /// A <see cref="System.String"/> that represents this instance.
    /// </returns>
    public abstract string ToString(Codes.Metar metar);

    #endregion
  }
}
