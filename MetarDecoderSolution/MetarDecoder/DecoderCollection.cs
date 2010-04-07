using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetarDecoder
{
  internal class DecoderCollection : List<Decoder>
  {
    public void Add(string regex, Decoder.DecodeDelegate decode, string description)
    {
      this.Add(new Decoder(regex, decode, description));
    }

    public string GetRegex()
    {
      StringBuilder ret = new StringBuilder();

      foreach (var fItem in this)
      {
        ret.Append(fItem.Regex);
      } // foreach (var fItem in this)

      return ret.ToString();
    }
  }
}
