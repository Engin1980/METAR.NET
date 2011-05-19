using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENG.Metar.Downloader
{
  /// <summary>
  /// Represents type able to define source for metar downloading. This type has a typo in name, so it is now 
  /// obsolete and will be replaced by IMetarRetriever with the "r" char at the end of line.
  /// </summary>
  /// 
  [Obsolete("Use IMetarRetriever instead of this interface.")]
  public interface IMetarRetrieve : IMetarRetriever
  {

  }
}
