﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENG.WMOCodes.Decoders.Internal.Basic
{
  abstract class CustomDecoder<T> : InternalDecoder<T>
  {
    protected override abstract T _Decode(ref string source);
  }
}
