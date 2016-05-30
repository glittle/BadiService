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
    [TestCase(171, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2014, 3, 21)]
    [TestCase(172, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2015, 3, 21)]
    [TestCase(173, 1, 1, RelationToMidnight.bEvePrior_AfterSunset_Frag1, 2016, 3, 19)]
    [TestCase(173, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2016, 3, 20)]
    public void GetGregorianDate_AroundNawRuz(int bYear, int bMonth, int bDay, RelationToMidnight relationToMidnight, int gYear, int gMonth, int gDay)
    {
      _badi.GetGDate(bYear, bMonth, bDay, relationToMidnight).ShouldBeSameDayAs(new DateTime(gYear, gMonth, gDay));
    }

    [Test]
    [TestCase(172, 18, 17, RelationToMidnight.bDay_BeforeSunset_Frag2, 2016, 2, 23)]
    [TestCase(172, 18, 17, RelationToMidnight.bEvePrior_AfterSunset_Frag1, 2016, 2, 22)]

    [TestCase(172, 17, 18, RelationToMidnight.bDay_BeforeSunset_Frag2, 2016, 2, 5)]
    public void GetGregorianDate_BeforeAyyamiHa(int bYear, int bMonth, int bDay, RelationToMidnight relationToMidnight, int gYear, int gMonth, int gDay)
    {
      _badi.GetGDate(bYear, bMonth, bDay, relationToMidnight).ShouldBeSameDayAs(new DateTime(gYear, gMonth, gDay));
    }

    [Test]
    [TestCase(172, 15, 18, RelationToMidnight.bDay_BeforeSunset_Frag2, 2015, 12, 29, TestName = "AroundJanA 18 Questions frag 2")]
    [TestCase(172, 15, 19, RelationToMidnight.bEvePrior_AfterSunset_Frag1, 2015, 12, 29, TestName = "AroundJanA 19 Questions frag 1")]
    [TestCase(172, 15, 19, RelationToMidnight.bDay_BeforeSunset_Frag2, 2015, 12, 30, TestName = "AroundJanA 19 Questions frag 2")]

    [TestCase(172, 16, 1, RelationToMidnight.bEvePrior_AfterSunset_Frag1, 2015, 12, 30, TestName = "AroundJanB 1 Honor frag 1")]
    [TestCase(172, 16, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2015, 12, 31, TestName = "AroundJanB 1 Honor frag 2")]

    [TestCase(172, 16, 2, RelationToMidnight.bEvePrior_AfterSunset_Frag1, 2015, 12, 31, TestName = "AroundJanB 2 Honor frag 1")]
    [TestCase(172, 16, 2, RelationToMidnight.bDay_BeforeSunset_Frag2, 2016, 1, 1, TestName = "AroundJanB 2 Honor frag 2")]

    [TestCase(172, 16, 3, RelationToMidnight.bEvePrior_AfterSunset_Frag1, 2016, 1, 1, TestName = "AroundJanB 3 Honor frag 1")]
    [TestCase(172, 16, 3, RelationToMidnight.bDay_BeforeSunset_Frag2, 2016, 1, 2, TestName = "AroundJanB 3 Honor frag 2")]

    public void GetGregorianDate_AroundJan1(int bYear, int bMonth, int bDay, RelationToMidnight relationToMidnight, int gYear, int gMonth, int gDay)
    {
      _badi.GetGDate(bYear, bMonth, bDay, relationToMidnight).ShouldBeSameDayAs(new DateTime(gYear, gMonth, gDay));
    }

  }


}