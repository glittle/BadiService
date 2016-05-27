using System;
using System.Web.UI.WebControls;

namespace BadiService.Areas.Badi.Models
{
  public class BadiCalc
  {
    private PresetKnowledge _presetKnowledge;
    public const int DefaultSunsetHour = 18;
    public const int DefaultSunsetMinute = 30;



    public BadiDate GetBadiDate(DateTime gDate, BeforeAfterSunset beforeAfter)
    {
      // not dealing with sunset. Force to before or after sunset...
      gDate = new DateTime(gDate.Year, gDate.Month, gDate.Day, DefaultSunsetHour, DefaultSunsetMinute + (beforeAfter == BeforeAfterSunset.AfterSunset ? 5 : -5), 0);

      var isAfterNawRuz = IsAfterNawRuz(gDate);
      var afterSunsetOffset = beforeAfter == BeforeAfterSunset.AfterSunset ? 1 : 0;

      var year = GetBadiYear(gDate);
      int month;
      int day;
      if (isAfterNawRuz)
      {
        var nawRuz = GetNawRuz(gDate.Year);
        day = gDate.DayOfYear - nawRuz.DayOfYear + afterSunsetOffset;
        month = (int)Math.Floor(day / 19D) + 1;
        day = day % 19;
        if (day == 0)
        {
          day = 19;
          month--;
        }
      }
      else
      {
        var lastDec31 = new DateTime(gDate.Year - 1, 12, 31);
        var lastNawRuz = GetNawRuz(gDate.Year - 1);

        day = gDate.DayOfYear + (lastDec31.DayOfYear - lastNawRuz.DayOfYear) + afterSunsetOffset;
        month = (int)Math.Floor(day / 19D) + 1;
        day = day % 19;
        if (day == 0)
        {
          day = 19;
          month--;
        }
        if (month >= 19)
        {
          var lastAyyamiHa = GetGDate(year, 19, 1).AddDays(-1);
          if (gDate.DayOfYear + afterSunsetOffset > lastAyyamiHa.DayOfYear)
          {
            month = 19;
            day = gDate.DayOfYear - lastAyyamiHa.DayOfYear + afterSunsetOffset;
          }
          else
          {
            month = 0;
          }
        }
      }

      return new BadiDate(year, month, day, BeforeAfterMidnight.AfterMidnight);

    }

    public bool IsAfterNawRuz(DateTime gDate)
    {
      return gDate >= GetNawRuz(gDate.Year);
    }

    public int GetBadiYear(DateTime gDate)
    {
      return gDate.Year - 1843 - (IsAfterNawRuz(gDate) ? 0 : 1);
    }

    public DateTime GetNawRuz(int gYear, bool dateOnlyNoTime = false)
    {
      var nawRuz = new DateTime(
        gYear,
        3,
        21 + PresetKnowledge.GetNawRuzOffset(gYear - 1843),
        dateOnlyNoTime ? 0 : DefaultSunsetHour,
        dateOnlyNoTime ? 0 : DefaultSunsetMinute,
        0
        );

      //TODO calculate sunset
      //if not dateOnlyNoTime

      return nawRuz;
    }


    public DateTime GetGDate(int bYear, int bMonth, int bDay, BeforeAfterMidnight beforeAfterMidnight = BeforeAfterMidnight.AfterMidnight, bool autoFix = false)
    {
      if (bMonth < 0)
      {
        if (autoFix)
        {
          bMonth = 1;
        }
        else
        {
          throw new ApplicationException("Invalid Badi month: " + bMonth);
        }
      }

      if (bMonth > 19)
      {
        if (autoFix)
        {
          bMonth = 19;
        }
        else
        {
          throw new ApplicationException("Invalid Badi month: " + bMonth);
        }
      }

      if (bDay < 1)
      {
        if (autoFix)
        {
          bDay = 1;
        }
        else
        {
          throw new ApplicationException("Invalid Badi day: " + bDay);
        }
      }
      if (bDay > 19)
      {
        if (autoFix)
        {
          bDay = 19;
        }
        else
        {
          throw new ApplicationException("Invalid Badi day: " + bDay);
        }
      }

      var gYear = bYear + 1843;
      var nawRuz = GetNawRuz(gYear);
      var beforeMidnightOffset = beforeAfterMidnight == BeforeAfterMidnight.BeforeMidnight ? -1 : 0;
      var answer = nawRuz.AddDays((bMonth - 1) * 19 + (bDay - 1 + beforeMidnightOffset));

      if (bMonth == 0 || bMonth == 19)
      {
        var nextNawRuz = GetNawRuz(gYear + 1);
        var startOfAla = nextNawRuz.AddDays(-19);

        if (bMonth == 19)
        {
          answer = startOfAla.AddDays(bDay - 1);
        }
        else
        {
          var firstAyyamiHa = GetGDate(bYear, 18, 19).AddDays(1);
          var lastAyyamiHa = GetGDate(bYear, 19, 1).AddDays(-1);

          var numDaysInAyyamiHa = DaysBetween(firstAyyamiHa, lastAyyamiHa);
          if (bDay > numDaysInAyyamiHa)
          {
            if (autoFix)
            {
              bDay = numDaysInAyyamiHa;
            }
            else
            {
              throw new ApplicationException("Invalid day for Ayyam-i-Ha: " + bDay);
            }
          }
          answer = startOfAla.AddDays(bDay - 1);
        }
      }
      return answer;
    }

    private int DaysBetween(DateTime d1, DateTime d2)
    {
      return 1 + (int)Math.Round(Math.Abs((d1 - d2).TotalDays));
    }

    private PresetKnowledge PresetKnowledge => _presetKnowledge ?? (_presetKnowledge = new PresetKnowledge());
  }
}