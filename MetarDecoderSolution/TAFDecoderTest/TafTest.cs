using ENG.Metar.Decoder.Types.TAF;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TAFDecoderTest
{
    
    
    /// <summary>
    ///This is a test class for TafTest and is intended
    ///to contain all TafTest Unit Tests
    ///</summary>
  [TestClass()]
  public class TafTest
  {


    private TestContext testContextInstance;

    /// <summary>
    ///Gets or sets the test context which provides
    ///information about and functionality for the current test run.
    ///</summary>
    public TestContext TestContext
    {
      get
      {
        return testContextInstance;
      }
      set
      {
        testContextInstance = value;
      }
    }

    #region Additional test attributes
    // 
    //You can use the following additional attributes as you write your tests:
    //
    //Use ClassInitialize to run code before running the first test in the class
    //[ClassInitialize()]
    //public static void MyClassInitialize(TestContext testContext)
    //{
    //}
    //
    //Use ClassCleanup to run code after all tests in a class have run
    //[ClassCleanup()]
    //public static void MyClassCleanup()
    //{
    //}
    //
    //Use TestInitialize to run code before running each test
    //[TestInitialize()]
    //public void MyTestInitialize()
    //{
    //}
    //
    //Use TestCleanup to run code after each test has run
    //[TestCleanup()]
    //public void MyTestCleanup()
    //{
    //}
    //
    #endregion


    /// <summary>
    ///A test for Create
    ///</summary>
    [TestMethod()]
    public void CreateTest()
    {
      string taf = 
        "TAF UBBG 310500Z 3106/0106 VRB04KT 1500 BR OVC003 TX27/3112Z TN15/0102Z " +
      "TEMPO 3106/3107 0400 VV001 " +
      "BECMG 3107/3108 11008KT 9999 NSW SCT040 SCT100 " +
      "TEMPO 3112/3116 SCT030CB BKN060 " +
      "TEMPO 3122/0104 1500 BR BKN005";

      Taf expected = null; // TODO: Initialize to an appropriate value
      Taf actual;
      actual = Taf.Create(taf);
      Assert.AreEqual(expected, actual);
      Assert.Inconclusive("Verify the correctness of this test method.");
    }
  }
}
