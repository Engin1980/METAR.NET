using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENG.Metar.Decoder
{
  /// <summary>
  /// Represents runway visibility information.
  /// </summary>
  /// <seealso cref="T:ENG.Metar.Decoder.MetarItem"/>
  public class RunwayVisibility : IMetarItem
  {

    #region Nested

    /// <summary>
    /// Represents tendency of visibility.
    /// </summary>
    public enum eTendency
    {
      /// <summary>
      /// Increasing tendency
      /// </summary>
      U,
      /// <summary>
      /// Decreasing tendency
      /// </summary>
      D,
      /// <summary>
      /// No change expected-tendency.
      /// </summary>
      N
    }

    /// <summary>
    /// Represents measuring device restriction.
    /// </summary>
    public enum eDeviceMeasurementRestriction
    {
      /// <summary>
      /// If used, visibility is at best at this value.
      /// Device cannot measure less value.
      /// </summary>
      M,
      /// <summary>
      /// If used, visibility is at worse at this value.
      /// Device cannot measure bigger value.
      /// </summary>
      P
    }
    #endregion Nested

    #region Properties

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private eDeviceMeasurementRestriction? _DeviceMeasurementRestriction = null;
    ///<summary>
    /// Sets/gets device measurement restriction. Null if not used.
    ///</summary>
    public eDeviceMeasurementRestriction? DeviceMeasurementRestriction
    {
      get
      {
        return (_DeviceMeasurementRestriction);
      }
      set
      {
        _DeviceMeasurementRestriction = value;
      }
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private eTendency? _Tendency;
    ///<summary>
    /// Sets/gets visibility tendency value. Null if not used.
    ///</summary>
    public eTendency? Tendency
    {
      get
      {
        return (_Tendency);
      }
      set
      {
        _Tendency = value;
      }
    }

    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private NonNegInt _Distance;
    ///<summary>
    /// Sets/gets Visibility value.
    ///</summary>
    public NonNegInt Distance
    {
      get
      {
        return (_Distance);
      }
      set
      {
        _Distance = value;
      }
    }

    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private NonNegInt? _VariableVisibility;
    ///<summary>
    /// Sets/gets VariableVisibility value. Null if visibility does not vary.
    ///</summary>
    public NonNegInt? VariableVisibility
    {
      get
      {
        return (_VariableVisibility);
      }
      set
      {
        _VariableVisibility = value;
      }
    }
    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private string _Runway;
    ///<summary>
    /// Sets/gets Runway designator.
    ///</summary>
    public string Runway
    {
      get
      {
        return (_Runway);
      }
      set
      {
        _Runway = value;
      }
    }

    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private bool _IsInFeet;
    ///<summary>
    /// Sets/gets true if visibility is in feet. Used in US.
    ///</summary>
    public bool IsInFeet
    {
      get
      {
        return (_IsInFeet);
      }
      set
      {
        _IsInFeet = value;
      }
    }

    #endregion Properties

    #region Inherited

#if INFO
   /// <summary>
    /// Returns item in text string.
    /// </summary>
    /// <param name="verbose">If false, only basic information is returned. If true, all (complex) information is provided.</param>
    /// <returns></returns>
public string ToInfo(bool verbose)
    {
      StringBuilder ret = new StringBuilder();

      ret.AppendSpaced("Runway " + Runway + " visibility");

      if (this.DeviceMeasurementRestriction.HasValue)
        if (this.DeviceMeasurementRestriction.Value == eDeviceMeasurementRestriction.P)
          ret.AppendSpaced("more than");
        else
          ret.AppendSpaced("less than");

      ret.AppendSpaced(this.Distance.ToString());
      if (this.VariableVisibility.HasValue)
        ret.AppendSpaced(" to " + this.VariableVisibility.Value.ToString());

      if (this.Tendency.HasValue)
        if (this.Tendency.Value == eTendency.D)
          ret.AppendSpaced(verbose ? "decreasing" : "decr.");
        else if (this.Tendency.Value == eTendency.U)
          ret.AppendSpaced(verbose ? "increasing" : "incr.");

      if (!this.IsInFeet)
        ret.AppendSpaced("meters.");
      else
        ret.AppendSpaced("feet.");

      return ret.ToString();
    }
#endif //INFO

    /// <summary>
    /// Returns item in metar string.
    /// </summary>
    /// <returns></returns>
    public string ToMetar()
    {
      StringBuilder ret = new StringBuilder();

      ret.Append("R" + Runway + "/");
      if (DeviceMeasurementRestriction != null)
        ret.Append(DeviceMeasurementRestriction.Value.ToString());
      ret.Append(Distance.ToString("0000"));
      if (VariableVisibility.HasValue)
        ret.Append("V" + VariableVisibility.Value.ToString("0000"));
      if (IsInFeet)
        ret.Append("FT");
      else if (Tendency.HasValue)
        ret.Append(Tendency.ToString());

      return ret.ToString();
    }

    #region MetarItem Members


    /// <summary>
    /// Proceed sanity check of inserted values.
    /// </summary>
    /// <param name="errors">Found errors.</param>
    /// <param name="warnings">Found warnings.</param>
    public void SanityCheck(ref List<string> errors, ref List<string> warnings)
    {
      if (string.IsNullOrEmpty(Runway))
        errors.Add("Runway number/sign is not set.");
    }

    #endregion

    #endregion Inherited

  }
}
