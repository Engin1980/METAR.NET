using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENG.Metar.Decoder
{
  /// <summary>
  /// Common interface to describe metar element.
  /// </summary>
  public interface IMetarItem
  {
    /// <summary>
    /// Returns item in metar string.
    /// </summary>
    /// <returns></returns>
    string ToMetar();
    /// <summary>
    /// Proceed sanity check of inserted values.
    /// </summary>
    /// <param name="errors">Found errors.</param>
    /// <param name="warnings">Found warnings.</param>
    void SanityCheck(ref List<string> errors, ref List<string> warnings); 
  }
}

