using System;
using BadiService.Areas.Badi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BadiService.Tests.Badi
{
  [TestClass]
  public class BadiCalcTest
  {
    private BadiCalc _badi;

    [TestInitialize]
    public void Setup()
    {
      _badi = new BadiCalc();

    }

    [TestMethod]
    public void GetBadiDate()
    {
      _badi.GetBadiDate(new DateTime(2016, 5, 26), BeforeAfterSunset.BeforeSunset).ShouldBe(new BadiDate(173, 4, 11, BeforeAfterMidnight.AfterMidnight));
    }

    [TestMethod]
    public void GetGregorianDate()
    {
      _badi.GetGDate(173, 4, 11).ShouldBeSameDayAs(new DateTime(2016, 5, 26));
    }
  }
}
