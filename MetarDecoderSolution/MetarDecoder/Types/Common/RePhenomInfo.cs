using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.Metar.Decoder.Formatters;

namespace ENG.Metar.Decoder
{
  public class RePhenomInfo : PhenomInfo
  {
    #region Inherited

    /// <summary>
    /// Returns item in text string.
    /// </summary>
    /// <param name="formatter">Formatter used to format string.</param>
    /// <returns></returns>
    public override string ToInfo(InfoFormatter formatter)
    {
      string ret = null;

      /* PHENOMS-INFO
       * 0 - true when isNSW
       * 1 - true if some phenoms are present
       * 2 - PHENOM-INFO
       * */

      string f = null;
      try
      {
        f = formatter.RePhenomsFormat;
      }
      catch { }
      if (f == null)
        return null;
      else if (f.Length == 0)
        return "";

#warning TODO Tady ten druhý argument "false" vůbec nevím co má dělat.

      ret = formatter.Format(
            f,
            false,
            this.Count != 0,
            GetPhenomInfo(formatter)
            );

      return ret;
    }

    /// <summary>
    /// Returns item in code string.
    /// </summary>
    /// <returns></returns>
    public override string ToCode()
    {
        StringBuilder ret = new StringBuilder();

        this.ForEach(
          i => ret.AppendSpaced("RE" + i.ToCode()));

        return ret.ToString().TrimEnd();
    }

    /// <summary>
    /// Returns a <see cref="T:System.String"/> that represents the current instance.
    /// </summary>
    /// <returns>A <see cref="T:System.String"/> that represents the current instance.</returns>
    public override string ToString()
    {
      return ESystem.Extensions.ObjectExt.ToInlineInfoString(this);
    }

    #endregion Inherited
  }
}
