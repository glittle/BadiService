using System;
using BadiService.Areas.Badi.Models;
using NUnit.Framework;

namespace BadiService.Tests.Badi
{
  [TestFixture]
  public class BadiCalcTest_GetBadi
  {
    private BadiCalc _badi;

    [OneTimeSetUp]
    public void Setup()
    {
      _badi = new BadiCalc();
    }

    [Test]
    // before ayyam-i-ha
    [TestCase(2016, 1, 1, RelationToSunset.gBeforeSunset, 172, 16, 2, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2016, 1, 1, RelationToSunset.gAfterSunset, 172, 16, 3, RelationToMidnight.bEvePrior_AfterSunset_Frag1)]
    [TestCase(2016, 1, 21, RelationToSunset.gBeforeSunset, 172, 17, 3, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2016, 2, 20, RelationToSunset.gBeforeSunset, 172, 18, 14, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    public void GetBadiDate_JanToAyyamiHa(int gYear, int gMonth, int gDay, RelationToSunset relationToSunset, int bYear, int bMonth,
      int bDay, RelationToMidnight relationToMidnight)
    {
      _badi.GetBadiDate(new DateTime(gYear, gMonth, gDay), relationToSunset)
        .ShouldBe(new BadiDate(bYear, bMonth, bDay, relationToMidnight));
    }


    [Test]
    // around ayyam-i-ha
    [TestCase(2016, 2, 25, RelationToSunset.gBeforeSunset, 172, 18, 19, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2016, 2, 25, RelationToSunset.gAfterSunset, 172, 0, 1, RelationToMidnight.bEvePrior_AfterSunset_Frag1)]

    [TestCase(2016, 2, 26, RelationToSunset.gBeforeSunset, 172, 0, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]

    [TestCase(2016, 2, 29, RelationToSunset.gBeforeSunset, 172, 0, 4, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2016, 2, 29, RelationToSunset.gAfterSunset, 172, 19, 1, RelationToMidnight.bEvePrior_AfterSunset_Frag1)]

    [TestCase(2016, 3, 1, RelationToSunset.gBeforeSunset, 172, 19, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    public void GetBadiDate_AroundAyyamiHa(int gYear, int gMonth, int gDay, RelationToSunset relationToSunset, int bYear,
      int bMonth, int bDay, RelationToMidnight relationToMidnight)
    {
      _badi.GetBadiDate(new DateTime(gYear, gMonth, gDay), relationToSunset)
        .ShouldBe(new BadiDate(bYear, bMonth, bDay, relationToMidnight));
    }


    [Test]
    // around Naw-Ruz
    [TestCase(2016, 3, 19, RelationToSunset.gBeforeSunset, 172, 19, 19, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2016, 3, 19, RelationToSunset.gAfterSunset, 173, 1, 1, RelationToMidnight.bEvePrior_AfterSunset_Frag1)]
    [TestCase(2016, 3, 20, RelationToSunset.gBeforeSunset, 173, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2016, 3, 20, RelationToSunset.gAfterSunset, 173, 1, 2, RelationToMidnight.bEvePrior_AfterSunset_Frag1)]
    public void GetBadiDate_NawRuz(int gYear, int gMonth, int gDay, RelationToSunset relationToSunset, int bYear,
      int bMonth, int bDay, RelationToMidnight relationToMidnight)
    {
      _badi.GetBadiDate(new DateTime(gYear, gMonth, gDay), relationToSunset)
        .ShouldBe(new BadiDate(bYear, bMonth, bDay, relationToMidnight));
    }

    [Test]
    // after naw ruz
    [TestCase(2016, 3, 21, RelationToSunset.gBeforeSunset, 173, 1, 2, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2016, 3, 21, RelationToSunset.gAfterSunset, 173, 1, 3, RelationToMidnight.bEvePrior_AfterSunset_Frag1)]

    [TestCase(2016, 5, 26, RelationToSunset.gBeforeSunset, 173, 4, 11, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2016, 5, 26, RelationToSunset.gAfterSunset, 173, 4, 12, RelationToMidnight.bEvePrior_AfterSunset_Frag1)]

    [TestCase(2016, 12, 31, RelationToSunset.gBeforeSunset, 173, 16, 2, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2016, 12, 31, RelationToSunset.gAfterSunset, 173, 16, 3, RelationToMidnight.bEvePrior_AfterSunset_Frag1)]

    [TestCase(2017, 12, 31, RelationToSunset.gAfterSunset, 174, 16, 3, RelationToMidnight.bEvePrior_AfterSunset_Frag1)]
    [TestCase(2018, 12, 31, RelationToSunset.gAfterSunset, 175, 16, 2, RelationToMidnight.bEvePrior_AfterSunset_Frag1)]
    [TestCase(2019, 12, 31, RelationToSunset.gAfterSunset, 176, 16, 2, RelationToMidnight.bEvePrior_AfterSunset_Frag1)]
    [TestCase(2020, 12, 31, RelationToSunset.gAfterSunset, 177, 16, 3, RelationToMidnight.bEvePrior_AfterSunset_Frag1)]
    public void GetBadiDate_AfterNawRuz(int gYear, int gMonth, int gDay, RelationToSunset relationToSunset, int bYear,
      int bMonth, int bDay, RelationToMidnight relationToMidnight)
    {
      _badi.GetBadiDate(new DateTime(gYear, gMonth, gDay), relationToSunset)
        .ShouldBe(new BadiDate(bYear, bMonth, bDay, relationToMidnight));
    }
  }
}