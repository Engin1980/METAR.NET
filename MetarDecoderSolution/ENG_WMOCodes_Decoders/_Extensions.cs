using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ENG.WMOCodes.Decoders
{
  static class _Extensions
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

    public static void CopyPropertiesTo(this object source, object target)
    {
      PropertyInfo[] sp = source.GetType().GetProperties();
      PropertyInfo[] tp = target.GetType().GetProperties();
      PropertyInfo[] shared = GetSharedProperties(sp, tp);
      object val;
      foreach (var fItem in shared)
      {
        val = fItem.GetValue(source, null);
        fItem.SetValue(target, val, null);
      } // foreach (var fItem in shared)
    }

    private static PropertyInfo[] GetSharedProperties(PropertyInfo[] sp, PropertyInfo[] tp)
    {
      List<PropertyInfo> ret = new List<PropertyInfo>();
      foreach (var fS in sp)
        foreach (var fT in tp)
          if (fS.Name == fT.Name) ret.Add(fS);
      return ret.ToArray();
    }
  }
}
