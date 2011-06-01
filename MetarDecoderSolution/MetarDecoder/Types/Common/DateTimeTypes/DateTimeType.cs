using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENG.Metar.Decoder.Types.Common.DateTimeTypes
{
  /// <summary>
  /// Represents common information about day-hour-minute.
  /// </summary>
public abstract  class DateTimeType : ICodeItem
  {
    #region ICodeItem Members

    /// <summary>
    /// Returns item in code string.
    /// </summary>
    /// <returns></returns>
    public abstract string ToCode();

    /// <summary>
    /// Proceed sanity check of inserted values.
    /// </summary>
    /// <param name="errors">Found errors.</param>
    /// <param name="warnings">Found warnings.</param>
    public abstract void SanityCheck(ref List<string> errors, ref List<string> warnings);

    #endregion
  }
}
