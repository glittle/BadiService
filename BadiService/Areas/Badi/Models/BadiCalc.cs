﻿using System;

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

//      var afterSunsetOffset = relationToSunset == RelationToSunset.gAfterSunset ? 1 : 0;
//      gSourceDate = gSourceDate.AddDays(afterSunsetOffset);

      var bDayRelationToMidnight = relationToSunset == RelationToSunset.gAfterSunset ? RelationToMidnight.bEveFrag1 : RelationToMidnight.bDayFrag2;

      int bMonth;
      int bDay;

      if (relationToSunset == RelationToSunset.gAfterSunset)
      {
        gSourceDate = gSourceDate.AddDays(1);
      }

      var gDayOfNawRuz = GetNawRuz(gSourceDate.Year, true);
      var gDayLoftiness1 = gDayOfNawRuz.AddDays(-19);

      var bYear = gSourceDate.Year - (gSourceDate >= gDayOfNawRuz ? 1843 : 1844);

      var isAfterAyyamiHa = gSourceDate >= gDayLoftiness1;
      if (isAfterAyyamiHa)
      {
        // forward: Loftiness --> Dec
        var bDaysAfterLoftiness1 = gSourceDate.DayOfYear - gDayLoftiness1.DayOfYear;
        var bNumMonthsFromLoftiness = (int)Math.Floor(bDaysAfterLoftiness1 / 19D);

        bDay = bDaysAfterLoftiness1 - bNumMonthsFromLoftiness * 19 + 1;
        bMonth = bNumMonthsFromLoftiness;

        if (bMonth == 0)
        {
          bMonth = 19;
        }
      }
      else
      {
        // back: Jan --> end of AyyamiHa
        var lastDec31DayOfYear = new DateTime(gSourceDate.Year - 1, 12, 31).DayOfYear;
        var lastNawRuzDayOfYear = GetNawRuz(gSourceDate.Year - 1, true).DayOfYear;
        var daysInLastGregYear = lastDec31DayOfYear - lastNawRuzDayOfYear;

        var dayOfBadiYear = daysInLastGregYear + gSourceDate.DayOfYear;

        var month0 = (int)Math.Floor(dayOfBadiYear / 19D);
        bDay = dayOfBadiYear - month0 * 19 + 1;
        bMonth = month0 + 1;

        if (bMonth == 19)
        {
          bMonth = 0;
        }
      }

      return new BadiDate(bYear, bMonth, bDay, bDayRelationToMidnight);
    }

    public DateTime GetGDate(int bYear, int bMonth, int bDay, RelationToMidnight relationToMidnight = RelationToMidnight.bDayFrag2,
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
          var beforeMidnightOffset = relationToMidnight == RelationToMidnight.bDayFrag2 ? 1 : 0;
          answer = nawRuz.AddDays((bMonth - 1) * 19 + (bDay - 1 + beforeMidnightOffset));
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