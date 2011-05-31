using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENG.Metar.Decoder
{
  internal class Decoder
  {
    public delegate void DecodeDelegate(
      System.Text.RegularExpressions.Group[] grp, ref Metar target);

    public readonly string Regex;
    public readonly string Description;
    public readonly DecodeDelegate DecodingMethod;

    public int GetBlockCount()
    {
      int ret = 0;

      for (int i = 0; i < Regex.Length; i++)
      {
        if (Regex[i] == '(')
          if (Regex[i + 1] != '?')
            ret++;
      }

      return ret;
    }

    public void Decode(System.Text.RegularExpressions.GroupCollection groups,
      int startIndex,
      ref Metar target)
    {
      System.Text.RegularExpressions.Group[] grp =
        CutGroups(groups, startIndex);

      DecodingMethod(grp, ref target);
    }

    private System.Text.RegularExpressions.Group[] CutGroups(
      System.Text.RegularExpressions.GroupCollection groupColl, int startIndex)
    {
      int count = GetBlockCount();
      System.Text.RegularExpressions.Group[] ret =
        new System.Text.RegularExpressions.Group[count];

      for (int i = 0; i < count; i++)
      {
        ret[i] = groupColl[startIndex + i];
      }

      return ret;
    }

    public Decoder(string regex, DecodeDelegate decodingMethod, string description)
    {
      this.Regex = regex;
      this.Description = description;
      this.DecodingMethod = decodingMethod;
    }

  }
}
