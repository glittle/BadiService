using BadiService.Areas.Badi.Models;
using NUnit.Framework;

namespace BadiService.Tests.Badi
{
  [TestFixture]
  public class BadiCalcTest_DaysInAyyamiHa
  {
    private BadiCalc _badi;

    [OneTimeSetUp]
    public void Setup()
    {
      _badi = new BadiCalc();
    }


    [Test]
    [TestCase(172, 4)]
    [TestCase(173, 4)]
    [TestCase(174, 5)]
    [TestCase(175, 4)]
    [TestCase(176, 4)]
    [TestCase(177, 4)]
    [TestCase(178, 5)]
    [TestCase(179, 4)]
    [TestCase(180, 4)]
    [TestCase(181, 4)]
    [TestCase(182, 5)]
    [TestCase(183, 4)]
    [TestCase(184, 4)]
    [TestCase(185, 4)]
    [TestCase(186, 4)]
    [TestCase(187, 5)]
    [TestCase(188, 4)]
    [TestCase(189, 4)]
    [TestCase(190, 4)]
    [TestCase(191, 5)]
    [TestCase(192, 4)]
    [TestCase(193, 4)]
    [TestCase(194, 4)]
    [TestCase(195, 5)]
    [TestCase(196, 4)]
    [TestCase(197, 4)]
    [TestCase(198, 4)]
    [TestCase(199, 5)]
    [TestCase(200, 4)]
    [TestCase(201, 4)]
    [TestCase(202, 4)]
    [TestCase(203, 5)]
    [TestCase(204, 4)]
    [TestCase(205, 4)]
    [TestCase(206, 4)]
    [TestCase(207, 5)]
    [TestCase(208, 4)]
    [TestCase(209, 4)]
    [TestCase(210, 4)]
    [TestCase(211, 5)]
    [TestCase(212, 4)]
    [TestCase(213, 4)]
    [TestCase(214, 4)]
    [TestCase(215, 4)]
    [TestCase(216, 5)]
    [TestCase(217, 4)]
    [TestCase(218, 4)]
    [TestCase(219, 4)]
    [TestCase(220, 5)]
    [TestCase(221, 4)]
    public void GetGregorianDate_AroundNawRuz(int bYear, int numDays)
    {
      _badi.DaysInAyyamiHa(bYear).ShouldBe(numDays);
    }
  }
}