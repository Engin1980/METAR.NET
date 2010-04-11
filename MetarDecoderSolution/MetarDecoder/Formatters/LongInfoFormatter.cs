using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENG.Metar.Decoder
{
  public class LongInfoFormatter : InfoFormatter
  {

    public override string MetarFormat
    {
      get {
        return "METAR for {0} issued at day {1}, {2}:{3}Z. {6} {7} {8}";
      }
    }

    public override string WindFormat
    {
      get {
        return "Wind {0} at {2} {3}{5}.";
      }
    }
    public override string WindGustingFormat
    {
      get
      {
        return "Wind {0} at {2}{3} ^gusting {4}{3}|{5}.";
      }
    }
    public override string WindVaryingFormat
    {
      get {
        return " vary between {0} and {2}";
      }
    }
    
    public override string VisibilityFormat
    {
      get {
        return "Visibility {2} {0} {1}.";
      }
    }
    public override string VisibilityWithOtherDistanceFormat
    {
      get
      {
        return "Visibility {2} {0} {1}, {5} {4} {1}.";
      }
    }
    public override string VisibilityClearFormat
    {
      get
      {
        return "Visibility unlimited.";
      }
    }

    public override string RunwaysVisibilityFormat
    {
      get {
        throw new NotImplementedException();
      }
    }

    public override string RuwnayVisibilityFormat
    {
      get { throw new NotImplementedException(); }
    }
  }
}
