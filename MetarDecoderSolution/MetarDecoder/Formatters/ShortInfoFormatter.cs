using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENG.Metar.Decoder
{
  public class ShortInfoFormatter: InfoFormatter
  {

    public override string MetarFormat
    {
      get {
        return "Metar for {0} issued at {2}:{3}. {6} Temperature {4}C.";
      }
    }

    public override string WindFormat
    {
      get { 
        return "Wind {1} at {4} {3}.";
      }
    }
    public override string WindGustingFormat
    {
      get
      {
        return "Wind {1} at {4} {3}.";
      }
    }
    public override string WindVaryingFormat {
      get
      {
        return "from {0} to {2}";
      }
    }


    public override string VisibilityFormat
    {
      get { throw new NotImplementedException(); }
    }

    public override string VisibilityWithOtherDistanceFormat
    {
      get { throw new NotImplementedException(); }
    }

    public override string VisibilityClearFormat
    {
      get { throw new NotImplementedException(); }
    }

    public override string RunwaysVisibilityFormat
    {
      get { throw new NotImplementedException(); }
    }

    public override string RuwnayVisibilityFormat
    {
      get { throw new NotImplementedException(); }
    }
  }
}
