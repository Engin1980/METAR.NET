using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESystem.Extensions;

namespace Tutorial
{
  public partial class FrmTest : Form
  {
    public FrmTest()
    {
      InitializeComponent();
    }

    private void btnSyncDown_Click(object sender, EventArgs e)
    {
      AddInfo("Starting sync download");

      string metar;

      // this specifies the downloader - from where and how the metar will be downloaded.
      ENG.Metar.Downloader.IMetarRetriever retriever =
        new ENG.Metar.Downloader.Retrievers.OldLineWeatherRetriever();

      try
      {
        // synchronously download the metar, parameters are
        // 1) which airport; 2) from which source
        metar = ENG.Metar.Downloader.Downloader.DownloadMetar(
          txtIcao.Text.Trim(), retriever);

        txtMetar.Text = metar;

        AddInfo("... downloaded");
      }
      catch (Exception ex)
      {
        AddInfo("Sync download failed - " + ex.GetMessages ());
      }
    }

    private void ClearInfo()
    {
      txtResult.Text = "";
    }
    private void AddInfo(string line)
    {
      txtResult.Text += line + "\r\n";

      txtResult.Select(txtResult.Text.Length, 0);
      txtResult.ScrollToCaret();
    }

    private void btnAsyncDown_Click(object sender, EventArgs e)
    {
      AddInfo("Downloading metar - asynchro...");

      // this specifies the downloader - from where and how the metar will be downloaded.
      ENG.Metar.Downloader.IMetarRetriever retriever =
        new ENG.Metar.Downloader.Retrievers.OldLineWeatherRetriever();

      ENG.Metar.Downloader.Downloader.DownloadMetarAsync(
        txtIcao.Text,
        retriever,
        new ENG.Metar.Downloader.Downloader.DownloadMetarCompletedDelegate(OnCompleted));

      AddInfo("... asynchro request send, waiting for result.");
    }

    private void OnCompleted(ENG.Metar.Downloader.MetarResult result)
    {
      if (this.InvokeRequired)
        this.Invoke (new Action (() => {OnCompleted(result);}));
      else
      {
        if (result.IsSuccessful)
        {
          AddInfo("... asynchro result returned.");
          txtMetar.Text = result.Result;
        }
        else
        {
          AddInfo("Asynchro download failed - " + result.Exception.GetMessages());
        }
      }
    }

    private void btnSanityCheck_Click(object sender, EventArgs e)
    {
      AddInfo("Performing sanity check...");

      ENG.Metar.Decoder.Metar mtr = GetMetar();

      if (mtr == null) return;

      List<string> er = new List<string>();
      List<string> w = new List<string>();

      mtr.SanityCheck(ref er, ref w);      

      er.ForEach(i => AddInfo("Sanity check error: " + i));
      w.ForEach(i => AddInfo("Sanity check warning: " + i));
      AddInfo("...done");
    }

    private ENG.Metar.Decoder.Metar GetMetar()
    {
      ENG.Metar.Decoder.Metar ret = null;

      try
      {
        ret = ENG.Metar.Decoder.Metar.Create(txtMetar.Text);
      }
      catch (Exception ex)
      {
        AddInfo("Error - " + ex.GetMessages());
      }

      return ret;
    }

    private void btnTest_Click(object sender, EventArgs e)
    {
      AddInfo("Performing metar-string test...");

      string er = null;

      ENG.Metar.Decoder.Metar.CheckMetarString(txtMetar.Text, out er);

      AddInfo("Found errors: " + er);
      AddInfo("...done");
    }

    private void btnEncDec_Click(object sender, EventArgs e)
    {
      AddInfo("Performing encode-decode test...");

      ENG.Metar.Decoder.Metar mtr = GetMetar();

      if (mtr == null) return;

      AddInfo("Orig: \r\n" + txtMetar.Text.Trim());
      AddInfo("EnDe: \r\n" + mtr.ToMetar());

      AddInfo("...done");
    }

    private void btnLongInfo_Click(object sender, EventArgs e)
    {
      AddInfo("Performing long-info print...");

      ENG.Metar.Decoder.Metar mtr = GetMetar();

      if (mtr == null) return;

      ENG.Metar.Decoder.Formatters.InfoFormatter ifo =
        new ENG.Metar.Decoder.Formatters.CzechLongInfoFormatter();

      string str = mtr.ToInfo(ifo, true, true, true, true);

      AddInfo(str);

      AddInfo("...done");
    }

    private void btnShortInfo_Click(object sender, EventArgs e)
    {
      AddInfo("Performing short-info print...");

      ENG.Metar.Decoder.Metar mtr = GetMetar();

      if (mtr == null) return;

      ENG.Metar.Decoder.Formatters.InfoFormatter ifo =
        new ENG.Metar.Decoder.Formatters.ShortInfoFormatter();

      string str = mtr.ToInfo(ifo, true, true, true, true);

      AddInfo(str);

      AddInfo("...done");
    }
  }
}
