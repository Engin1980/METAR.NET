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

#if SILVERLIGHT == false

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

#endif


#if SILVERLIGHT == false

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

#endif

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

    private class MyRequest
    {
      public WebRequest Request = null;
      public WebResponse Response = null;
      public Stream Stream = null;
      public DownloadMetarCompletedDelegate Finisher = null;
    }

    /// <summary>
    /// Used to download metar asynchronously.
    /// </summary>
    private void DownloadAsynchronously()
    {
      string icao = aIcao;

      MetarResult ret = null;

      WebRequest req = HttpWebRequest.Create(
        retr.GetUrlForICAO(icao));

      MyRequest mr = new MyRequest()
      {
        Request = req,
        Finisher = aDel
      };

      try
      {

        req.BeginGetResponse(new AsyncCallback(_BeginGetResponseCallback), mr);

      }
      catch (Exception ex)
      {
        ret = new MetarResult(
          new MetarDownloadException("Failed to download metar from web.", ex));
      }

      if (ret != null)
        mr.Finisher(ret);
    }

    private void _BeginGetResponseCallback(IAsyncResult asynchronousResult)
    {
      MetarResult ret = null;
      MyRequest myRequestState = (MyRequest)asynchronousResult.AsyncState;

      try
      {
        // State of request is asynchronous.
        WebRequest myHttpWebRequest = myRequestState.Request;
        myRequestState.Response = (HttpWebResponse)myHttpWebRequest.EndGetResponse(asynchronousResult);

        // Read the response into a Stream object.
        Stream responseStream = myRequestState.Response.GetResponseStream();
        myRequestState.Stream = responseStream;

        string metar = retr.DecodeMetar(responseStream);

        myRequestState.Stream.Close();
        myRequestState.Response.Close();

        ret = new MetarResult(metar);
      }
      catch (WebException ex)
      {
        ret = new MetarResult(
          new Exception("Failed to download data with metar.", ex));
      }

      myRequestState.Finisher(ret);
    }

  }
}
