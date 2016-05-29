using System;
using BadiService.Areas.Badi.Models;
using NUnit.Framework;

namespace BadiService.Tests.Badi
{
  [TestFixture]
  public class BadiCalcTest_GetGregorian
  {
    private BadiCalc _badi;

    [OneTimeSetUp]
    public void Setup()
    {
      _badi = new BadiCalc();
    }


    [Test]
    [TestCase(2014, 3, 21, 171, 1, 1, RelationToMidnight.bDayFrag2)]
    [TestCase(2015, 3, 21, 172, 1, 1, RelationToMidnight.bDayFrag2)]
    [TestCase(2016, 3, 20, 173, 1, 1, RelationToMidnight.bDayFrag2)]
    [TestCase(2016, 3, 19, 173, 1, 1, RelationToMidnight.bEveFrag1)]
    public void GetGregorianDate(int gYear, int gMonth, int gDay, int bYear, int bMonth, int bDay,
      RelationToMidnight relationToMidnight)
    {
      _badi.GetGDate(bYear, bMonth, bDay, relationToMidnight).ShouldBeSameDayAs(new DateTime(gYear, gMonth, gDay));
    }
  }
}