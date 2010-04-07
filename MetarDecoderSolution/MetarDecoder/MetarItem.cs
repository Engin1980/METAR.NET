using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetarDecoder
{
  /// <summary>
  /// Common interface to describe metar element.
  /// </summary>
  public interface MetarItem
  {
    /// <summary>
    /// Returns item in metar string.
    /// </summary>
    /// <returns></returns>
    string ToMetar();
    ///// <summary>
    ///// Returns item in text string.
    ///// </summary>
    ///// <returns></returns>
    //string ToInfo();
    /// <summary>
    /// Proceed sanity check of inserted values.
    /// </summary>
    /// <param name="errors">Found errors.</param>
    /// <param name="warnings">Found warnings.</param>
    void SanityCheck(ref List<string> errors, ref List<string> warnings); 
  }
}

