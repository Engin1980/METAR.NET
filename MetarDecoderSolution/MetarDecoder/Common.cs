using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESystem.Extensions;

namespace ENG.Metar.Decoder
{
  
  /// <summary>
  /// Represents static class with common info
  /// </summary>
  public static class  Common
  {

    /// <summary>
    /// Units.
    /// </summary>
    public enum eUnit
    {
      /// <summary>
      /// Kilometres
      /// </summary>
      km,
      /// <summary>
      /// Metres
      /// </summary>
      m,
      /// <summary>
      /// Miles
      /// </summary>
      mi,
      /// <summary>
      /// Feet
      /// </summary>
      ft,
      /// <summary>
      /// Knots
      /// </summary>
      kt
    }

    /// <summary>
    /// World directions
    /// </summary>
    public enum eDirection
    {
      /// <summary>
      /// North
      /// </summary>
      N,
      /// <summary>
      /// NorthEast
      /// </summary>
      NE,
      /// <summary>
      /// East
      /// </summary>
      E,
      /// <summary>
      /// SouthEast
      /// </summary>
      SE,
      /// <summary>
      /// South
      /// </summary>
      S,
      /// <summary>
      /// SouthWest
      /// </summary>
      SW,
      /// <summary>
      /// West
      /// </summary>
      W,
      /// <summary>
      /// NortWest
      /// </summary>
      NW
    }

    /// <summary>
    /// Converts direction as integer into enum eDirection. <see cref="eDirection"/>
    /// </summary>
    /// <param name="heading">Heading, values from 0 to 360</param>
    /// <returns></returns>
    public static eDirection HeadingToeDirection(int heading)
    {
      if (!heading.IsBetween(0, 360))
        throw new ArgumentException("Invalid heading. Should be between 0 to 360.");

      if ((heading < 22) || (heading > 337))
        return  eDirection.N;
      else if (heading < 67)
        return eDirection.NE;
      else if (heading < 117)
        return eDirection.E;
      else if (heading < 157)
        return  eDirection.SE;
      else if (heading < 202)
        return eDirection.S;
      else if (heading < 247)
        return eDirection.SW;
      else if (heading < 292)
        return eDirection.W;
      else if (heading < 338)
        return eDirection.NW;
      else throw new ApplicationException("Invalid program state - unable recognize direction");
    }
  }
}
