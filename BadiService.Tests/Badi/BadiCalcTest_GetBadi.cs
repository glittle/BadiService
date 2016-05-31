using System;
using BadiService.Areas.Badi.Models;
using NUnit.Framework;
using NUnit.Framework.Internal;

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
    public void GetBadiDate_AroundNawRuz(int gYear, int gMonth, int gDay, RelationToSunset relationToSunset, int bYear,
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

[Test]
    [TestCase(2015, 3, 21, RelationToSunset.gBeforeSunset, 172, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2016, 3, 20, RelationToSunset.gBeforeSunset, 173, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2017, 3, 20, RelationToSunset.gBeforeSunset, 174, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2018, 3, 21, RelationToSunset.gBeforeSunset, 175, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2019, 3, 21, RelationToSunset.gBeforeSunset, 176, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2020, 3, 20, RelationToSunset.gBeforeSunset, 177, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2021, 3, 20, RelationToSunset.gBeforeSunset, 178, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2022, 3, 21, RelationToSunset.gBeforeSunset, 179, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2023, 3, 21, RelationToSunset.gBeforeSunset, 180, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2024, 3, 20, RelationToSunset.gBeforeSunset, 181, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2025, 3, 20, RelationToSunset.gBeforeSunset, 182, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2026, 3, 21, RelationToSunset.gBeforeSunset, 183, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2027, 3, 21, RelationToSunset.gBeforeSunset, 184, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2028, 3, 20, RelationToSunset.gBeforeSunset, 185, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2029, 3, 20, RelationToSunset.gBeforeSunset, 186, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2030, 3, 20, RelationToSunset.gBeforeSunset, 187, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2031, 3, 21, RelationToSunset.gBeforeSunset, 188, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2032, 3, 20, RelationToSunset.gBeforeSunset, 189, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2033, 3, 20, RelationToSunset.gBeforeSunset, 190, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2034, 3, 20, RelationToSunset.gBeforeSunset, 191, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2035, 3, 21, RelationToSunset.gBeforeSunset, 192, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2036, 3, 20, RelationToSunset.gBeforeSunset, 193, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2037, 3, 20, RelationToSunset.gBeforeSunset, 194, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2038, 3, 20, RelationToSunset.gBeforeSunset, 195, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2039, 3, 21, RelationToSunset.gBeforeSunset, 196, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2040, 3, 20, RelationToSunset.gBeforeSunset, 197, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2041, 3, 20, RelationToSunset.gBeforeSunset, 198, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2042, 3, 20, RelationToSunset.gBeforeSunset, 199, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2043, 3, 21, RelationToSunset.gBeforeSunset, 200, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2044, 3, 20, RelationToSunset.gBeforeSunset, 201, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2045, 3, 20, RelationToSunset.gBeforeSunset, 202, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2046, 3, 20, RelationToSunset.gBeforeSunset, 203, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2047, 3, 21, RelationToSunset.gBeforeSunset, 204, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2048, 3, 20, RelationToSunset.gBeforeSunset, 205, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2049, 3, 20, RelationToSunset.gBeforeSunset, 206, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2050, 3, 20, RelationToSunset.gBeforeSunset, 207, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2051, 3, 21, RelationToSunset.gBeforeSunset, 208, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2052, 3, 20, RelationToSunset.gBeforeSunset, 209, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2053, 3, 20, RelationToSunset.gBeforeSunset, 210, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2054, 3, 20, RelationToSunset.gBeforeSunset, 211, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2055, 3, 21, RelationToSunset.gBeforeSunset, 212, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2056, 3, 20, RelationToSunset.gBeforeSunset, 213, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2057, 3, 20, RelationToSunset.gBeforeSunset, 214, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2058, 3, 20, RelationToSunset.gBeforeSunset, 215, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2059, 3, 20, RelationToSunset.gBeforeSunset, 216, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2060, 3, 20, RelationToSunset.gBeforeSunset, 217, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2061, 3, 20, RelationToSunset.gBeforeSunset, 218, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2062, 3, 20, RelationToSunset.gBeforeSunset, 219, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2063, 3, 20, RelationToSunset.gBeforeSunset, 220, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    [TestCase(2064, 3, 20, RelationToSunset.gBeforeSunset, 221, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2)]
    public void GetBadiDate_NawRuz(int gYear, int gMonth, int gDay, RelationToSunset relationToSunset, int bYear,
      int bMonth, int bDay, RelationToMidnight relationToMidnight)
    {
      _badi.GetBadiDate(new DateTime(gYear, gMonth, gDay), relationToSunset)
        .ShouldBe(new BadiDate(bYear, bMonth, bDay, relationToMidnight));
    }

  }
}