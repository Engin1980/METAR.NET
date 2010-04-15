using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ENG.Metar.Decoder
{
  /// <summary>
  /// Represents metar.
  /// </summary>
  public class Metar : IMetarItem
  {
    #region Nested

    /// <summary>
    /// Types of metar. Now METAR only supported, SPECI not supported.
    /// </summary>
    public enum eType
    {
      /// <summary>
      /// Metar type.
      /// </summary>
      METAR
    }

    #endregion Nested

    #region Properties

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private eType _Type = eType.METAR;
    ///<summary>
    /// Sets/gets Type value.
    ///</summary>
    public eType Type
    {
      get
      {
        return (_Type);
      }
      set
      {
        _Type = value;
      }
    }
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private string _ICAO = "----";
    ///<summary>
    /// Sets/gets ICAO value.
    ///</summary>
    public string ICAO
    {
      get
      {
        return (_ICAO);
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("This value cannot be null.");
        _ICAO = value;
      }
    }
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private DayTime _Date = new DayTime()
    {
      Day = DateTime.Today.Day,
      Hour = DateTime.Now.Hour,
      Minute = 0
    };
    ///<summary>
    /// Sets/gets Date value.
    ///</summary>
    public DayTime Date
    {
      get
      {
        return (_Date);
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("This value cannot be null.");
        _Date = value;
      }
    }
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private bool _IsAUTO = false;
    ///<summary>
    /// Sets/gets IsAUTO value. The optional code word AUTO shall be inserted before the wind group 
    /// when a report contains fully automated observations without human intervention. 
    ///</summary>
    public bool IsAUTO
    {
      get
      {
        return (_IsAUTO);
      }
      set
      {
        _IsAUTO = value;
      }
    }
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private Wind _Wind = new Wind();
    ///<summary>
    /// Sets/gets Wind value, including VRB wind, wind varying, and gusts.
    ///</summary>
    public Wind Wind
    {
      get
      {
        return (_Wind);
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("This value cannot be null.");
        _Wind = value;
      }
    }
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private Visibility _Visibility = new Visibility();
    ///<summary>
    /// Sets/gets Visibility value, including directions.
    ///</summary>
    public Visibility Visibility
    {
      get
      {
        return (_Visibility);
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("This value cannot be null.");
        _Visibility = value;
      }
    }
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private PhenomInfo _Phenomens = null;
    ///<summary>
    /// Sets/gets Phenomens value.
    ///</summary>
    public PhenomInfo Phenomens
    {
      get
      {
        return (_Phenomens);
      }
      set
      {
        _Phenomens = value;
      }
    }
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private CloudInfo _Clouds = null;
    ///<summary>
    /// Sets/gets Clouds value.
    ///</summary>
    public CloudInfo Clouds
    {
      get
      {
        return (_Clouds);
      }
      set
      {
        _Clouds = value;
      }
    }
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private int _Temperature = 20;
    ///<summary>
    /// Sets/gets Temperature value.
    ///</summary>
    public int Temperature
    {
      get
      {
        return (_Temperature);
      }
      set
      {
        _Temperature = value;
      }
    }
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private int _DewPoint = 10;
    ///<summary>
    /// Sets/gets DewPoint value.
    ///</summary>
    public int DewPoint
    {
      get
      {
        return (_DewPoint);
      }
      set
      {
        _DewPoint = value;
      }
    }
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private PressureInfo _Pressure = new PressureInfo();
    ///<summary>
    /// Sets/gets Pressure value.
    ///</summary>
    public PressureInfo Pressure
    {
      get
      {
        return (_Pressure);
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("This value cannot be null.");
        _Pressure = value;
      }
    }
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private PhenomInfo _RePhenomens = null;
    ///<summary>
    /// Sets/gets RePhenoms value.
    ///</summary>
    public PhenomInfo RePhenomens
    {
      get
      {
        return (_RePhenomens);
      }
      set
      {
        _RePhenomens = value;
      }
    }
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private WindShearInfo _WindShears = null;
    ///<summary>
    /// Sets/gets WindShears value, or null if not presented in metar.
    ///</summary>
    public WindShearInfo WindShears
    {
      get
      {
        return (_WindShears);
      }
      set
      {
        _WindShears = value;
      }
    }
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private RunwayConditionInfo _RunwayConditions = null;
    ///<summary>
    /// Sets/gets RunwayConditions value, or null if not presented in metar.
    ///</summary>
    public RunwayConditionInfo RunwayConditions
    {
      get
      {
        return (_RunwayConditions);
      }
      set
      {
        _RunwayConditions = value;
      }
    }
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private TrendInfo _Trend = new TrendInfo();
    ///<summary>
    /// Sets/gets Trend value. Allways value is in here, when no info found, trend type is null.
    /// Trend in metar is required, (NOSIG text is minimum).
    ///</summary>
    public TrendInfo Trend
    {
      get
      {
        return (_Trend);
      }
      set
      {
        _Trend = value;
      }
    }
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private string _Remark = null;
    ///<summary>
    /// Sets/gets Remark value (without RMK prefix), or null if not presented in metar.
    ///</summary>
    public string Remark
    {
      get
      {
        return (_Remark);
      }
      set
      {
        _Remark = value;
      }
    }

    /// <summary>
    /// Calculates humidity. Very rough aproximation.
    /// </summary>
    public double Humidity
    {
      get
      {
        return 100 - 5d * (Temperature - DewPoint);
      }
    }

    #endregion Properties

    #region .ctor
    #endregion .ctor

    #region Static methods

    /// <summary>
    /// Creates metar instance from string.
    /// </summary>
    /// <param name="metarString"></param>
    /// <returns></returns>
    public static Metar Create(string metarString)
    {
      Metar ret = new Metar();
      DecoderCollection decs = GetDecoders();

      string rgx = "^" + decs.GetRegex() + "$";
      Match m = Regex.Match(metarString.ToUpper().Trim(), rgx);

      if (m.Success)
      {
        int index = 1;
        foreach (var fItem in decs)
        {
          fItem.Decode(m.Groups, index, ref ret);
          index += fItem.GetBlockCount();
        } // foreach (var fItem in decs)
      }
      else
        throw new MetarDecodeException(metarString, "Metar string not recognized.");

      return ret;
    }

    /// <summary>
    /// Checks if metar string is recognizable by this class.
    /// Returns true if success, otherwise false, if failed, and in error variable is error description.
    /// </summary>
    /// <param name="metarString"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public static bool CheckMetarString(string metarString, out string error)
    {
      DecoderCollection decs = GetDecoders();
      string rgx = "^" + decs.GetRegex() + "$";

      if (Regex.IsMatch(metarString, rgx))
      {
        error = null;
        return true;
      }
      else
      {
        StringBuilder brgx = new StringBuilder();
        brgx.Append("^");
        brgx.Append(".*");
        for (int i = 0; i < decs.Count - 1; i++)
        {
          brgx.Remove(brgx.Length - 2, 2);
          brgx.Append(decs[i].Regex + ".*");
          if (!Regex.IsMatch(metarString, brgx.ToString()))
          {
            error = "Metar incorrect in part " + decs[i].Description;
            return false;
          }
        }
        error = "Metar incorrect at the end.";
        return false;
      }
    }

    #endregion Static methods

    #region Consts

    private const string R_METAR_BLOCK = "(" + R_METAR + ")";
    private const string R_METAR = "METAR";
    private const string R_ICAO = "[A-Z]{4}";
    private const string R_DATE = @"(?#date) ((\d{2})(\d{2})(\d{2})Z)";
    private const string R_AUTO = @"(?#auto)( AUTO)?";
    private const string R_WIND = @"(?#wind)( ((\d{3}|VRB)(\d{2})(G(\d{2,3}))?(KT|MPS|KMH))( (\d{3})V(\d{3}))?)";
    private const string R_VISIBILITY =
      @"(?#visib) ((CAVOK)|(SKC)|((\d{4})(NE|SW|NW|SE|N|E|S|W)?( (\d{4})(N|NE|E|SE|S|SW|W|NW))?)|((M)?(\d+)(/(\d))?SM))";
    private const string R_ICAO_BLOCK = @"(?#icao) (" + R_ICAO + ")";
    private const string R_VIS_RWYVIS = @"((?#rwyVis)( (R(\d{2}(R|L|C)?)/(M|P)?(\d{4})(V(\d{4}))?(FT|U|N|D)?))*)";
    private const string R_PHENOMS = @"(?#phenoms)(" + R_PHENOM_SET + "*)?";
    private const string R_PHENOM_SET = "( " + R_PHENOM_ITEM + "+)";
    private const string R_PHENOM_ITEM = @"(\-|\+|VC|MI|BC|PR|DR|BL|SH|TS|FZ|DZ|RA|SN|SG|IC|PL|GR|GS|BR|FG|FU|VA|DU|SA|HZ|PO|SQ|FC|SS|DS)";
    private const string R_CLOUDS = @"(?#sky)(( NSC)|( SKC)| VV((\d{3})|/{3})|" + R_CLOUD_ITEM + "*)";
    private const string R_CLOUD_ITEM = @"( (FEW|SCT|BKN|OVC)(\d{3})(CB|TCU)?/*)";
    private const string R_TEMPDEW = @"(?#temp-dew) ((M)*(\d{2})/(M)*(\d{2}))";
    private const string R_PRESSURE = @"(?#press) ((Q|A)(\d{4}))";
    private const string R_RE_PHENOMS = @"(?#re-phen)(( " + R_RE_PHENOM_ITEM + ")+)?";
    private const string R_RE_PHENOM_ITEM = @"(RE" + R_PHENOM_ITEM + "+)";
    private const string R_WS = @"RWY(\d{2}(R|L|C)?)";
    private const string R_WSS = @"(?#ws)( (WS ALL RWY)| (WS( " + R_WS + ")*))?";
    private const string R_RWY_CONDS = @"(?#rwyCond)(( SNOCLO)|(( " + R_RWY_COND + ")*))?";
    private const string R_RWY_COND = @"R(\d{2}(L|R|C)*)/((\d|/)(\d|/)(\d{2}|/{2})(\d{2}|/{2})|(CLDR//))";
    // trends
    private const string RT_TYPEDATES = @"(?#tr-type-date)( (NOSIG)| ((TEMPO|BECMG)(( " + RT_TYPEDATE + ")*)))?";
    private const string RT_TYPEDATE = @"(FM|TL|AT)(\d{2})(\d{2})";
    private const string RT_WIND = @"(?#tr-wind)" + R_WIND + "?";
    private const string RT_VISIBILITY = @"(?#tr-visib)( ((CAVOK)|(\d{4})|((M)?(\d+)(/(\d))?SM)))?";
    private const string RT_PHENOMS = @"((( NSW)|(( " + R_PHENOM_ITEM + "+))*))?";
    private const string RT_CLOUD = @"(?#tr-sky)(( (NSC)| (SKC)| VV(\d{3})|" + R_CLOUD_ITEM + "*))?";
    // remarks
    private const string R_RMK = @"(?#rmk)( RMK (.*))?";

    #endregion Consts

    #region Internal methods

    internal static DecoderCollection GetDecoders()
    {
      DecoderCollection ret = new DecoderCollection();

      ret.Add(R_METAR_BLOCK, DecodeMetarPrefix, "METAR prefix");
      ret.Add(R_ICAO_BLOCK, DecodeICAO, "ICAO");
      ret.Add(R_DATE, DecodeDate, "Release date");
      ret.Add(R_AUTO, DecodeAuto, "AUTO");
      ret.Add(R_WIND, DecodeWind, "Wind");
      ret.Add(R_VISIBILITY + R_VIS_RWYVIS, DecodeVisibility, "Visibility");
      ret.Add(R_PHENOMS, DecodePhenoms, "Phenomens");
      ret.Add(R_CLOUDS, DecodeClouds, "Clouds");
      ret.Add(R_TEMPDEW, DecodeTempDew, "Temperature/Dew point");
      ret.Add(R_PRESSURE, DecodePressure, "Pressure");
      ret.Add(R_RE_PHENOMS, DecodeRePhenoms, "Residual phenoms");
      ret.Add(R_WSS, DecodeWS, "Windshear infos");
      ret.Add(R_RWY_CONDS, DecodeRwyConds, "Runway condition info");
      // trend
      ret.Add(RT_TYPEDATES, DecodeTrendTypeDates, "Trend type (+ times)");
      ret.Add(RT_WIND, DecodeTrendWind, "Trend wind");
      ret.Add(RT_VISIBILITY, DecodeTrendVisibility, "Trend visibility");
      ret.Add(RT_PHENOMS, DecodeTrendPhenoms, "Trend phenomens");
      ret.Add(RT_CLOUD, DecodeTrendClouds, "Trend clouds");
      ret.Add(R_RMK, DecodeRemark, "Remarks");

      return ret;
    }

    #endregion Internal methods

    #region Decoders
    private static void DecodeMetarPrefix(Group[] grp, ref Metar obj)
    {
      obj.Type = (eType)Enum.Parse(typeof(eType), grp[0].Value);
    }
    private static void DecodeICAO(Group[] grp, ref Metar obj)
    {
      obj.ICAO = grp[0].Value;
    }
    private static void DecodeDate(Group[] grp, ref Metar obj)
    {
      obj.Date = new DayTime()
      {
        Day = grp[1].GetIntValue(),
        Hour = grp[2].GetIntValue(),
        Minute = grp[3].GetIntValue()
      };
    }
    private static void DecodeAuto(Group[] grp, ref Metar obj)
    {
      obj.IsAUTO = grp[0].Success;
    }
    private static void DecodeWind(Group[] grp, ref Metar obj)
    {
      Wind ret = new Wind();

      if (grp[2].Value == "VRB")
        ret.IsVariable = true;
      else
        ret.Direction = grp[2].GetIntValue();
      ret.Speed = grp[3].GetIntValue();
      ret.GustSpeed = (grp[5].Success ? (int?)grp[5].GetIntValue() : null);
      ret.Unit = (Wind.eUnit)Enum.Parse(typeof(Wind.eUnit), grp[6].Value);

      if (grp[7].Success)
      {
        ret.Variability = new WindVariable();
        ret.Variability.FromDirection = grp[8].GetIntValue();
        ret.Variability.ToDirection = grp[9].GetIntValue();
      }
      else
        ret.Variability = null;

      obj.Wind = ret;
    }
    private static void DecodeVisibility(Group[] grp, ref Metar obj)
    {
      Visibility ret = new Visibility();

      if (grp[1].Success)
        ret.SetCAVOK();
      else if (grp[2].Success)
        ret.SetSKC();
      else if (grp[3].Success)
      {
        ret.UseEUStyle = true;
        ret.Distance = grp[4].GetIntValue();
        if (grp[5].Success)
        {
          ret.DirectionSpecification = (ENG.Metar.Decoder.Visibility.eDirection)Enum.Parse(
            typeof(ENG.Metar.Decoder.Visibility.eDirection), grp[5].Value);
        }
        else
          ret.DirectionSpecification = null;
        if (grp[6].Success)
        {
          ret.OtherDistance = grp[7].GetIntValue();
          ret.OtherDirectionSpecification = (ENG.Metar.Decoder.Visibility.eDirection)Enum.Parse(
            typeof(ENG.Metar.Decoder.Visibility.eDirection), grp[8].Value);
        }
        else
        {
          ret.OtherDistance = null;
          ret.OtherDirectionSpecification = null;
        }
      }
      else
        ret.SetMiles(new Racional(
          grp[11].GetIntValue(),
          (grp[13].Success) ? grp[13].GetIntValue() : 1), grp[10].Success);

      obj.Visibility = ret;

      xDecodeRunwayVisibility(grp, ref obj);
    }
    private static void xDecodeRunwayVisibility(Group[] grp, ref Metar obj)
    {
      Visibility v = obj.Visibility;
      v.Runways = new List<RunwayVisibility>();

      if (grp[14].Success == false)
        return;

      RunwayVisibility rwy = null;

      string rwys = grp[14].Value;
      string oneRwyVisRegex = R_VIS_RWYVIS.Substring(1, R_VIS_RWYVIS.Length - 3);

      Match m = Regex.Match(rwys, oneRwyVisRegex);
      //(?#rwyVis)( (R(\d{2}(R|L|C)?)/(M|P)?(\d{4})(V(\d{4}))?(FT|U|N|D)?))

      while (m.Success)
      {
        rwy = new RunwayVisibility();

        rwy.Runway = m.Groups[3].Value;

        if (m.Groups[5].Success)
          rwy.DeviceMeasurementRestriction = (RunwayVisibility.eDeviceMeasurementRestriction)Enum.Parse(
            typeof(RunwayVisibility.eDeviceMeasurementRestriction), m.Groups[5].Value);
        else
          rwy.DeviceMeasurementRestriction = null;

        rwy.Distance = m.Groups[6].GetIntValue();

        if (m.Groups[7].Success)
          rwy.VariableVisibility = m.Groups[8].GetIntValue();

        if (m.Groups[9].Success)
        {
          if (m.Groups[9].Value == "FT")
          {
            rwy.IsInFeet = true;
            rwy.Tendency = null;
          }
          else
          {
            rwy.IsInFeet = false;
            rwy.Tendency = (RunwayVisibility.eTendency)
              Enum.Parse(typeof(RunwayVisibility.eTendency), m.Groups[9].Value);
          }

        }

        v.Runways.Add(rwy);
        m = m.NextMatch();
      }

    }
    private static void DecodePhenoms(Group[] grp, ref Metar obj)
    {
      PhenomInfo lst = new PhenomInfo(false);

      string str = grp[0].Value;

      Match m = Regex.Match(str, R_PHENOM_SET);
      ePhenomCollection coll = null;
      while (m.Success)
      {
        coll = new ePhenomCollection();

        coll = xDecodeePhenomSet(m);

        lst.Add(coll);

        m = m.NextMatch();

      }

      obj.Phenomens = lst;
    }
    private static ePhenomCollection xDecodeePhenomSet(Match setMatch)
    {
      ePhenomCollection ret = new ePhenomCollection();

      Match m = Regex.Match(setMatch.Value, R_PHENOM_ITEM);

      while (m.Success)
      {
        if (m.Value == "-")
          ret.Add(ePhenomCollection.ePhenom.Light);
        else if (m.Value == "+")
          ret.Add(ePhenomCollection.ePhenom.Heavy);
        else
          ret.Add(
            (ePhenomCollection.ePhenom)Enum.Parse(typeof(ePhenomCollection.ePhenom), m.Value));
        m = m.NextMatch();
      }

      return ret;
    }
    private static void DecodeClouds(Group[] grp, ref Metar obj)
    {

      if (grp[0].Length > 0)
      {
        CloudInfo ret = new CloudInfo();

        if (grp[1].Success)
          ret.SetNSC();
        else if (grp[2].Success)
          ret.SetSKC();
        else if (grp[3].Success)
        {
          if (grp[3].Value == "///")
            ret.SetVerticalVisibility(null);
          else
            ret.SetVerticalVisibility(
              grp[3].GetIntValue());
        }
        else
        {
          string str = grp[0].Value;
          Match m = Regex.Match(str, R_CLOUD_ITEM);

          while (m.Success)
          {
            ret.Add(xDecodeCloud(m));
            m = m.NextMatch();
          }
        }

        obj.Clouds = ret;
      }
      else
        obj.Clouds = null;
    }
    private static Cloud xDecodeCloud(Match m)
    {
      Cloud ret = new Cloud();

      ret.SetClouds(
        m.Groups[2].Value, m.Groups[3].GetIntValue(), m.Groups[4].Value == "CB", m.Groups[4].Value == "TCU");

      return ret;
    }
    private static void DecodeTempDew(Group[] grp, ref Metar obj)
    {
      obj.Temperature = grp[2].GetIntValue();
      if (grp[1].Success)
        obj.Temperature *= -1;
      obj.DewPoint = grp[4].GetIntValue();
      if (grp[3].Success)
        obj.DewPoint *= -1;
    }
    private static void DecodePressure(Group[] grp, ref Metar obj)
    {
      PressureInfo ret = new PressureInfo();

      if (grp[1].Value == "Q")
        ret.Set(grp[2].GetIntValue(), PressureInfo.eUnit.hPa);
      else
        ret.Set(grp[2].GetIntValue() / 100.0, ENG.Metar.Decoder.PressureInfo.eUnit.mmHq);

      obj.Pressure = ret;
    }
    private static void DecodeRePhenoms(Group[] grp, ref Metar obj)
    {
      PhenomInfo lst = new PhenomInfo(true);

      string str = grp[0].Value;

      Match m = Regex.Match(str, R_RE_PHENOM_ITEM);
      ePhenomCollection coll = null;
      while (m.Success)
      {
        coll = new ePhenomCollection();

        coll = xDecodeePhenomSet(m);

        lst.Add(coll);

        m = m.NextMatch();

      }

      obj.RePhenomens = lst;
    }
    private static void DecodeWS(Group[] grp, ref Metar obj)
    {
      WindShearInfo ret = new WindShearInfo();

      if (grp[0].Success)
      {
        if (grp[1].Success)
          ret.IsAllRunways = true;
        else
        {
          Match m = Regex.Match(grp[2].Value, R_WS);
          while (m.Success)
          {
            ret.Add(
              xDecodeWindShear(m));

            m = m.NextMatch();
          }

        }

        obj.WindShears = ret;
      }
      else
        obj.WindShears = null;
    }
    private static WindShear xDecodeWindShear(Match m)
    {
      WindShear ret = new WindShear();

      ret.Runway = m.Groups[1].Value;

      return ret;
    }
    private static void DecodeRwyConds(Group[] grp, ref Metar obj)
    {
      RunwayConditionInfo ret = new RunwayConditionInfo();

      if (grp[0].Success)
      {

        if (grp[1].Success)
          ret.IsSNOCLO = true;
        else if (grp[2].Success)
        {
          Match m = Regex.Match(grp[2].Value, R_RWY_COND);
          while (m.Success)
          {
            ret.Add(
              xDecodeRwyCond(m));

            m = m.NextMatch();
          }
        }

        obj.RunwayConditions = ret;
      }
      else
        obj.RunwayConditions = null;
    }
    private static RunwayCondition xDecodeRwyCond(Match m)
    {
      RunwayCondition ret = new RunwayCondition();

      ret.Runway = m.Groups[1].Value;
      if (m.Groups[8].Success)
        ret.IsCleared = true;
      else
      {
        ret.Deposit = (m.Groups[4].Value == "/" ? null : (RunwayCondition.eDeposit?)m.Groups[4].GetIntValue());
        ret.Contamination = (m.Groups[5].Value == "/" ? null : (RunwayCondition.eContamination?)m.Groups[5].GetIntValue());
        ret.Depth = (m.Groups[6].Value == "//" ? null : (RunwayCondition.eDepth?)m.Groups[6].GetIntValue());
        ret.Friction = (m.Groups[7].Value == "//" ? null : (RunwayCondition.eFriction?)m.Groups[7].GetIntValue());
      }

      return ret;
    }
    private static void DecodeTrendTypeDates(Group[] grp, ref Metar obj)
    {
      if (grp[0].Success)
      {

        TrendInfo ret = new TrendInfo();


        if (grp[1].Success)
          ret.Type = TrendInfo.eType.NOSIG;
        else
        {
          ret.Type = (TrendInfo.eType)Enum.Parse(typeof(TrendInfo.eType), grp[3].Value);

          Match m = Regex.Match(grp[4].Value, RT_TYPEDATE);
          while (m.Success)
          {
            ret.Times.Add(xDecodeTrendDate(m));

            m = m.NextMatch();
          }
        }

        obj.Trend = ret;
      }
      else
        obj.Trend = null;
    }
    private static TrendTime xDecodeTrendDate(Match m)
    {
      TrendTime ret = new TrendTime();

      ret.Type = (TrendTime.eType)Enum.Parse(typeof(TrendTime.eType), m.Groups[1].Value);
      ret.Hour = int.Parse(m.Groups[2].Value);
      ret.Minute = int.Parse(m.Groups[3].Value);

      return ret;
    }
    private static void DecodeTrendWind(Group[] grp, ref Metar obj)
    {
      if (obj.Trend == null)
        return;

      if (grp[0].Success)
      {
        Wind ret = new Wind();

        if (grp[2].Value == "VRB")
          ret.IsVariable = true;
        else
          ret.Direction = grp[2].GetIntValue();
        ret.Speed = grp[3].GetIntValue();
        ret.GustSpeed = (grp[5].Success ? (int?)grp[5].GetIntValue() : null);
        ret.Unit = (Wind.eUnit)Enum.Parse(typeof(Wind.eUnit), grp[6].Value);

        if (grp[7].Success)
        {
          ret.Variability = new WindVariable();
          ret.Variability.FromDirection = grp[8].GetIntValue();
          ret.Variability.ToDirection = grp[9].GetIntValue();
        }
        else
          ret.Variability = null;

        obj.Trend.Wind = ret;
      }
      else
        obj.Trend.Wind = null;

    }
    private static void DecodeTrendVisibility(Group[] grp, ref Metar obj)
    {
      if (obj.Trend == null)
        return;

      if (grp[0].Success)
      {
        TrendVisibility ret = new TrendVisibility();

        if (grp[2].Success)
          ret.SetCAVOK();
        else if (grp[3].Success)
        {
          ret.UseEUStyle = true;
          ret.Distance = grp[3].GetIntValue();
        }
        else
          ret.SetMiles(new Racional(
            grp[6].GetIntValue(),
            (grp[8].Success) ? grp[8].GetIntValue() : 1), grp[5].Success);

        obj.Trend.Visibility = ret;
      }
      else
        obj.Trend.Visibility = null;
    }
    private static void DecodeTrendPhenoms(Group[] grp, ref Metar obj)
    {
      if (obj.Trend == null)
        return;

      if (grp[0].Success)
      {
        if (grp[2].Success)
          obj.Trend.Phenomens = new PhenomInfo(false) { IsNSW = true };
        else
        {
          PhenomInfo lst = new PhenomInfo(false);

          string str = grp[1].Value;

          Match m = Regex.Match(str, R_PHENOM_SET);
          ePhenomCollection coll = null;
          while (m.Success)
          {
            coll = new ePhenomCollection();

            coll = xDecodeePhenomSet(m);

            lst.Add(coll);

            m = m.NextMatch();

          }

          obj.Trend.Phenomens = lst;
        }
      }
      else
        obj.Trend.Phenomens = null;
    }
    private static void DecodeTrendClouds(Group[] grp, ref Metar obj)
    {
      if (obj.Trend == null)
        return;

      if (grp[0].Success)
      {
        CloudInfo ret = new CloudInfo();

        if (grp[2].Success)
          ret.SetNSC();
        else if (grp[3].Success)
          ret.SetSKC();
        else if (grp[4].Success)
          ret.SetVerticalVisibility(
            grp[4].GetIntValue());
        else
        {
          string str = grp[0].Value;
          Match m = Regex.Match(str, R_CLOUD_ITEM);

          while (m.Success)
          {
            ret.Add(xDecodeCloud(m));
            m = m.NextMatch();
          }
        }

        obj.Trend.Clouds = ret;
      }
      else
        obj.Trend.Clouds = null;
    }
    private static void DecodeRemark(Group[] grp, ref Metar obj)
    {
      if (grp[0].Success)
        obj.Remark = grp[1].Value;
      else
        obj.Remark = null;
    }

    #endregion Decoders

    #region Inherited
#if INFO
public string ToInfo(InfoFormatter formatter)
{
  StringBuilder ret = new StringBuilder();

  /*
     * METAR:
     * 0 - icao
     * 1 - date-day
     * 2 - date-hour
     * 3 - date-minute
     * 4 - temperature
     * 5 - dew-point
     * 6 - wind
     * 
   * */

  ret.AppendFormat(
    formatter.MetarFormat,
    this.ICAO,
    this.Date.Day,
    this.Date.Hour,
    this.Date.Minute,
    this.Temperature,
    this.DewPoint,
    this.Wind.ToInfo (formatter),
    this.Visibility.ToInfo(formatter));

        string pom = ret.ToString();

        pom = formatter.Normalize(pom);

  return pom.ToString();
}

    #endif //INFO
    
    /// <summary>
    /// Returns item in metar string.
    /// </summary>
    /// <returns></returns>
    public string ToMetar()
    {
      StringBuilder ret = new StringBuilder();

      //    ENG.Metar.Decoder.Metar x =
      //ENG.Metar.Decoder.Metar.Create(
      //"METAR LKPR 312300Z VRB03KT 0800 R08/0300 R09/0400D -SN +RABR FEW020CB OVC040TCU M01/M10 Q0997 " +
      //"RERA RESN WS RWY24C RWY04 R04C/012345 R22/////// " +
      //"TEMPO FM1010 TL2020 AT1330 VRB03G20KT 010V040 M1/8SM RA SNBR FEW040 OVC050TCU RMK HOTOVO");

      ret.AppendSpaced(this.Type.ToString());
      ret.AppendSpaced(this.ICAO);
      ret.AppendSpaced(this.Date.ToMetar());
      if (this.IsAUTO) ret.AppendSpaced("AUTO");
      ret.AppendSpaced(this.Wind.ToMetar());
      ret.AppendSpaced(this.Visibility.ToMetar());
      if (this.Phenomens != null)
        ret.AppendSpaced(this.Phenomens.ToMetar());
      if (this.Clouds != null)
        ret.AppendSpaced(this.Clouds.ToMetar());
      ret.AppendSpaced(IntToMetarString(this.Temperature) + "/" + IntToMetarString(this.DewPoint));
      ret.AppendSpaced(this.Pressure.ToMetar());
      if (this.RePhenomens != null)
        ret.AppendSpaced(this.RePhenomens.ToMetar());
      if (this.WindShears != null)
        ret.AppendSpaced(this.WindShears.ToMetar());
      if (this.RunwayConditions != null)
        ret.AppendSpaced(this.RunwayConditions.ToMetar());
      if (this.Trend != null)
        ret.AppendSpaced(this.Trend.ToMetar());
      if (!string.IsNullOrEmpty(this.Remark))
        ret.Append("RMK " + this.Remark);

      return ret.ToString().TrimEnd();
    }

    #region MetarItem Members
#if INFO
    /// <summary>
    /// Returns item in text string.
    /// </summary>
    /// <returns></returns>
    public string ToInfo()
    {
      throw new NotImplementedException();
    }
#endif //INFO
    /// <summary>
    /// Proceed sanity check of inserted values.
    /// </summary>
    /// <param name="errors">Found errors.</param>
    /// <param name="warnings">Found warnings.</param>
    public void SanityCheck(ref List<string> errors, ref List<string> warnings)
    {
      Date.SanityCheck(ref errors, ref warnings);
      Wind.SanityCheck(ref errors, ref warnings);
      Visibility.SanityCheck(ref errors, ref warnings);
      if (Phenomens != null) Phenomens.SanityCheck(ref errors, ref warnings);
      if (Clouds != null) Clouds.SanityCheck(ref errors, ref warnings);
      Pressure.SanityCheck(ref errors, ref warnings);
      if (RePhenomens != null) RePhenomens.SanityCheck(ref errors, ref warnings);
      if (WindShears != null) WindShears.SanityCheck(ref errors, ref warnings);
      if (RunwayConditions != null) RunwayConditions.SanityCheck(ref errors, ref warnings);
      Trend.SanityCheck(ref errors, ref warnings);
    }

    #endregion MetarItem Members

    #endregion Inherited

    #region Private methods
    private static string IntToMetarString(int p)
    {
      if (p < 0)
        return "M" + (-p).ToString("00");
      else
        return p.ToString("00");
    }

    #endregion Private methods
  }
}
