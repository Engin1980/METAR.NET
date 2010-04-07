using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace ENG.Metar.Downloader
{
  /// <summary>
  /// Class responsible for downloading metar information from source.
  /// </summary>
  public class Downloader
  {
    /// <summary>
    /// Local decoder used to retrieve metar.
    /// </summary>
    IMetarRetrieve retr;
    /// <summary>
    /// Delegate used to announce when asynchronous download is completed.
    /// Is used for both, successful and unsuccessful downloads.
    /// </summary>
    /// <param name="result">Result containing data</param>
    public delegate void DownloadMetarCompletedDelegate(MetarResult result);
    /// <summary>
    /// private async synchro variable
    /// </summary>
    private string aIcao;
    /// <summary>
    /// private async synchro variable
    /// </summary>
    private DownloadMetarCompletedDelegate aDel;

    /// <summary>
    /// Initializes a new Instance of ENG.Metar.Downloader.Downloader
    /// </summary>
    /// <param name="metarRetriever">Metar retrievere used to decode metar from source stream</param>
    public Downloader(IMetarRetrieve metarRetriever)
    {
      retr = metarRetriever;
    }

    /// <summary>
    /// Download metar synchronously.
    /// </summary>
    /// <param name="icao">Icao code of airport/station.</param>
    /// <param name="metarRetriever">Metar retrievere used to decode metar from source stream</param>
    /// <returns>Metar as string.</returns>
    /// <exception cref="MetarDownloadException">
    /// Raised when any error occurs.
    /// </exception>
    public static string DownloadMetar(string icao, IMetarRetrieve metarRetriever)
    {
      Downloader d = new Downloader(metarRetriever);

      string ret = d.DownloadMetar(icao);

      return ret;
    }

    /// <summary>
    /// Download metar synchronously.
    /// </summary>
    /// <param name="ICAO">Icao code of airport/station.</param>
    /// <returns>Metar as string.</returns>
    /// <exception cref="MetarDownloadException">
    /// Raised when any error occurs.
    /// </exception>
    public string DownloadMetar(string ICAO)
    {
      string ret = "";

      WebRequest req = HttpWebRequest.Create(
        retr.GetUrlForICAO(ICAO));

      try
      {
        WebResponse resp = req.GetResponse();

        System.IO.Stream respStream = resp.GetResponseStream();

        ret = retr.DecodeMetar(respStream);

        respStream.Close();
        resp.Close();
      }
      catch (Exception ex)
      {
        throw new MetarDownloadException ("Failed to download metar from web.", ex) ;
      }

      return ret;
    }

    /// <summary>
    /// Download metar asynchronously.
    /// </summary>
    /// <param name="icao">Icao code of airport/station.</param>
    /// <param name="metarRetriever">Metar retrievere used to decode metar from source stream</param>
    /// <param name="downloadMetarCompletedDelegate">Delegate function raised when download is completed or error occured.</param>
    /// <exception cref="MetarDownloadException">
    /// Raised when any error occurs.
    /// </exception>
    public static void DownloadMetarAsync(string icao, IMetarRetrieve metarRetriever,
      DownloadMetarCompletedDelegate downloadMetarCompletedDelegate)
    {
      Downloader d = new Downloader(metarRetriever);

      d.DownloadMetarAsync(icao, downloadMetarCompletedDelegate);
    }

    /// <summary>
    /// Download metar asynchronously.
    /// </summary>
    /// <param name="icao">Icao code of airport/station.</param>
    /// <param name="downloadMetarCompletedDelegate">Delegate function raised when download is completed or error occured.</param>
    /// <exception cref="MetarDownloadException">
    /// Raised when any error occurs.
    /// </exception>
    public void DownloadMetarAsync(
      string icao, DownloadMetarCompletedDelegate downloadMetarCompletedDelegate)
    {
      System.Threading.Thread t = new System.Threading.Thread(
        new System.Threading.ThreadStart(DownloadAsynchronously));

      aIcao = icao;
      aDel = downloadMetarCompletedDelegate;

      t.Start();
    }

    /// <summary>
    /// Used to download metar asynchronously.
    /// </summary>
    private void DownloadAsynchronously()
    {
      string icao = aIcao;
      DownloadMetarCompletedDelegate del = aDel;
      MetarResult ret = null;
      try
      {
        string met = DownloadMetar(icao);
        ret = new MetarResult(met);
      }
      catch (Exception ex)
      {
        ret = new MetarResult(ex);
      }

      del(ret);
    }

  }
}
