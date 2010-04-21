using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tutorial
{
  class Program
  {
    static void Main(string[] args)
    {
      DownloadMetarForHamburgSynchronically();

      DownloadMetarForHamburgAsynchronically();

      DecodeAndEncodeMetar();

      PrintShortInfo();

      PrintLongInfo();
    }

    private static void DecodeAndEncodeMetar()
    {
      // this is source example string
      string sourceMetar = "METAR LOWG 312320Z AUTO 00000KT 0200 R35/0650N R17/1200D BCFG 06/05 Q1010 RMK BASE S CLD004 N CLD007";

      // into this object decode metar will be stored
      ENG.Metar.Decoder.Metar metarObject = null;

      try
      {
        // try to decode metar - call static Create method
        metarObject =
          ENG.Metar.Decoder.Metar.Create(sourceMetar);
      }
      catch (ENG.Metar.Decoder.MetarDecodeException ex)
      {
        // Error during decode
        Console.WriteLine("Unable to parse metar from string. "+ ex.Message);
      }
      catch (Exception ex)
      {
        // Other error
        Console.WriteLine("Unknown error during decode. Info: " + ex.Message);
      }

      // If successfully decoded
      if (metarObject != null)
      {
        // creates back metar string
        string targetMetar = metarObject.ToMetar();

        // and compare string. should be the same.
        Console.WriteLine("Original metar:");
        Console.WriteLine(sourceMetar);
        Console.WriteLine("Decoded and encoded metar:");
        Console.WriteLine(targetMetar);
      }

      Console.ReadKey();
    }

    private static void DownloadMetarForHamburgAsynchronically()
    {
      Console.WriteLine("Downloading metar - asynchro...");

      // this specifies the downloader - from where and how the metar will be downloaded.
      ENG.Metar.Downloader.IMetarRetrieve retriever =
        new ENG.Metar.Downloader.Retrievers.OldLineWeatherRetriever();

      ENG.Metar.Downloader.Downloader.DownloadMetarAsync(
        "EDDH",
        retriever,
        new ENG.Metar.Downloader.Downloader.DownloadMetarCompletedDelegate (OnCompleted));

      // do something other interesting stuff
    }
    private static void OnCompleted(ENG.Metar.Downloader.MetarResult result)
    {
      if (result.IsSuccessful)
      {
        Console.WriteLine("Metar for Hamburg is: ");
        Console.WriteLine(result.Result);
      }
      else
      {
        Console.WriteLine("Error occurs. Description: " + result.Exception.Message);
      }
    }

    private static void DownloadMetarForHamburgSynchronically()
    {
      Console.WriteLine("Downloading metar - synchro...");

      // here will be the result
       string eddhMetar;

      // this specifies the downloader - from where and how the metar will be downloaded.
       ENG.Metar.Downloader.IMetarRetrieve retriever =
         new ENG.Metar.Downloader.Retrievers.OldLineWeatherRetriever();
      
      try
      {
        // synchronously download the metar, parameters are
        // 1) which airport; 2) from which source
        eddhMetar = ENG.Metar.Downloader.Downloader.DownloadMetar(
          "EDDH", retriever);

        Console.WriteLine("Metar for Hamburg is: ");
        Console.WriteLine(eddhMetar);
      }
      catch (Exception ex)
      {
        Console.WriteLine("Error occurs. Description: " + ex.Message);
      }

      Console.ReadKey();
    }

    private static void PrintShortInfo()
    {
      string sourceMetar = "METAR LOWG 312320Z AUTO 00000KT 0200 R35/0650N R17/1200D BCFG 06/05 Q1010 RMK BASE S CLD004 N CLD007";
      ENG.Metar.Decoder.Metar metar =
        ENG.Metar.Decoder.Metar.Create(sourceMetar);

      ENG.Metar.Decoder.Formatters.InfoFormatter ifo =
        new ENG.Metar.Decoder.Formatters.ShortInfoFormatter();

      string str = metar.ToInfo(ifo, true, true, true, true);

      Console.WriteLine(str);
      Console.ReadKey();
    }

    private static void PrintLongInfo()
    {
      string sourceMetar = "METAR LOWG 312320Z AUTO 00000KT 0200 R35/0650N R17/1200D BCFG 06/05 Q1010 RMK BASE S CLD004 N CLD007";
      ENG.Metar.Decoder.Metar metar =
        ENG.Metar.Decoder.Metar.Create(sourceMetar);

      ENG.Metar.Decoder.Formatters.InfoFormatter ifo =
        new ENG.Metar.Decoder.Formatters.LongInfoFormatter();

      string str = metar.ToInfo(ifo, true, true, true, true);

      Console.WriteLine(str);
      Console.ReadKey();
    }
  }
}
