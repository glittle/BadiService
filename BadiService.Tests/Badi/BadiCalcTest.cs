using System;
using System.Data;
using BadiService.Areas.Badi.Models;
using NUnit.Framework;

namespace BadiService.Tests.Badi
{
  [TestFixture]
  public class BadiCalcTest
  {
    private BadiCalc _badi;

    [OneTimeSetUp]
    public void Setup()
    {
      _badi = new BadiCalc();

    }

    [Test]
    // before naw ruz
    [TestCase(2014, 1, 21, RelationToSunset.gDayBeforeSunset, 170, 17, 3, RelationToMidnight.bDayAfterMidnight)]
    // around naw ruz
    [TestCase(2014, 3, 21, RelationToSunset.gDayBeforeSunset, 171, 1, 1, RelationToMidnight.bDayAfterMidnight)]
    //2016
    [TestCase(2016, 3, 19, RelationToSunset.gDayBeforeSunset, 172, 19, 19, RelationToMidnight.bDayAfterMidnight)]
    [TestCase(2016, 3, 19, RelationToSunset.gDayAfterSunset, 173, 1, 1, RelationToMidnight.bDayBeforeMidnight)]

    [TestCase(2016, 3, 20, RelationToSunset.gDayBeforeSunset, 173, 1, 1, RelationToMidnight.bDayAfterMidnight)]
    [TestCase(2016, 3, 20, RelationToSunset.gDayAfterSunset, 173, 1, 2, RelationToMidnight.bDayBeforeMidnight)]

    [TestCase(2016, 3, 21, RelationToSunset.gDayBeforeSunset, 173, 1, 2, RelationToMidnight.bDayAfterMidnight)]


    // after naw ruz
    [TestCase(2016, 5, 26, RelationToSunset.gDayBeforeSunset, 173, 4, 11, RelationToMidnight.bDayAfterMidnight)]
    // around ayyam-i-ha
    [TestCase(2017, 2, 25, RelationToSunset.gDayBeforeSunset, 173, 0, 1, RelationToMidnight.bDayAfterMidnight)]
    [TestCase(2017, 2, 28, RelationToSunset.gDayBeforeSunset, 173, 0, 4, RelationToMidnight.bDayAfterMidnight)]
    [TestCase(2017, 3, 1, RelationToSunset.gDayBeforeSunset, 173, 19, 1, RelationToMidnight.bDayAfterMidnight)]

    public void GetBadiDate(int gYear, int gMonth, int gDay, RelationToSunset relationToSunset, int bYear, int bMonth, int bDay, RelationToMidnight relationToMidnight)
    {
      _badi.GetBadiDate(new DateTime(gYear, gMonth, gDay), relationToSunset).ShouldBe(new BadiDate(bYear, bMonth, bDay, relationToMidnight));
    }

    [Test]
    [TestCase(2014, 3, 21, 171, 1, 1, RelationToMidnight.bDayAfterMidnight)]
    [TestCase(2015, 3, 21, 172, 1, 1, RelationToMidnight.bDayAfterMidnight)]
    [TestCase(2016, 3, 20, 173, 1, 1, RelationToMidnight.bDayAfterMidnight)]
    [TestCase(2016, 3, 19, 173, 1, 1, RelationToMidnight.bDayBeforeMidnight)]
    public void GetGregorianDate(int gYear, int gMonth, int gDay, int bYear, int bMonth, int bDay, RelationToMidnight relationToMidnight)
    {
      _badi.GetGDate(bYear, bMonth, bDay, relationToMidnight).ShouldBeSameDayAs(new DateTime(gYear, gMonth, gDay));
    }
  }
}
