using System;

namespace BadiService.Areas.Badi.Models
{
  public class BadiCalc
  {
    public BadiDate CalculateBadiDate(DateTime gDate, BeforeAfterSunset beforeAfter)
    {
      return new BadiDate();
    }

    public bool IsAfterNawRuz(DateTime gDate)
    {
      return gDate > GetNawRuz(gDate.Year);
    }

    public DateTime GetNawRuz(int year) { }
  }

  public enum BeforeAfterSunset
  {
    Undefined,
    BeforeSunset,
    AfterSunset
  }

  public class BadiDate
  {
    public int Year { get; set; }
    public int Month { get; set; }
    public int Day { get; set; }
  }
}