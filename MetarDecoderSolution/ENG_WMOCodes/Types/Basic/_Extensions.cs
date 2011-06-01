using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ENG.WMOCodes.Types.Basic
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

    public static void CopyPropertiesTo(this object source, object target)
    {
      error error error
      // tady je to v lojzovi. kopíruje to i parametry kolekcí, když se kopírují kolekce, což je průser
      // a netuším co s tím

      PropertyInfo[] sp = source.GetType().GetProperties();
      PropertyInfo[] tp = target.GetType().GetProperties();
      PropertyInfo[] shared = GetSharedProperties(sp, tp);
      object val;

      foreach (var fItem in shared)
      {
        try
        {
          val = fItem.GetValue(source, null);
          fItem.SetValue(target, val, null);
        }
        catch (Exception ex)
        {
          throw new Exception("Failed to copy property " + fItem.Name + " of type " + source.GetType().FullName + ".", ex);
        }
      } // foreach (var fItem in shared)
    }

    private static PropertyInfo[] GetSharedProperties(PropertyInfo[] sp, PropertyInfo[] tp)
    {
      List<PropertyInfo> ret = new List<PropertyInfo>();
      foreach (var fS in sp)
        foreach (var fT in tp)
          if (fS.Name == fT.Name && fS.CanRead && fT.CanWrite) ret.Add(fS);
      return ret.ToArray();
    }

  }
}
