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


    [Test]
    [TestCase(172, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2015, 3, 21)]
    [TestCase(173, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2016, 3, 20)]
    [TestCase(174, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2017, 3, 20)]
    [TestCase(175, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2018, 3, 21)]
    [TestCase(176, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2019, 3, 21)]
    [TestCase(177, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2020, 3, 20)]
    [TestCase(178, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2021, 3, 20)]
    [TestCase(179, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2022, 3, 21)]
    [TestCase(180, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2023, 3, 21)]
    [TestCase(181, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2024, 3, 20)]
    [TestCase(182, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2025, 3, 20)]
    [TestCase(183, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2026, 3, 21)]
    [TestCase(184, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2027, 3, 21)]
    [TestCase(185, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2028, 3, 20)]
    [TestCase(186, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2029, 3, 20)]
    [TestCase(187, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2030, 3, 20)]
    [TestCase(188, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2031, 3, 21)]
    [TestCase(189, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2032, 3, 20)]
    [TestCase(190, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2033, 3, 20)]
    [TestCase(191, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2034, 3, 20)]
    [TestCase(192, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2035, 3, 21)]
    [TestCase(193, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2036, 3, 20)]
    [TestCase(194, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2037, 3, 20)]
    [TestCase(195, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2038, 3, 20)]
    [TestCase(196, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2039, 3, 21)]
    [TestCase(197, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2040, 3, 20)]
    [TestCase(198, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2041, 3, 20)]
    [TestCase(199, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2042, 3, 20)]
    [TestCase(200, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2043, 3, 21)]
    [TestCase(201, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2044, 3, 20)]
    [TestCase(202, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2045, 3, 20)]
    [TestCase(203, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2046, 3, 20)]
    [TestCase(204, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2047, 3, 21)]
    [TestCase(205, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2048, 3, 20)]
    [TestCase(206, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2049, 3, 20)]
    [TestCase(207, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2050, 3, 20)]
    [TestCase(208, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2051, 3, 21)]
    [TestCase(209, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2052, 3, 20)]
    [TestCase(210, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2053, 3, 20)]
    [TestCase(211, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2054, 3, 20)]
    [TestCase(212, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2055, 3, 21)]
    [TestCase(213, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2056, 3, 20)]
    [TestCase(214, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2057, 3, 20)]
    [TestCase(215, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2058, 3, 20)]
    [TestCase(216, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2059, 3, 20)]
    [TestCase(217, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2060, 3, 20)]
    [TestCase(218, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2061, 3, 20)]
    [TestCase(219, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2062, 3, 20)]
    [TestCase(220, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2063, 3, 20)]
    [TestCase(221, 1, 1, RelationToMidnight.bDay_BeforeSunset_Frag2, 2064, 3, 20)]
    public void GetGregorianDate_NawRuz(int bYear, int bMonth, int bDay, RelationToMidnight relationToMidnight, int gYear, int gMonth, int gDay)
    {
      _badi.GetGDate(bYear, bMonth, bDay, relationToMidnight).ShouldBeSameDayAs(new DateTime(gYear, gMonth, gDay));
    }

  }


}