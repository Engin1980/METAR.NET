using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENG.Metar.Decoder
{
  /// <summary>
  /// Describes visibility.
  /// </summary>
  /// <seealso cref="T:ENG.Metar.Decoder.TrendVisibility"/>
  public class Visibility : TrendVisibility
  {
    #region Properties

    /// <summary>
    /// Represents world direction
    /// </summary>
    public enum eDirection
    {
      /// <summary>
      /// North
      /// </summary>
      N,
      /// <summary>
      /// Northeast
      /// </summary>
      NE,
      /// <summary>
      /// East
      /// </summary>
      E,
      /// <summary>
      /// Southeast
      /// </summary>
      SE,
      /// <summary>
      /// South
      /// </summary>
      S,
      /// <summary>
      /// Southwest
      /// </summary>
      SW,
      /// <summary>
      /// West
      /// </summary>
      W,
      /// <summary>
      /// Northwest
      /// </summary>
      NW
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private eDirection? _DirectionSpecification;
    ///<summary>
    /// Sets/gets directory specification value. (e.g. 3000NE). Null if not used.
    ///</summary>
    public eDirection? DirectorySpecification
    {
      get
      {
        return (_DirectionSpecification);
      }
      set
      {
        _DirectionSpecification = value;
      }
    }

    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private Racional? _OtherDistance;
    ///<summary>
    /// Sets/gets other direction measured distance (e.g. numeric second part in 3000NE 1200S). Null if not used.
    ///</summary>
    public Racional? OtherDistance
    {
      get
      {
        return (_OtherDistance);
      }
      set
      {
        _OtherDistance = value;
      }
    }

    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private eDirection? _OtherWayRestriction;
    ///<summary>
    /// Sets/gets other measured distance's direction (e.g. postfix of second part in 3000NE 1200S). Null if not used.
    /// Must be used when OtherDistance is used.
    ///</summary>
    public eDirection? OtherWayRestriction
    {
      get
      {
        return (_OtherWayRestriction);
      }
      set
      {
        _OtherWayRestriction = value;
      }
    }

    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private List<RunwayVisibility> _Runways = new List<RunwayVisibility>();
    ///<summary>
    /// Sets/gets runway designator.
    ///</summary>
    public List<RunwayVisibility> Runways
    {
      get
      {
        return (_Runways);
      }
      set
      {
        _Runways = value;
      }
    }
    #endregion Properties

    #region Methods    
    /// <summary>
    /// Set distance in meters. Sets EU style.
    /// </summary>
    /// <param name="distance">Visibility distance.</param>
    public override void SetMeters(int distance)
    {
      UseEUStyle = true;
      Distance = distance;
      DirectorySpecification = null;
      OtherDistance = null;
      OtherWayRestriction = null;
      IsDevicesMinimumValue = false;
    }

    /// <summary>
    /// Sets distance in meters with direction specification.
    /// </summary>
    /// <param name="distance">Distance</param>
    /// <param name="way">Direction specification.</param>
    public void SetMeters(int distance, eDirection way)
    {
      SetMeters(distance);
      DirectorySpecification = way;
    }

    /// <summary>
    /// Sets distance in meters with direction specification.
    /// </summary>
    /// <param name="distance">Distance</param>
    /// <param name="way">Direction specification.</param>
    /// <param name="secondDistance">Other visibility</param>
    /// <param name="secondWay">Other visibility direction</param>
    public void SetMeters(int distance, eDirection way, int secondDistance, eDirection secondWay)
    {
      SetMeters(distance, way);
      OtherDistance = secondDistance;
      OtherWayRestriction = secondWay;
    }

    #endregion Methods

    /// <summary>
    /// Returns item in metar string.
    /// </summary>
    /// <returns></returns>
    public override string ToMetar()
    {
      StringBuilder ret = new StringBuilder();

      if (IsClear)
      {
        if (UseEUStyle)
          ret.Append("SKC");
        else
          ret.Append("CAVOK");
      }
      else if (UseEUStyle)
      {
        ret.Append(Distance.Value.ToString("0000"));
        if (DirectorySpecification.HasValue)
          ret.Append(DirectorySpecification.ToString());

        if (OtherDistance.HasValue)
        {
          ret.Append(OtherDistance.Value.Value.ToString("0000"));
          ret.Append(OtherWayRestriction.ToString());
        }

      }
      else
      {
        if (IsDevicesMinimumValue)
          ret.Append("M");
        ret.Append(Distance.ToString(false) + "SM");
      }

      foreach (var fItem in Runways)
      {
        ret.Append(" " + fItem.ToMetar());
      } // foreach (var fItem in Runways)

      return ret.ToString();
    }

    /// <summary>
    /// Proceed sanity check of inserted values.
    /// </summary>
    /// <param name="errors">Found errors.</param>
    /// <param name="warnings">Found warnings.</param>
    public override void SanityCheck(ref List<string> errors, ref List<string> warnings)
    {
      base.SanityCheck(ref errors, ref warnings);

      if (UseEUStyle && (Distance.Value > 10000))
        errors.Add("Maximum value for EU distance is 9999 meters. If more, use CAVOK instead.");
      else if (!UseEUStyle && (Distance.Value > 10))
        errors.Add("Maximum value for non-EU (USA) distance is 10 miles. If more, use SKC instead.");

      if (UseEUStyle && IsDevicesMinimumValue)
        warnings.Add("IsDeviceMinimumValue flag is not used in EU style and will be ignored.");

      if ((OtherDistance.HasValue && !OtherWayRestriction.HasValue)
        ||
        (!OtherDistance.HasValue && OtherWayRestriction.HasValue))
        errors.Add("Both Other-distance and Other-way-restriction must have value or set be to null.");

      if (IsClear && OtherDistance.HasValue)
        warnings.Add("Is-clear is true, and also other-distance value is set. This combination is probably not correct.");
    }
  }
}
