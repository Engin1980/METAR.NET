using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENG.Metar.Decoder.Decoders
{
  [Serializable]
  public class DecodeException : Exception
  {
    private string _Description;
    public string Description
    {
      get
      {
        return _Description;
      }
    }

    public DecodeException(string decoderDescription, Exception inner)
      : base(null, inner)
    {
      this._Description = decoderDescription;
    }

    protected DecodeException(
    System.Runtime.Serialization.SerializationInfo info,
    System.Runtime.Serialization.StreamingContext context)
      : base(info, context) { }

    public override string Message
    {
      get
      {
        return GenerateMessage();
      }
    }

    private string GenerateMessage()
    {
      StringBuilder tree = new StringBuilder();
      DecodeException curr = this;

      tree.Append("Decoding failed at ");

      while (curr != null)
      {
        tree.Append("->" + curr.Description);
        curr = curr.InnerException as DecodeException;
      }

      if (curr.InnerException != null)
        tree.Append(". Reason: " + curr.InnerException.Message);

      return tree.ToString();
    }
  }
}
