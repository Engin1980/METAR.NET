using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENG.Metar.Decoder.Types.METAR
{
  class CloudInfo : TrendCloudInfo
  {
    #region Properties
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private bool _IsNCD = false;
    ///<summary>
    /// Gets "no clouds detected" value. That is NCD in metar.
    ///</summary>
    public bool IsNCD
    {
      get
      {
        return ((this.Count == 0) && _IsNCD);
      }
    }
    #endregion Properties

    #region Methods

    protected override void SetAllFlagsOff()
    {
      _IsNCD = false;
      base.SetAllFlagsOff();
    }
    public virtual void SetNCD()
    {
      SetAllFlagsOff();
      _IsNCD = true;
    }

    public override string ToCode()
    {
      if (IsNCD)
        return "NCD";
      else
        return base.ToCode();
    }

    public override void SanityCheck(ref List<string> errors, ref List<string> warnings)
    {
      base.SanityCheck(ref errors, ref warnings);
      if (IsNCD && (IsSKC || IsNSC || IsVerticalVisibility || Count > 0))
        errors.Add("Unable to have IsNCD flag on, when IsSKC, IsNSC, IsVerticalVisibility is true or count of clouds > 0.");
    }

    #endregion Methods
  }
}
