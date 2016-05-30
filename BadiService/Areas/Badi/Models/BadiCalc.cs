using System;

namespace BadiService.Areas.Badi.Models
{
  public class BadiCalc
  {
    private PresetKnowledge _presetKnowledge;
    private readonly SunCalc _sunCalc;

    public BadiCalc()
    {
      _sunCalc = new SunCalc();
    }

    private PresetKnowledge PresetKnowledge => _presetKnowledge ?? (_presetKnowledge = new PresetKnowledge());


    public BadiDate GetBadiDate(DateTime gSourceDate, RelationToSunset relationToSunset)
    {
      // strip off time
      gSourceDate = gSourceDate.Date;

      var bDayRelationToMidnight = relationToSunset == RelationToSunset.gAfterSunset ? RelationToMidnight.bEvePrior_AfterSunset_Frag1 : RelationToMidnight.bDay_BeforeSunset_Frag2;

      int bMonth;
      int bDay;

      if (relationToSunset == RelationToSunset.gAfterSunset)
      {
        gSourceDate = gSourceDate.AddDays(1);
      }

      var gDayOfNawRuz = GetNawRuz(gSourceDate.Year, true);
      var gDayLoftiness1 = gDayOfNawRuz.AddDays(-19);

      var bYear = gSourceDate.Year - (gSourceDate >= gDayOfNawRuz ? 1843 : 1844);

      var isBeforeLoftiness = gSourceDate < gDayLoftiness1;
      if (isBeforeLoftiness)
      {
        // back: Jan --> end of AyyamiHa
        var dec31OfLastYear = new DateTime(gSourceDate.Year - 1, 12, 31).DayOfYear;
        var nawRuzOfLastYear = GetNawRuz(gSourceDate.Year - 1, true).DayOfYear;
        var daysInLastGregYear = dec31OfLastYear - nawRuzOfLastYear;
        var daysAfterLastNawRuz = daysInLastGregYear + gSourceDate.DayOfYear;

        var month0 = (int) Math.Floor(daysAfterLastNawRuz/19D);
        bDay = 1 + daysAfterLastNawRuz - month0*19;
        bMonth = month0 + 1;

        if (bMonth == 19)
        {
          bMonth = 0;
        }
      }
      else
      {
        // forward: Loftiness --> Dec
        var bDaysAfterLoftiness1 = gSourceDate.DayOfYear - gDayLoftiness1.DayOfYear;
        var bNumMonthsFromLoftiness = (int) Math.Floor(bDaysAfterLoftiness1/19D);

        bDay = 1 + bDaysAfterLoftiness1 - bNumMonthsFromLoftiness*19;
        bMonth = bNumMonthsFromLoftiness;

        if (bMonth == 0)
        {
          bMonth = 19;
        }
      }

      return new BadiDate(bYear, bMonth, bDay, bDayRelationToMidnight);
    }

    public DateTime GetGDate(int bYear, int bMonth, int bDay, RelationToMidnight relationToMidnight = RelationToMidnight.bDay_BeforeSunset_Frag2,
      bool autoFix = false)
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

      DateTime answer;

      var gYear = bYear + 1843;

      switch (bMonth)
      {
        case 0:
        case 19:
          var nextNawRuz = GetNawRuz(gYear + 1, true);
          var startOfAla = nextNawRuz.AddDays(-19);

          if (bMonth == 0)
          {
            var firstDayOfAyyamiHa = GetGDate(bYear, 18, 19).AddDays(1);
            var lastDayOfAyyamiHa = GetGDate(bYear, 19, 1).AddDays(-1);

            var numDaysInAyyamiHa = DaysBetween(firstDayOfAyyamiHa, lastDayOfAyyamiHa);
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
          }
          answer = startOfAla.AddDays(bDay - 1);
          break;

        default:
          var nawRuz = GetNawRuz(gYear, true);
          //var beforeMidnightOffset = relationToMidnight == RelationToMidnight.bDay_BeforeSunset_Frag2 ? 1 : 0;
          answer = nawRuz.AddDays((bMonth - 1) * 19 + bDay);
          break;
      }

      // if calcuating sunset, get sunset for this date!

      return answer;
    }

    private DateTime GetNawRuz(int gYear, bool dateOnly = false)
    {
      // the first moment of the new year... at sunset usually on March 19 sunset or March 20 sunset
      // if dateOnly, then the following day
      var nawRuz = new DateTime(
        gYear,
        3,
        (dateOnly ? 21 : 20) + PresetKnowledge.GetNawRuzOffset(gYear - 1843)
        );

      return dateOnly
        ? nawRuz
        : _sunCalc.AtSunset(nawRuz);
    }

    public int GetBadiYear(DateTime gDate)
    {
      return gDate.Year - 1843 - (IsAfterStartOfNawRuz(gDate) ? 0 : 1);
    }


    public bool IsAfterStartOfNawRuz(DateTime gDate)
    {
      return gDate >= GetNawRuz(gDate.Year);
    }

    private int DaysBetween(DateTime d1, DateTime d2)
    {
      return 1 + (int)Math.Round(Math.Abs((d1 - d2).TotalDays));
    }
  }
}