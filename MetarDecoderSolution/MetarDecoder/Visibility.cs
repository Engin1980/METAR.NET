using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.Metar.Decoder.Formatters;

namespace ENG.Metar.Decoder
{
  /// <summary>
  /// Describes visibility.
  /// </summary>
  /// <seealso cref="T:ENG.Metar.Decoder.TrendVisibility"/>
  public class Visibility : TrendVisibility
  {
    #region Nested

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
    #endregion Nested

    #region Properties

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private eDirection? _DirectionSpecification;
    ///<summary>
    /// Sets/gets directory specification value. (e.g. 3000NE). Null if not used.
    ///</summary>
    public eDirection? DirectionSpecification
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
    private eDirection? _OtherDirectionSpecification;
    ///<summary>
    /// Sets/gets other measured distance's direction (e.g. postfix of second part in 3000NE 1200S). Null if not used.
    /// Must be used when OtherDistance is used.
    ///</summary>
    public eDirection? OtherDirectionSpecification
    {
      get
      {
        return (_OtherDirectionSpecification);
      }
      set
      {
        _OtherDirectionSpecification = value;
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
      SetMeters (distance, null, null, null);
    }

    /// <summary>
    /// Sets distance in meters with direction specification.
    /// </summary>
    /// <param name="distance">Distance</param>
    /// <param name="way">Direction specification.</param>
    public void SetMeters(int distance, eDirection way)
    {
      SetMeters(distance, way, null, null);
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
      SetMeters(distance, way, secondDistance, secondWay);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="distance"></param>
    /// <param name="way"></param>
    /// <param name="secondDistance"></param>
    /// <param name="secondWay"></param>
    public void SetMeters(int distance, eDirection? way, int? secondDistance, eDirection? secondWay)
    {
      UseEUStyle = true;
      IsDevicesMinimumValue = false;

      this.Distance = distance;
      this.DirectionSpecification = way;
      this.OtherDistance = secondDistance;
      this.OtherDirectionSpecification = secondWay;
    }

    #endregion Methods

    #region Inherited

    /// <summary>
    /// Returns item in text string.
    /// </summary>
    /// <param name="formatter">If false, only basic information is returned. If true, all (complex) information is provided.</param>
    /// <returns></returns>
    public override string ToInfo(InfoFormatter formatter)
    {
      string ret = "";

      //VISIBILITY
      //0 - isClear
      //1 - distanceOrNull
      //2 - distance unit
      //3 - distance unit long
      //4 - distance direction (if any), or null
      //5 - not used
      //6 - true if it is minimum measurable distance, or false
      //7 - other distance if used, or null
      //8 - other distance direction if other distance used, or null
      //9 - true if runwayVisibility definitions is present, false otherwise
      //10 - (iter) RUNWAY-VISIBILITY


      // 11 - 
      // 

      string f = null;
      try
      {
        f = formatter.VisibilityFormat;
      }
      catch { }
      if (f == null)
        return null;
      else if (f.Length == 0)
        return "";

      ret = formatter.Format(
  f,
  this.IsClear, //0
  this.Distance.HasValue ?  this.Distance.Value.ToString(false) : null,
  this.UseEUStyle ? "m" : "sm",
  this.UseEUStyle ? "meters" : "miles",
  this.DirectionSpecification.HasValue ? this.DirectionSpecification.Value.ToString() : null,
  null,
  this.IsDevicesMinimumValue, // 6
  this.OtherDistance.HasValue ? this.OtherDistance.Value.ToString(false) : "-",
  this.OtherDistance.HasValue ? this.OtherDirectionSpecification.Value.ToString() : "-"
  , (this.Runways != null && this.Runways.Count > 0)
  , GetRunwaysVisibilities(formatter)
  );

      return ret;
    }

    private object GetRunwaysVisibilities(InfoFormatter formatter)
    {
      StringBuilder ret = new StringBuilder();

      Runways.ForEach(r => ret.Append(r.ToInfo(formatter)));

      return ret;
    }



    private string eDirectionToInfo(eDirection eDirection, bool verbose)
    {
      string ret;

      switch (eDirection)
      {
        case eDirection.E:
          ret = (verbose ? "east" : "E");
          break;
        case eDirection.N:
          ret = (verbose ? "north" : "N");
          break;
        case eDirection.NE:
          ret = (verbose ? "northeast" : "NE");
          break;
        case eDirection.NW:
          ret = (verbose ? "northwest" : "NW");
          break;
        case eDirection.S:
          ret = (verbose ? "south" : "S");
          break;
        case eDirection.SE:
          ret = (verbose ? "southeast" : "SE");
          break;
        case eDirection.SW:
          ret = (verbose ? "southwest" : "SW");
          break;
        case eDirection.W:
          ret = (verbose ? "west" : "W");
          break;
        default:
          throw new NotImplementedException();
      }

      return ret;
    }

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
        ret.Append(Distance.Value.Value.ToString("0000"));
        if (DirectionSpecification.HasValue)
          ret.Append(DirectionSpecification.ToString());

        if (OtherDistance.HasValue)
        {
          ret.Append(" " + OtherDistance.Value.Value.ToString("0000"));
          ret.Append(OtherDirectionSpecification.ToString());
        }

      }
      else
      {
        if (IsDevicesMinimumValue)
          ret.Append("M");
        ret.Append(Distance.Value.ToString(false) + "SM");
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

      if ((OtherDistance.HasValue && !OtherDirectionSpecification.HasValue)
        ||
        (!OtherDistance.HasValue && OtherDirectionSpecification.HasValue))
        errors.Add("Both Other-distance and Other-way-restriction must have value or set be to null.");

      if (IsClear && OtherDistance.HasValue)
        warnings.Add("Is-clear is true, and also other-distance value is set. This combination is probably not correct.");
    }

    #endregion Inherited

  }
}
