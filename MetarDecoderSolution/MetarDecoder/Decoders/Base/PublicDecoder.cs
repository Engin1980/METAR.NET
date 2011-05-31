using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENG.Metar.Decoder.Decoders.Base
{
  public abstract class PublicDecoder<T> 
  {
    public abstract string Description { get; }

    public T Decode(string source)
    {
      T ret = default(T);

      try
      {
        ret = _Decode(source);
      } // try
      catch (DecodeException ex)
      {
        throw new DecodeException(Description, ex.InnerException);
      }
      catch (Exception ex)
      {
        throw new DecodeException(Description, ex);
      } // catch (Exception ex)

      return ret;
    }

    protected abstract T _Decode(string source);
  }
}
