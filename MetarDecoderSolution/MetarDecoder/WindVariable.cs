using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESystem;

namespace MetarDecoder
{
  /// <summary>
  /// Represents wind variability between values.
  /// </summary>
  /// <seealso cref="T:MetarDecoder.MetarItem"/>
  public class WindVariable : MetarItem
  {
    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private int _FromDirection;
    ///<summary>
    /// Sets/gets FromDirection value.
    ///</summary>
    public int FromDirection
    {
      get
      {
        return (_FromDirection);
      }
      set
      {
        if (!value.IsBetween(0, 360))
          throw new ArgumentOutOfRangeException("Value must be between 0 and 360.");
        _FromDirection = value;
      }
    }

    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private int _ToDirection;
    ///<summary>
    /// Sets/gets ToDirection value.
    ///</summary>
    public int ToDirection
    {
      get
      {
        return (_ToDirection);
      }
      set
      {
        if (!value.IsBetween(0, 360))
          throw new ArgumentOutOfRangeException("Value must be between 0 and 360.");
        _ToDirection = value;
      }
    }

    /// <summary>
    /// Returns item in metar string.
    /// </summary>
    /// <returns></returns>
    public string ToMetar()
    {
      return FromDirection.ToString("000") + "V" + ToDirection.ToString("000");
    }

    /// <summary>
    /// Proceed sanity check of inserted values.
    /// </summary>
    /// <param name="errors">Found errors.</param>
    /// <param name="warnings">Found warnings.</param>
    public void SanityCheck(ref List<string> errors, ref List<string> warnings)
    {
      // nothing
      if (FromDirection == ToDirection)
        warnings.Add("Significant variable wind range is 0.");
    }
  }
}
