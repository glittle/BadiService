using System;

namespace BadiService.Areas.Badi.Models
{
  public class BadiCalc
  {
    private readonly SunCalc _sunCalc;
    private PresetKnowledge _presetKnowledge;

    public BadiCalc()
    {
      _sunCalc = new SunCalc();
    }

    private PresetKnowledge PresetKnowledge => _presetKnowledge ?? (_presetKnowledge = new PresetKnowledge());


    public BadiDate GetBadiDate(DateTime gSourceDate, RelationToSunset relationToSunset)
    {
      // strip off time
      gSourceDate = gSourceDate.Date;
      var bDayRelationToMidnight = relationToSunset == RelationToSunset.gAfterSunset
        ? RelationToMidnight.bEvePrior_AfterSunset_Frag1
        : RelationToMidnight.bDay_BeforeSunset_Frag2;

      if (relationToSunset == RelationToSunset.gAfterSunset)
      {
        gSourceDate = gSourceDate.AddDays(1);
      }

      var gYear = gSourceDate.Year;
      var gDayOfNawRuz = GetNawRuz(gYear, true);
      var gDayLoftiness1 = gDayOfNawRuz.AddDays(-19);

      var bYear = gYear - (gSourceDate >= gDayOfNawRuz ? 1843 : 1844);
      int bMonth;
      int bDay;

      var isBeforeLoftiness = gSourceDate < gDayLoftiness1;
      if (isBeforeLoftiness)
      {
        // back: Jan --> end of AyyamiHa
        var gDayLoftiness1LastYear = GetNawRuz(gYear - 1, true).AddDays(-19);
        var daysAfterLoftiness1LastYear = (int) Math.Round((gSourceDate - gDayLoftiness1LastYear).TotalDays);
        var numMonthsFromLoftinessLastYear = (int) Math.Floor(daysAfterLoftiness1LastYear / 19D);

        bDay = 1 + daysAfterLoftiness1LastYear - numMonthsFromLoftinessLastYear * 19;
        bMonth = numMonthsFromLoftinessLastYear;

        if (bMonth == 19)
        {
          bMonth = 0;
        }
      }
      else
      {
        // forward: Loftiness --> Dec
        var bDaysAfterLoftiness1 = (int) Math.Round((gSourceDate - gDayLoftiness1).TotalDays);
        var bNumMonthsFromLoftiness = (int) Math.Floor(bDaysAfterLoftiness1 / 19D);

        bDay = 1 + bDaysAfterLoftiness1 - bNumMonthsFromLoftiness * 19;
        bMonth = bNumMonthsFromLoftiness;

        if (bMonth == 0)
        {
          bMonth = 19;
        }
      }

      return new BadiDate(bYear, bMonth, bDay, bDayRelationToMidnight);
    }

    public DateTime GetGregorianDate(int bYear, int bMonth, int bDay, RelationToMidnight relationToMidnight = RelationToMidnight.bDay_BeforeSunset_Frag2,
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
          var numDaysInAyyamiHa = DaysInAyyamiHa(bYear);
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
          answer = GetGregorianDate(bYear, 18, 19).AddDays(bDay);

          break;

        case 19:
          var nextNawRuz = GetNawRuz(gYear + 1, true);
          var firstDayOfLoftiness = nextNawRuz.AddDays(-19);

          answer = firstDayOfLoftiness.AddDays(bDay - 1);
          break;

        default:
          var nawRuz = GetNawRuz(gYear, true);
          //var beforeMidnightOffset = relationToMidnight == RelationToMidnight.bDay_BeforeSunset_Frag2 ? 1 : 0;
          answer = nawRuz.AddDays((bMonth - 1)*19 + bDay - 1);
          break;
      }

      // if calcuating sunset, get sunset for this date!
      if (relationToMidnight == RelationToMidnight.bEvePrior_AfterSunset_Frag1)
      {
        answer = answer.AddDays(-1);
      }

      return answer;
    }

    public int DaysInAyyamiHa(int bYear)
    {
      var firstDayOfAyyamiHa = GetGregorianDate(bYear, 18, 19).AddDays(1);
      var lastDayOfAyyamiHa = GetGregorianDate(bYear, 19, 1).AddDays(-1);

      return DaysBetween(firstDayOfAyyamiHa, lastDayOfAyyamiHa);
    }

    private DateTime GetNawRuz(int gYear, bool frag2DateOnly = false)
    {
      // the first moment of the new year... at sunset usually on March 19 sunset or March 20 sunset
      // if frag2DateOnly, then the following day
      var nawRuz = new DateTime(
        gYear,
        3,
        (frag2DateOnly ? 21 : 20) + PresetKnowledge.GetNawRuzOffset(gYear - 1843)
        );

      return frag2DateOnly
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
      return 1 + (int) Math.Round(Math.Abs((d1 - d2).TotalDays));
    }
  }
}