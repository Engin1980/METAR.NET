using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;

using MetarDecoder;

namespace Tester
{
  class Program
  {
    public enum eTest
    {
      A,
      B,
      C
    }

    static void Main(string[] args)
    {
      TryAsync();
      return;

      MetarDownloader.Downloader d = new MetarDownloader.Downloader(
        new MetarDownloader.Retrievers.USAS1VatsimRetriever());
      string metar="";

      while (true)
      {
        Console.WriteLine("Enter icao:");
        string icao = Console.ReadLine().Trim().ToUpper();

        try
        {
          metar = d.DownloadMetar(icao);
          Console.WriteLine(metar);

          Console.WriteLine(" ... decoding");
          Comparer(metar);
        }
        catch (MetarDownloader.MetarDownloadException ex)
        {
          Console.WriteLine("Failed download metar: " + ex.ToString());
        }
        catch (MetarDecoder.MetarDecodeException ex)
        {
          Console.WriteLine("Failed to decode metar: " + ex.ToString());
          Console.WriteLine("Trying analysis:");

          string err;
          Metar.CheckMetarString(metar, out err);
          Console.WriteLine("Found error: " + err);
        }
      }

      //Comparer("METAR LKPR 312300Z 22009KT CAVOK 03/02 Q1006 NOSIG");
      //Comparer("METAR EGPH 312250Z 26008KT 240V300 CAVOK 03/M02 Q0992");
      //Comparer("METAR LIPZ 312250Z VRB03KT CAVOK 08/06 Q1010");
      //Comparer("METAR LOWG 312320Z AUTO 00000KT 0200 R35/0650N R17/1200D BCFG 06/05 Q1010 RMK BASE S CLD004 N CLD007");
      //Comparer("METAR EGSC 311750Z 26015G28KT 230V290 9999 BKN027 05/00 Q0994");
      //Comparer("METAR KABC 121755Z AUTO 21016G24KT 180V240 1SM R11/P6000FT -RA BR BKN015 OVC025 06/04 A2990 RMK A02 PK WND 20032/25 WSHFT 1715 VIS 3/4V1 1/2 VIS 3/4 RWY11 RAB07 CIG 013V017 CIG 017 RWY11 PRESFR SLP125 POOO3 6OOO9 T00640036 10066 21012 58033 TSNO $");
      //Comparer("METAR EETN 012150Z 26004KT 6000 SCT002 OVC012 04/04 Q1013 R08/000095 TEMPO 2000 BR");
      //Comparer("METAR EGSH 012050Z 23009KT 9999 -RA FEW007 BKN012 05/03 Q1009 TEMPO RA");
      ////Comparer(
      ////  "METAR LKPR 312300Z VRB03KT 0800 R08/0300 R09/0400V0600D -SN +RABR FEW020CB OVC040TCU M01/M10 Q0997 " +
      ////  "RERA RESN WS RWY24C RWY04 R04C/012345 R22/////// " +
      ////  "TEMPO FM1010 TL2020 AT1330 VRB03G20KT 010V040 M1/8SM RA SNBR FEW040 OVC050TCU RMK HOTOVO");

      
    }

    private static bool isDown = false;
    private static void TryAsync()
    {
      MetarDownloader.Downloader d = new MetarDownloader.Downloader(
        new MetarDownloader.Retrievers.OldLineWeatherRetriever());

      MetarDownloader.Downloader.DownloadMetarCompletedDelegate deleg = null;
      deleg = DownDone;

      isDown = false;
      d.DownloadMetarAsync("LKTB", deleg);
      while (!isDown)
      {
        Console.WriteLine("fuj");
      }
      Console.ReadKey();
      
    }

    private static void DownDone (MetarDownloader.MetarResult res)
    {
      Console.WriteLine("Finíto: ");
      if (res.IsSuccessful)
        Console.WriteLine(res.Result);
      else
        Console.WriteLine("Erroráč: " + res.Exception.Message);

      isDown = true;
    }



    private static void Comparer(string metar)
    {
      MetarDecoder.Metar weather = MetarDecoder.Metar.Create(metar);
      string nM = weather.ToMetar();

      Console.WriteLine("old: " + metar);
      Console.WriteLine("new: " + nM);
      Console.WriteLine();
      Console.ReadKey();
    }
  }
}
