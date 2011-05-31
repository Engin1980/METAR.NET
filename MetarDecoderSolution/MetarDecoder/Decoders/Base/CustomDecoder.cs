using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENG.Metar.Decoder.Decoders.Base
{
  abstract class CustomDecoder<T> : InternalDecoder<T>
  {
    protected override abstract T _Decode(ref string source);
  }
}
