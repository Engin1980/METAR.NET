using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.Metar.Decoder.Formatters;

namespace ENG.Metar.Decoder
{
  /// <summary>
  /// Represents set of items defining the phenomen. E.g. +RAHZ.
  /// </summary>
  public class ePhenomCollection : List<ENG.Metar.Decoder.ePhenomCollection.ePhenom>, IMetarItem
  {
    #region Nested

    /// <summary>
    /// All types of phenomens.
    /// </summary>
    public enum ePhenom
    {
      /// <summary>
      /// Light. Value "-"
      /// </summary>
      Light = 100,
      /// <summary>
      /// Heavy. Value "+"
      /// </summary>
      Heavy,
      /// <summary>
      /// In vicinity
      /// </summary>
      VC,
      // Description
      /// <summary>
      /// Shallow
      /// </summary>
      MI = 200,
      /// <summary>
      /// Patches
      /// </summary>
      BC, // Pásy, chuchvalce 
      /// <summary>
      /// Partial
      /// </summary>
      PR, // Částečné pokrytí 
      /// <summary>
      /// Low drifting
      /// </summary>
      DR, // Nízko zvířený 
      /// <summary>
      /// Blowing
      /// </summary>
      BL, // Zvířený 
      /// <summary>
      /// Shower
      /// </summary>
      SH, // Přeháňka 
      /// <summary>
      /// Thunderstorm
      /// </summary>
      TS, // Bouřka 
      /// <summary>
      /// Freezing
      /// </summary>
      FZ, // Podchlazené (namrzající)
      // Type
      /// <summary>
      /// Drizzle
      /// </summary>
      DZ = 300, // Mrholení 
      /// <summary>
      /// Rain
      /// </summary>
      RA, // Déšť 
      /// <summary>
      /// Snow
      /// </summary>
      SN, // Sníh 
      /// <summary>
      /// Snow grains
      /// </summary>
      SG, // Sněhová zrna 
      /// <summary>
      /// Ice crystals
      /// </summary>
      IC, // Ledové jehličky 
      /// <summary>
      /// Ice pellets
      /// </summary>
      PL, // Zmrzlý déšť 
      /// <summary>
      /// Hail
      /// </summary>
      GR, // Kroupy 
      /// <summary>
      /// Snow pellets
      /// </summary>
      GS, // Malé kroupy, krupky
      // Opacity
      /// <summary>
      /// Mist
      /// </summary>
      BR = 400, // Kouřmo 
      /// <summary>
      /// Fog
      /// </summary>
      FG, // Mlha 
      /// <summary>
      /// Smoke
      /// </summary>
      FU, // Kouř 
      /// <summary>
      /// Volcanic ash.
      /// </summary>
      VA, // Vulkanický popel 
      /// <summary>
      /// Dust
      /// </summary>
      DU, // Rozsáhlý prach 
      /// <summary>
      /// Sand
      /// </summary>
      SA, // Písek 
      /// <summary>
      /// Haze
      /// </summary>
      HZ, // Zákal 
      //Other
      /// <summary>
      /// Dust/sand whirls 
      /// </summary>
      PO = 500, // Prachové/písečné víry 
      /// <summary>
      /// Squalls
      /// </summary>
      SQ, // Húlava 
      /// <summary>
      /// Funnel cloud
      /// </summary>
      FC, // Nálevkovitý oblak (tornádo) 
      /// <summary>
      /// Sand storm
      /// </summary>
      SS, // Písečná vichřice 
      /// <summary>
      /// Dust storm
      /// </summary>
      DS // Prachová vichřice 
    }

    #endregion Nested

    #region Methods

    /// <summary>
    /// Decodes and adds new phenomenom from string (e.g. from +RAHZ).
    /// </summary>
    /// <param name="value">String value to decode adds.</param>
    public void Add(string value)
    {
      ePhenom val = DecodePhenom(value);
      base.Add(val);
    }

    private ePhenom DecodePhenom(string value)
    {
      if (value == "-")
        return ePhenom.Light;
      else if (value == "+")
        return ePhenom.Heavy;
      else
      {
        ePhenom ret = (ePhenom)Enum.Parse(typeof(ePhenom), value);
        return ret;
      }
    }

    #endregion Methods

    #region Inherited

    internal string ToInfo(InfoFormatter formatter)
    {
      string ret = null;

      /* PHENOM-FORMAT
       * 0 - true if some phenoms are in block
       * 1 - PHENOM-INFO
       * */

      string f = null;
      try
      {
        f = formatter.PhenomFormat;
      }
      catch { }
      if (f == null)
        return null;
      else if (f.Length == 0)
        return "";

      ret = formatter.Format(
        formatter.PhenomFormat,
        this.Count != 0,
        GetPhenomInfo(formatter)
        );

      return ret;
    }

    private string GetPhenomInfo(InfoFormatter formatter)
    {
      StringBuilder ret = new StringBuilder();

      /* PHENOM-FORMAT
 * 0 - phenom-item-abbreviation (e.g. RA)
 * 1 - phenom item string (e.g. rain)
 * */

      foreach (var fItem in this)
      {
        ret.Append(
          formatter.Format(
            formatter.PhenomItemFormat,
            formatter.PhenomCollectionPhenomToString (fItem, false),
            formatter.PhenomCollectionPhenomToString (fItem, true)
            ));

      } // foreach (var fItem in this)

      return ret.ToString();
    }

    /// <summary>
    /// Returns item in metar string.
    /// </summary>
    /// <returns></returns>
    public string ToMetar()
    {
      StringBuilder ret = new StringBuilder();

      foreach (var fItem in this)
      {
        if (fItem == ePhenom.Heavy)
          ret.Append("+");
        else if (fItem == ePhenom.Light)
          ret.Append("-");
        else
          ret.Append(fItem.ToString());
      } // foreach (var fItem in this)

      return ret.ToString();
    }

    /// <summary>
    /// Proceed sanity check of inserted values.
    /// </summary>
    /// <param name="errors">Found errors.</param>
    /// <param name="warnings">Found warnings.</param>
    public void SanityCheck(ref List<string> errors, ref List<string> warnings)
    {
      if (IsCorrectPhenomOrder())
      {
        warnings.Add(
          "There is invalid order of phenoms. The are expected 5 disjoint groups (in brackets) containing " +
          "(-,+,VC) (MI,BC,PR,DR,BL,SH,TS,FZ) (DZ,RA,SN,SG,IC,PL,GR,GS) (BR,FG,FU,VA,DU,SA,HZ) (PO,SQ,FC,SS,DS).");
      }
    }

    #endregion Inherited


    #region Private

    private bool IsCorrectPhenomOrder()
    {
      int lasVal = 0;
      int currVal;
      foreach (ePhenom fItem in this)
      {
        currVal = (int)fItem;
        currVal = currVal / 100;
        if (currVal < lasVal)
          return false;
        else
          lasVal = currVal;
      } // foreach (ePhenom fItem in this)

      return true;
    }

    #endregion Private

  }
}
