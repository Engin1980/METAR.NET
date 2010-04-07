using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENG.Metar.Decoder
{
  /// <summary>
  /// Represents one cloud (e.g. OVC040TCU).
  /// </summary>
  public class Cloud : MetarItem
  {
    /// <summary>
    /// List of types of clouds.
    /// </summary>
    public enum eType
    {
      /// <summary>
      /// Few clouds.
      /// </summary>
      FEW,
      /// <summary>
      /// Scattered clouds.
      /// </summary>
      SCT,
      /// <summary>
      /// Broken clouds.
      /// </summary>
      BKN,
      /// <summary>
      /// Overcast clouds.
      /// </summary>
      OVC
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private bool _IsCB;
    ///<summary>
    /// Sets/gets if cloud is cumulonimbus. That is, if there is prefix with CB, e.g. OVC040CB.
    ///</summary>
    ///<remarks>
    ///This property cannot be true if IsTCU is true.
    ///</remarks>
    public bool IsCB
    {
      get
      {
        return (_IsCB);
      }
      set
      {
        _IsCB = value;
      }
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private bool _IsTCU;
    ///<summary>
    /// Sets/gets if cloud is towering cumulus. That is, if there is prefix with TCU, e.g. OVC040TCU.
    ///</summary>
    ///<remarks>
    ///This property cannot be true if IsCB is true.
    ///</remarks>
    public bool IsTCU
    {
      get
      {
        return (_IsTCU);
      }
      set
      {
        _IsTCU = value;
      }
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private NonNegInt _Altitude;
    ///<summary>
    /// Sets/gets Altitude value, in hundreds of feet, e.g. OVC040 for 4000 ft.
    ///</summary>
    public NonNegInt Altitude
    {
      get
      {
        return (_Altitude);
      }
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private eType _Type;
    ///<summary>
    /// Sets/gets type of clouds. <see cref="eType"/>
    ///</summary>
    public eType Type
    {
      get
      {
        return (_Type);
      }
    }

    /// <summary>
    /// Sets cloud type.
    /// </summary>
    /// <param name="type">Type of cloud.</param>
    /// <param name="altitude">Altitude in hunderds of ft.</param>
    /// <param name="isCB">True if cloud is cumulonimbus (CB).</param>
    /// <param name="isTCU">True if cloud is towering cumulus (TCU).</param>
    public void SetClouds(eType type, int altitude, bool isCB, bool isTCU)
    {
      if (isCB && isTCU)
        throw new ArgumentException("Unable to set both CB and TCU flags to true.", "isTCU");

      _IsTCU = isTCU;
      _IsCB = isCB;

      _Type = type;

      if (altitude > 999)
        throw new ArgumentException("Invalid altitude value. Maximum is 999.", "altitude");
      _Altitude = altitude;
    }

    /// <summary>
    /// Sets cloud type.
    /// </summary>
    /// <param name="type">Type of cloud.</param>
    /// <param name="altitude">Altitude in hunderds of ft.</param>
    public void SetClouds(eType type, int altitude)
    {
      SetClouds(type, altitude, false, false);
    }

    /// <summary>
    /// Sets cloud type.
    /// </summary>
    /// <param name="type">Type of cloud (as string).</param>
    /// <param name="altitude">Altitude in hunderds of ft.</param>
    /// <param name="isCB">True if cloud is cumulonimbus (CB).</param>
    /// <param name="isTCU">True if cloud is towering cumulus (TCU).</param>
    public void SetClouds(string type, int altitude, bool isCB, bool isTCU)
    {
      eType t = (eType)Enum.Parse(typeof(eType), type);
      SetClouds(t, altitude, isCB, isTCU);
    }

    /// <summary>
    /// Sets cloud type.
    /// </summary>
    /// <param name="type">Type of cloud (as string).</param>
    /// <param name="altitude">Altitude in hunderds of ft.</param>
    public void SetClouds(string type, int altitude)
    {
      eType t = (eType)Enum.Parse(typeof(eType), type);
      SetClouds(t, altitude);
    }

    /// <summary>
    /// Returns item in metar string.
    /// </summary>
    /// <returns></returns>
    public string ToMetar()
    {
      StringBuilder ret = new StringBuilder();

      ret.Append(Type.ToString());
      ret.Append(Altitude.ToString("000"));
      if (IsTCU)
        ret.Append("TCU");
      else if (IsCB)
        ret.Append("CB");

      return ret.ToString();
    }

    /// <summary>
    /// Proceed sanity check of inserted values.
    /// </summary>
    /// <param name="errors">Found errors.</param>
    /// <param name="warnings">Found warnings.</param>
    public void SanityCheck(ref List<string> errors, ref List<string> warnings)
    {
      if (Altitude > 400)
        warnings.Add("Altitude value over 400 is probably incorrect.");
      if (IsTCU && IsCB)
        errors.Add("IsTCU and IsCB flags cannot be set both at same time. Only one of them can be used.");
    }
  }
}
