using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENG.WMOCodes.Decoders.Internal
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
      : base("", inner)
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

      while (true)
      {
        tree.Append("->" + curr.Description);
        if (curr.InnerException != null && curr.InnerException is DecodeException)
          curr = curr.InnerException as DecodeException;
        else
          break;
      }

      tree.Append(". Reason:");

      Exception ex = curr.InnerException;
      while (ex != null)
      {
        tree.Append(" >> " + curr.InnerException.Message);
        ex = ex.InnerException;
      }

      return tree.ToString();
    }
  }
}
