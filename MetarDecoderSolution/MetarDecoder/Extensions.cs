using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetarDecoder
{
  /// <summary>
  /// Represents extensions.
  /// </summary>
  public static class Extensions
  {
    /// <summary>
    /// Adds string into string builder and then empty space at the end.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="text"></param>
    public static void AppendSpaced(this System.Text.StringBuilder builder, string text)
    {
      builder.Append(text);
      if ((!string.IsNullOrEmpty(text)) && (!text.EndsWith(" ")))
        builder.Append(" ");
    }

    /// <summary>
    /// Returns value from Group parsed as integer. Exception if fails.
    /// </summary>
    /// <param name="grp"></param>
    /// <returns></returns>
    public static int GetIntValue(this System.Text.RegularExpressions.Group grp)
    {
      int ret =
        int.Parse(grp.Value);
      return ret;
    }
    /// <summary>
    /// Returns value from Capture parsed as integer. Exception if fails.
    /// </summary>
    /// <param name="grp"></param>
    /// <returns></returns>
    public static int GetIntValue(this System.Text.RegularExpressions.Capture grp)
    {
      int ret =
        int.Parse(grp.Value);
      return ret;
    }

  }
}
