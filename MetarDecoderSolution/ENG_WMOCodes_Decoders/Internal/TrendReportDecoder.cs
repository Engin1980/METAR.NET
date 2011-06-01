﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;

namespace ENG.WMOCodes.Decoders.Internal
{
  class TrendReportDecoder : CustomDecoder<TrendReport>
  {
    public override string Description
    {
      get { return "Trend report"; }
    }

    protected override TrendReport _Decode(ref string source)
    {
      TrendReport ret = new TrendReport();

      try
      {
        ret.Wind = new WindDecoder() { Required = false }.Decode(ref source);
        ret.Visibility = new TrendVisibilityDecoder() { Required = false }.Decode(ref source);
        ret.Phenomens = new TrendPhenomInfoDecoder() { Required = false }.Decode(ref source);
        ret.Clouds = new TrendCloudInfoDecoder() { Required = false }.Decode(ref source);
      } // try
      catch (Exception ex)
      {
        throw new DecodeException(this.Description, ex);
      } // catch (Exception ex)

      return ret;
    }

  }
}