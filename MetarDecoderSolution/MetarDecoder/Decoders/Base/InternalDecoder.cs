using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENG.Metar.Decoder.Decoders.Base
{
  internal abstract class InternalDecoder<T>
  {
    public abstract string Description { get; }

    public T Decode(ref string source)
    {
      T ret = default(T);

      try
      {
        ret = _Decode(ref source);
      } // try
      catch (DecodeException ex)
      {
        throw new DecodeException(Description + " ->" + ex.Description, ex.InnerException);
      }
      catch (Exception ex)
      {
        throw new DecodeException("Decode failed at " + Description, ex);
      } // catch (Exception ex)

      return ret;
    }

    protected abstract T _Decode(ref string source);
  }
}
