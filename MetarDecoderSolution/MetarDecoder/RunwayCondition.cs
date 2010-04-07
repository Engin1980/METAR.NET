using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetarDecoder
{
  /// <summary>
  /// Represents info about runway conditions (contamination, depth and braking action).
  /// </summary>
  /// <seealso cref="T:MetarDecoder.MetarItem"/>
  public class RunwayCondition : MetarItem
  {
    /// <summary>
    /// Presents runway deposit.
    /// </summary>
    public enum eDeposit
    {
      /// <summary>
      /// Clean dry runway.
      /// </summary>
      CleanDry = 0,
      /// <summary>
      /// Wet runway.
      /// </summary>
      WetDamp = 1,
      /// <summary>
      /// Wet patches on runway.
      /// </summary>
      WetOrWetPatches = 2,
      /// <summary>
      /// Rime or frosts on runway.
      /// </summary>
      RimeOrFrost= 3,
      /// <summary>
      /// Dry snow on runay.
      /// </summary>
      DrySnow = 4,
      /// <summary>
      /// Wet snow on runway.
      /// </summary>
      WetSnow = 5,
      /// <summary>
      /// Slush on runway.
      /// </summary>
      Slush = 6,
      /// <summary>
      /// Ice on runway.
      /// </summary>
      Ice = 7,
      /// <summary>
      /// Compact snow on runway.
      /// </summary>
      CompactSnow = 8,
      /// <summary>
      /// Frozen ruts or ridges on runway.
      /// </summary>
      FrozentRutsRidges = 9
    }
    /// <summary>
    /// Represents amount of contamination on runway.
    /// </summary>
    public enum eContamination
    {
      /// <summary>
      /// Reserved, not used.
      /// </summary>
      Reserved0 = 0,
      /// <summary>
      /// Less than 10%.
      /// </summary>
      LessThan10Percent = 1,
      /// <summary>
      /// More than 10% but less than 25%.
      /// </summary>
      LessThan25Percent = 2,
      /// <summary>
      /// Reserved, not used.
      /// </summary>
      Reserved3 = 3,
      /// <summary>
      /// Reserved, not used.
      /// </summary>
      Reserved4 = 4,
      /// <summary>
      /// More than 25%, but less than 50%.
      /// </summary>
      LessThan50Percent = 5,
      /// <summary>
      /// Reserved, not used.
      /// </summary>
      Reserved6 = 6,
      /// <summary>
      /// Reserved, not used.
      /// </summary>
      Reserved7 = 7,
      /// <summary>
      /// Reserved, not used.
      /// </summary>
      Reserved8 = 8,
      /// <summary>
      /// More than 50% including 100%.
      /// </summary>
      MoreThan50Percent = 9
    }

    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private string _Runway;
    ///<summary>
    /// Sets/gets Runway name/sign.
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
    /// True if definition is for all runways.
    /// </summary>
    /// <value></value>
    public bool IsForAllRunways
    {
      get
      {
        return (Runway == "88");
      }
    }
    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private eDeposit? _Deposit;
    ///<summary>
    /// Sets/gets deposit of runway. Null if unknown/not reported (that is / in metar).
    ///</summary>
    public eDeposit? Deposit
    {
      get
      {
        return (_Deposit);
      }
      set
      {
        _Deposit = value;
      }
    }
    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private eContamination? _Contamination;
    ///<summary>
    /// Sets/gets contamination level of runway. Null if unknown/not reported (that is / in metar).
    ///</summary>
    public eContamination? Contamination
    {
      get
      {
        return (_Contamination);
      }
      set
      {
        _Contamination = value;
      }
    }
    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private int? _Depth;
    ///<summary>
    /// Sets/gets contamination depth on runway. Null if unknown/not reported (that is // in metar).
    ///</summary>
    public int? Depth
    {
      get
      {
        return (_Depth);
      }
      set
      {
        _Depth = value;
      }
    }
    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private int? _Friction;
    ///<summary>
    /// Sets/gets friction/braking effect of runway. Null if unknown/not reported (that is // in metar).
    ///</summary>
    public int? Friction
    {
      get
      {
        return (_Friction);
      }
      set
      {
        _Friction = value;
      }
    }

    /// <summary>
    /// Returns item in metar string.
    /// </summary>
    /// <returns></returns>
    public string ToMetar()
    {
      StringBuilder ret = new StringBuilder();

      ret.Append(
        "R" +Runway + "/" +
        (Deposit.HasValue ? ((int)Deposit.Value).ToString() : "/") +
        (Contamination.HasValue ? ((int)Contamination.Value).ToString() : "/") +
        (Depth.HasValue ? Depth.Value.ToString("00") : "//") +
        (Friction.HasValue ? Friction.Value.ToString("00") : "//"));

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

      if (Contamination.HasValue)
      {
        switch (Contamination.Value)
        {
          case eContamination.Reserved0:
          case eContamination.Reserved3:
          case eContamination.Reserved4:
          case eContamination.Reserved6:
          case eContamination.Reserved7:
          case eContamination.Reserved8:
            warnings.Add("This runway contamination value is reserved and should not be used.");
            break;
        }
      }
    }

    #endregion
  }
}
